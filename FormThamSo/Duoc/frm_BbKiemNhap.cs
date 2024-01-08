using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frm_BbKiemNhap : DevExpress.XtraEditors.XtraForm
    {
        public frm_BbKiemNhap()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV != "30009")
            {
                this.cmbPL.Properties.Items.Clear();
                this.cmbPL.Properties.Items.AddRange(new object[] {
               "Thuốc",
                "Vật tư y tế",
                "Hóa chất"});
            }
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int _id = 0;
        public frm_BbKiemNhap(int id) 
        {
            InitializeComponent();
            _id = id;
            if (DungChung.Bien.MaBV != "30009")
            {
                this.cmbPL.Properties.Items.Clear();
                this.cmbPL.Properties.Items.AddRange(new object[] {
               "Thuốc",
                "Vật tư y tế",
                "Hóa chất"});
            }   

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<_Id> _lid = new List<_Id>();
        public class _Id
        {
            private int id;
            public int ID
            {
                set { id = value; }
                get { return id; }
            }
        }
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("TextBBKiemNhap.txt"))
                {
                    File.Delete("TextBBKiemNhap.txt");
                }
                else
                {
                    FileStream fs1;
                    fs1 = new FileStream("TextBBKiemNhap.txt", FileMode.Create);
                    StreamWriter sWriter = new StreamWriter(fs1, Encoding.UTF8);
                    sWriter.WriteLine("");
                    sWriter.Flush();
                    fs1.Close();
                }

                FileStream fs = new FileStream("TextBBKiemNhap.txt", FileMode.Append);
                StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file
                writeFile.WriteLine(txtTV1goi.Text);
                writeFile.WriteLine(lupTV1.Text);
                writeFile.WriteLine(txtChucDanh1.Text);


                writeFile.WriteLine(txtTV2goi.Text);
                writeFile.WriteLine(lupTV2.Text);
                writeFile.WriteLine(txtChucDanh2.Text);


                writeFile.WriteLine(txtTV3goi.Text);
                writeFile.WriteLine(lupTV3.Text);
                writeFile.WriteLine(txtChucDanh3.Text);


                writeFile.WriteLine(txtTV4goi.Text);
                writeFile.WriteLine(lupTV4.Text);
                writeFile.WriteLine(txtChucDanh4.Text);


                writeFile.WriteLine(txtTV5goi.Text);
                writeFile.WriteLine(lupTV5.Text);
                writeFile.WriteLine(txtChucDanh5.Text);


                writeFile.WriteLine(txtTV6goi.Text);
                writeFile.WriteLine(lupTV6.Text);
                writeFile.WriteLine(txtChucDanh6.Text);


                writeFile.WriteLine(txtTV7goi.Text);
                writeFile.WriteLine(lupTV7.Text);
                writeFile.WriteLine(txtChucDanh7.Text);


                writeFile.WriteLine(txtLyDo.Text);
                writeFile.WriteLine(txtKetThuc.Text);

                writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
                writeFile.Flush();

                writeFile.Close();

                List<String> chucdanh = new List<string>();
                if (txtChucDanh1.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh1.Text.ToString());
                }
                if (txtChucDanh2.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh2.Text.ToString());
                }
                if (txtChucDanh3.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh3.Text.ToString());
                }
                if (txtChucDanh4.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh4.Text.ToString());
                }
                if (txtChucDanh5.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh5.Text.ToString());
                }
                if (txtChucDanh6.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh6.Text.ToString());
                }
                if (txtChucDanh7.Text.ToString() != "")
                {
                    chucdanh.Add(txtChucDanh7.Text.ToString());
                }

                frmIn frm = new frmIn();
                int i = 1; int j = 0;
                string dscb = "", dscd = "", dscv = "", dscb1 = "", dscd1 = "", dscd2 = "", dscd3 = "";
                DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                var _ldichvu = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                                join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                select new
                                {
                                    dv.QCPC,
                                    dv.MaQD,
                                    dv.MaTam,
                                    dv.MaDV,
                                    nhomdv.TenNhom,
                                    tieunhomdv.TenTN,
                                    dv.TenDV,
                                    dv.DongY,
                                    dv.SoDK,
                                    dv.NuocSX,
                                    dv.NhaSX,
                                    dv.HamLuong
                                }).ToList();
                if (radLoaiBC.SelectedIndex == 1)
                {
                    #region có số lô 20001
                    if (DungChung.Bien.MaBV == "20001")
                    {

                        QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001 frmts = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001();
                        frmts.passCB = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001.PassCB(PassData);
                        frmts.ShowDialog();

                        BaoCao.Rep_BBKiemNhap_20001_Insolo rep = new BaoCao.Rep_BBKiemNhap_20001_Insolo();
                        rep.celThongKeDuoc.Text = _tencbIn;
                        //BaoCao.rep_BbKiemNhap rep = new BaoCao.rep_BbKiemNhap();
                        rep.tieude.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        rep.ngaykk.Value = "Hôm nay, " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        rep.tenbv.Value = DungChung.Bien.TenCQ;
                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {

                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        } if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += txtChucDanh2.Text.Trim() + "\n";
                        } if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += txtChucDanh3.Text.Trim() + "\n";
                        } if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += txtChucDanh4.Text.Trim() + "\n";
                            //if(txtChucDanh4.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV4.Text;
                            //}

                        } if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += txtChucDanh5.Text.Trim() + "\n";
                            //if (txtChucDanh5.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV5.Text;
                            //}
                        } if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += txtChucDanh6.Text.Trim() + "\n";
                            //if (txtChucDanh6.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV6.Text;
                            //}
                        } if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += txtChucDanh7.Text.Trim() + "\n";
                            //if (txtChucDanh7.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV7.Text;
                            //}
                        }
                        if ((cbocv1.Text).Length > 1)
                        {
                            dscv += cbocv1.Text.Trim() + "\n";
                        }
                        if ((cbocv2.Text).Length > 1)
                        {
                            dscv += cbocv2.Text.Trim() + "\n";
                        }
                        if ((cbocv3.Text).Length > 1)
                        {
                            dscv += cbocv3.Text.Trim() + "\n";
                        }
                        if ((cbocv4.Text).Length > 1)
                        {
                            dscv += cbocv4.Text.Trim() + "\n";
                        }
                        if ((cbocv5.Text).Length > 1)
                        {
                            dscv += cbocv5.Text.Trim() + "\n";
                        }
                        if ((cbocv6.Text).Length > 1)
                        {
                            dscv += cbocv6.Text.Trim() + "\n";
                        }
                        if ((cbocv7.Text).Length > 1)
                        {
                            dscv += cbocv7.Text.Trim() + "\n";
                        }
                        rep.TV1.Value = dscb.ToString();
                        rep.TV2.Value = dscd.ToString();
                        rep.TV3.Value = dscv.ToString();
                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {
                            //var q0 = (from id in _lid
                            //          join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                            //          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            //          join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                            //          join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                            //          join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom

                            //          select new
                            //          {
                            //              dv.MaQD,
                            //              nhapd.NgayNhap,
                            //              nhapdct.IDNhapct,
                            //              nhapdct.IDNhap,
                            //              nhapdct.VAT,
                            //              nhomdv.TenNhom,
                            //              tieunhomdv.TenTN,
                            //              nhapd.SoCT,
                            //              dv.TenDV,
                            //              dv.DongY,
                            //              nhapdct.DonVi,
                            //              dv.SoDK,
                            //              dv.NuocSX,
                            //              dv.NhaSX,
                            //              nhapd.MaCC,
                            //              nhapd.MaKP,
                            //              nhapdct.HanDung,
                            //              nhapdct.SoLo,
                            //              DonGia = nhapdct.DonGiaCT,
                            //              nhapdct.SoLuongN,
                            //              nhapdct.ThanhTienN
                            //          }).OrderBy(p => p.IDNhapct).ToList();
                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new
                                          {
                                              nhapdct.MaDV,
                                              nhapd.NgayNhap,
                                              nhapdct.IDNhapct,
                                              nhapdct.IDNhap,
                                              nhapdct.VAT,
                                              nhapd.SoCT,
                                              nhapdct.DonVi,
                                              nhapd.MaCC,
                                              nhapd.MaKP,
                                              nhapdct.HanDung,
                                              nhapdct.SoLo,
                                              DonGia = nhapdct.DonGiaCT,
                                              nhapdct.SoLuongN,
                                              nhapdct.ThanhTienN
                                          }).OrderBy(p => p.IDNhapct).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.MaDV equals dv.MaDV
                                      join id in _lid on nd.MaDV equals id.ID
                                      select new { nd, dv }).ToList();
                            var kphong = (from id in _lid
                                          join nd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nd.IDNhap
                                          join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                                          join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC
                                          select new
                                          {
                                              kp.TenKP,
                                              ncc.TenCC,
                                              nd.SoCT,
                                              nd.NgayNhap,
                                              nd.TenNguoiCC
                                          }).FirstOrDefault();
                            if (kphong != null)
                            {
                                DateTime ngaynhap = Convert.ToDateTime(kphong.NgayNhap);
                                rep.soct.Value = kphong.SoCT.ToString();
                                rep.tencty.Value = kphong.TenCC.ToString();
                                rep.tenkho.Value = kphong.TenKP.ToString();
                                rep.ngaynhap.Value = "Ngày " + ngaynhap.Day + " tháng " + ngaynhap.Month + " năm " + ngaynhap.Year;
                                rep.nguoigiao.Text = kphong.TenNguoiCC.ToString();
                            }

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.dv.MaQD,
                                         nd.nd.NgayNhap,
                                         nd.nd.IDNhapct,
                                         nd.nd.IDNhap,
                                         nd.nd.VAT,
                                         nd.dv.TenNhom,
                                         nd.dv.TenTN,
                                         nd.nd.SoCT,
                                         nd.dv.TenDV,
                                         nd.dv.NhaSX,
                                         nd.dv.NuocSX,
                                         nd.dv.DongY,
                                         nd.nd.DonVi,
                                         nd.dv.SoDK,
                                         nd.nd.HanDung,
                                         nd.nd.SoLo,
                                         DonGia = nd.nd.DonGia,
                                         nd.nd.SoLuongN,
                                         ThanhTienN = nd.nd.DonGia * nd.nd.SoLuongN,
                                     }).OrderBy(p => p.IDNhapct).ToList();
                            double TT = 0, ttt = 0, ttst = 0, vat = 0;
                            TT = q.Sum(p => p.ThanhTienN);
                            TT = Math.Round(TT, 0);
                            rep.cel_ttong.Text = TT.ToString("###,###");

                            vat = (q.Count() != 0) ? Convert.ToDouble(q.First().VAT) : 0.00;
                            if (vat > 0)
                            {
                                ttt = TT * (double)vat / 100;
                                ttt = Math.Round(ttt, 0);
                                rep.cel_ttthue.Text = ttt.ToString("###,###");
                            }
                            ttst = TT + ttt;
                            ttst = Math.Round(ttst, 0);
                            rep.cel_ttsauthue.Text = ttst.ToString("###,###");
                            rep.doctien.Value = DungChung.Ham.DocTienBangChu(ttst, " đồng./");
                            rep.ykien.Value = txtLyDo.Text;
                            rep.ketoan.Text = DungChung.Bien.KeToanTruong;
                            rep.thukho.Text = DungChung.Bien.ThuKho;
                            rep.Truongkhoaduoc.Text = DungChung.Bien.TruongKhoaDuoc;

                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;

                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                        #endregion
                    }
                    else if (DungChung.Bien.MaBV == "30009")
                    {
                        BaoCao.Rep_BBKiemNhap_InSoLo_30009 rep = new BaoCao.Rep_BBKiemNhap_InSoLo_30009(chucdanh);
                        //BaoCao.rep_BbKiemNhap rep = new BaoCao.rep_BbKiemNhap();
                        rep.TieuDe.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        rep.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;

                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        if ((cbocv1.Text).Length > 1)
                        {
                            dscv += cbocv1.Text.Trim() + "\n";
                        }
                        if ((cbocv2.Text).Length > 1)
                        {
                            dscv += cbocv2.Text.Trim() + "\n";
                        }
                        if ((cbocv3.Text).Length > 1)
                        {
                            dscv += cbocv3.Text.Trim() + "\n";
                        }
                        if ((cbocv4.Text).Length > 1)
                        {
                            dscv += cbocv4.Text.Trim() + "\n";
                        }
                        if ((cbocv5.Text).Length > 1)
                        {
                            dscv += cbocv5.Text.Trim() + "\n";
                        }
                        if ((cbocv6.Text).Length > 1)
                        {
                            dscv += cbocv6.Text.Trim() + "\n";
                        }
                        if ((cbocv7.Text).Length > 1)
                        {
                            dscv += cbocv7.Text.Trim() + "\n";
                        }
                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV3goi.Value = dscv.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.CD1.Value = txtChucDanh1.Text;
                        rep.CD2.Value = txtChucDanh2.Text;
                        rep.CD3.Value = txtChucDanh3.Text;
                        rep.CD4.Value = txtChucDanh4.Text;
                        rep.CD5.Value = txtChucDanh5.Text;

                        rep.Thoigian.Value = txtThoiGian.Text;
                        rep.Diadiem.Value = txtDiaDiem.Text;
                        rep.NoiDung.Value = txtNoiDung.Text;
                        rep.YKNX.Value = txtLyDo.Text;
                        rep.KetThuc.Value = txtKetThuc.Text;

                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {
                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapd, nhapdct }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      //id in _lid
                                      //          join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                                      //          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      //          join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                      //          join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                      //          join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                      select new
                                      {
                                          dv.QCPC,
                                          dv.NhaSX,
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.HamLuong,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          nd.nhapd.TenNguoiCC,
                                          dv.SoDK,
                                          dv.NuocSX,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.nhapdct.VAT > 0) ? nd.nhapdct.DonGiaCT : (dv.DongY == 1 ? nd.nhapdct.DonGiaDY : nd.nhapdct.DonGia), // dv.DongY == 1 ? nhapdct.DonGiaDY : (((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023") && nhapdct.VAT > 0) ? nhapdct.DonGiaCT : nhapdct.DonGia),
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.MaQD,
                                         nd.NhaSX,
                                         nd.QCPC,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = DungChung.Bien.MaBV == "12122" ? (nd.TenDV + nd.HamLuong) : nd.TenDV,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NuocSX,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.VAT > 0) ? (nd.DonGia * nd.SoLuongN) : nd.ThanhTienN,
                                     }).OrderBy(p => p.IDNhapct).ToList();
                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);
                            if (_lid.Count == 1 && (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001"))
                            {
                                rep.celVAT_lbl.Text = "VAT: ";
                                if (q.Count > 0)
                                {
                                    double vat = (double)q.First().VAT;
                                    rep.celVAT.Text = vat.ToString() + "%";
                                    if (vat > 0)
                                    {
                                        TT = TT + TT * (double)vat / 100;

                                    }
                                }
                            }
                            TT = Math.Round(TT, 0);
                            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
                                rep.colThanhTienTong.Text = TT.ToString("###,###");
                            else
                            {
                                string a = string.Format(DungChung.Bien.FormatString[1], TT);
                                rep.colThanhTienTong.Text = a; //TT.ToString("###,###.00");
                            }
                            if (tungay.ToString().Substring(0, 10) == denngay.ToString().Substring(0, 10))
                            {
                                rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                            }
                            else// if(tungay!=denngay)
                            {
                                rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                            }
                            if (chkIn.Checked == true)
                            {
                                rep.InCD.Value = ("Tổ trưởng tổ kiểm nhập").ToUpper();
                            }
                            if (chkIn.Checked == false)
                            {
                                rep.InCD.Value = ("Chủ tịch hội đồng").ToUpper();
                            }
                            rep.TongTien.Value = TT;

                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;

                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                    }
                    else
                    {
                        #region bv khác
                        BaoCao.Rep_BbKiemNhap_InSoLo rep = new BaoCao.Rep_BbKiemNhap_InSoLo(chucdanh);
                        //BaoCao.rep_BbKiemNhap rep = new BaoCao.rep_BbKiemNhap();
                        rep.TieuDe.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        rep.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;

                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        } if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                        } if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                        } if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                        } if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                        } if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                        } if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        if ((cbocv1.Text).Length > 1)
                        {
                            dscv += cbocv1.Text.Trim() + "\n";
                        }
                        if ((cbocv2.Text).Length > 1)
                        {
                            dscv += cbocv2.Text.Trim() + "\n";
                        }
                        if ((cbocv3.Text).Length > 1)
                        {
                            dscv += cbocv3.Text.Trim() + "\n";
                        }
                        if ((cbocv4.Text).Length > 1)
                        {
                            dscv += cbocv4.Text.Trim() + "\n";
                        }
                        if ((cbocv5.Text).Length > 1)
                        {
                            dscv += cbocv5.Text.Trim() + "\n";
                        }
                        if ((cbocv6.Text).Length > 1)
                        {
                            dscv += cbocv6.Text.Trim() + "\n";
                        }
                        if ((cbocv7.Text).Length > 1)
                        {
                            dscv += cbocv7.Text.Trim() + "\n";
                        }
                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV3goi.Value = dscv.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.CD1.Value = txtChucDanh1.Text;
                        rep.CD2.Value = txtChucDanh2.Text;
                        rep.CD3.Value = txtChucDanh3.Text;
                        rep.CD4.Value = txtChucDanh4.Text;
                        rep.CD5.Value = txtChucDanh5.Text;

                        rep.Thoigian.Value = txtThoiGian.Text;
                        rep.Diadiem.Value = txtDiaDiem.Text;
                        rep.NoiDung.Value = txtNoiDung.Text;
                        rep.YKNX.Value = txtLyDo.Text;
                        rep.KetThuc.Value = txtKetThuc.Text;

                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {
                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapd, nhapdct }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      //id in _lid
                                      //          join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                                      //          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      //          join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                      //          join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                      //          join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                      select new
                                      {
                                          dv.QCPC,
                                          dv.NhaSX,
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.HamLuong,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          nd.nhapd.TenNguoiCC,
                                          dv.SoDK,
                                          dv.NuocSX,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.nhapdct.VAT > 0) ? nd.nhapdct.DonGiaCT : (dv.DongY == 1 ? nd.nhapdct.DonGiaDY : nd.nhapdct.DonGia), // dv.DongY == 1 ? nhapdct.DonGiaDY : (((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023") && nhapdct.VAT > 0) ? nhapdct.DonGiaCT : nhapdct.DonGia),
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.MaQD,
                                         nd.NhaSX,
                                         nd.QCPC,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = DungChung.Bien.MaBV == "12122" ? (nd.TenDV + nd.HamLuong) : nd.TenDV,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NuocSX,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.VAT > 0) ? (nd.DonGia * nd.SoLuongN) : nd.ThanhTienN,
                                     }).OrderBy(p => p.IDNhapct).ToList();
                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);
                            if (_lid.Count == 1 && (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001"))
                            {
                                rep.celVAT_lbl.Text = "VAT: ";
                                if (q.Count > 0)
                                {
                                    double vat = (double)q.First().VAT;
                                    rep.celVAT.Text = vat.ToString() + "%";
                                    if (vat > 0)
                                    {
                                        TT = TT + TT * (double)vat / 100;

                                    }
                                }
                            }
                            TT = Math.Round(TT, 0);
                            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
                                rep.colThanhTienTong.Text = TT.ToString("###,###");
                            else
                            {
                                string a = string.Format(DungChung.Bien.FormatString[1], TT);
                                rep.colThanhTienTong.Text = a; //TT.ToString("###,###.00");
                            }
                            if (tungay.ToString().Substring(0, 10) == denngay.ToString().Substring(0, 10))
                            {
                                rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                            }
                            else// if(tungay!=denngay)
                            {
                                rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                            }
                            if (chkIn.Checked == true)
                            {
                                rep.InCD.Value = ("Tổ trưởng tổ kiểm nhập").ToUpper();
                            }
                            if (chkIn.Checked == false)
                            {
                                rep.InCD.Value = ("Chủ tịch hội đồng").ToUpper();
                            }
                            rep.TongTien.Value = TT;

                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;

                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                        #endregion
                    }
                }
                else
                {
                    #region ko số lô 20001
                    if (DungChung.Bien.MaBV == "20001")
                    {
                        QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001 frmts = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001();
                        frmts.passCB = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001.PassCB(PassData);
                        frmts.ShowDialog();

                        BaoCao.Rep_BBKiemNhap_20001 rep = new BaoCao.Rep_BBKiemNhap_20001();
                        rep.celThongKeDuoc.Text = _tencbIn;
                        //BaoCao.rep_BbKiemNhap rep = new BaoCao.rep_BbKiemNhap();
                        rep.tieude.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        rep.ngaykk.Value = "Hôm nay, " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        rep.tenbv.Value = DungChung.Bien.TenCQ;
                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {

                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += txtChucDanh2.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += txtChucDanh3.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += txtChucDanh4.Text.Trim() + "\n";
                            //if (txtChucDanh4.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV4.Text;
                            //}
                        }
                        if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += txtChucDanh5.Text.Trim() + "\n";
                            //if (txtChucDanh5.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV5.Text;
                            //}
                        }
                        if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += txtChucDanh6.Text.Trim() + "\n";
                            //if (txtChucDanh6.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV6.Text;
                            //}
                        }
                        if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += txtChucDanh7.Text.Trim() + "\n";
                            //if (txtChucDanh7.Text.ToString().Trim().ToLower() == "thống kê dược")
                            //{
                            //    rep.celThongKeDuoc.Text = lupTV7.Text;
                            //}
                        }
                        if ((cbocv1.Text).Length > 1)
                        {
                            dscv += cbocv1.Text.Trim() + "\n";
                        }
                        if ((cbocv2.Text).Length > 1)
                        {
                            dscv += cbocv2.Text.Trim() + "\n";
                        }
                        if ((cbocv3.Text).Length > 1)
                        {
                            dscv += cbocv3.Text.Trim() + "\n";
                        }
                        if ((cbocv4.Text).Length > 1)
                        {
                            dscv += cbocv4.Text.Trim() + "\n";
                        }
                        if ((cbocv5.Text).Length > 1)
                        {
                            dscv += cbocv5.Text.Trim() + "\n";
                        }
                        if ((cbocv6.Text).Length > 1)
                        {
                            dscv += cbocv6.Text.Trim() + "\n";
                        }
                        if ((cbocv7.Text).Length > 1)
                        {
                            dscv += cbocv7.Text.Trim() + "\n";
                        }
                        rep.TV1.Value = dscb.ToString();
                        rep.TV2.Value = dscd.ToString();
                        rep.TV3.Value = dscv.ToString();
                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {
                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 1)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapd, nhapdct }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      //    id in _lid
                                      //join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                                      //join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      //join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                      //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                      //join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom

                                      select new
                                      {
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          dv.SoDK,
                                          dv.NuocSX,
                                          dv.NhaSX,
                                          nd.nhapd.MaCC,
                                          nd.nhapd.MaKP,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = nd.nhapdct.DonGiaCT,
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();
                            var kphong = (from id in _lid
                                          join nd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nd.IDNhap
                                          join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                                          join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC
                                          select new
                                          {
                                              kp.TenKP,
                                              ncc.TenCC,
                                              nd.SoCT,
                                              nd.NgayNhap,
                                              nd.TenNguoiCC
                                          }).FirstOrDefault();
                            if (kphong != null)
                            {
                                DateTime ngaynhap = Convert.ToDateTime(kphong.NgayNhap);
                                rep.soct.Value = kphong.SoCT.ToString();
                                rep.tencty.Value = kphong.TenCC.ToString();
                                rep.tenkho.Value = kphong.TenKP.ToString();
                                rep.ngaynhap.Value = "Ngày " + ngaynhap.Day + " tháng " + ngaynhap.Month + " năm " + ngaynhap.Year;
                                rep.nguoigiao.Text = kphong.TenNguoiCC.ToString();
                            }

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.MaQD,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         nd.TenDV,
                                         nd.NhaSX,
                                         nd.NuocSX,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = nd.DonGia * nd.SoLuongN,
                                     }).OrderBy(p => p.IDNhapct).ToList();
                            double TT = 0, ttt = 0, ttst = 0, vat = 0;
                            TT = q.Sum(p => p.ThanhTienN);
                            TT = Math.Round(TT, 0);
                            rep.cel_ttong.Text = TT.ToString("###,###");
                            vat = q.First().VAT;
                            if (vat > 0)
                            {
                                ttt = TT * (double)vat / 100;
                                ttt = Math.Round(ttt, 0);
                                rep.cel_ttthue.Text = ttt.ToString("###,###");
                            }
                            else
                            {
                                rep.cel_ttthue.Text = "0";
                            }
                            ttst = TT + ttt;
                            ttst = Math.Round(ttst, 0);
                            rep.cel_ttsauthue.Text = ttst.ToString("###,###");
                            rep.doctien.Value = DungChung.Ham.DocTienBangChu(ttst, " đồng./");
                            rep.ykien.Value = txtLyDo.Text;
                            rep.ketoan.Text = DungChung.Bien.KeToanTruong;
                            rep.thukho.Text = DungChung.Bien.ThuKho;
                            rep.Truongkhoaduoc.Text = DungChung.Bien.TruongKhoaDuoc;
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;

                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                        #endregion
                    }
                    else if (DungChung.Bien.MaBV == "24297")
                    {
                        #region bv khác
                        BaoCao.rep_BbKiemNhap_12345 repT = new BaoCao.rep_BbKiemNhap_12345();
                        repT.TieuDe.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        repT.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;

                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {

                            dscb += txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += txtChucDanh1.Text.Trim() + ": " + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += txtChucDanh2.Text.Trim() + ": " + "\n";
                        }
                        if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += txtChucDanh3.Text.Trim() + ": " + "\n";
                        }
                        if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += txtChucDanh4.Text.Trim() + ": " + "\n";
                        }
                        if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += txtChucDanh5.Text.Trim() + ": " + "\n";
                        }
                        if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += txtChucDanh6.Text.Trim() + ": " + "\n";
                        }
                        if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += txtChucDanh7.Text.Trim() + ": " + "\n";
                        }
                        repT.TV1goi.Value = dscb.ToString();
                        repT.TV2goi.Value = dscd.ToString();
                        repT.TV1.Value = lupTV1.Text;
                        repT.TV2.Value = lupTV2.Text;
                        repT.TV3.Value = lupTV3.Text;
                        repT.TV4.Value = lupTV4.Text;
                        repT.TV5.Value = lupTV5.Text;
                        repT.TV6.Value = lupTV6.Text;
                        repT.TV7.Value = lupTV7.Text;
                        repT.CD1.Value = txtChucDanh1.Text;
                        repT.CD2.Value = txtChucDanh2.Text;
                        repT.CD3.Value = txtChucDanh3.Text;
                        repT.CD4.Value = txtChucDanh4.Text;
                        repT.CD5.Value = txtChucDanh5.Text;
                        repT.Thoigian.Value = txtThoiGian.Text;
                        repT.Diadiem.Value = txtDiaDiem.Text;
                        repT.NoiDung.Value = txtNoiDung.Text;
                        repT.YKNX.Value = txtLyDo.Text;
                        //repT.KetThuc.Value = txtKetThuc.Text;

                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {

                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapdct, nhapd }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      //    id in _lid
                                      //join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                                      //join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      //join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                      //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                      //join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                      select new
                                      {
                                          dv.MaQD,
                                          dv.MaTam,
                                          nd.nhapdct.DonGiaCT,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          nd.nhapd.MaCC,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          dv.SoDK,
                                          dv.NuocSX,
                                          dv.HamLuong,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.nhapdct.VAT > 0) ? nd.nhapdct.DonGiaCT : (dv.DongY == 1 ? nd.nhapdct.DonGiaDY : nd.nhapdct.DonGia), // dv.DongY == 1 ? nhapdct.DonGiaDY : (( (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023") && nhapdct.VAT > 0) ?nhapdct.DonGiaCT : nhapdct.DonGia),

                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.MaQD,
                                         nd.MaTam,
                                         nd.MaCC,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = DungChung.Bien.MaBV == "12122" ? (nd.TenDV + nd.HamLuong) : nd.TenDV,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NuocSX,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.DonGiaCT,
                                         nd.SoLuongN,
                                         ThanhTienN = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.VAT > 0) ? (nd.DonGia * nd.SoLuongN) : nd.ThanhTienN,
                                     }).OrderBy(p => p.IDNhapct).ToList();

                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);

                            if (_lid.Count == 1 && (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001"))
                            {
                                //repT.celVAT_lbl.Text = "VAT: ";
                                if (q.Count > 0)
                                {
                                    double vat = (double)q.First().VAT;
                                    //repT.celVAT.Text = vat.ToString() + "%";
                                    if (vat > 0)
                                    {
                                        TT = TT + TT * (double)vat / 100;

                                    }
                                }
                            }
                            repT.Soct.Text = q.First().SoCT;
                            repT.NgayNhap.Text = q.First().NgayNhap.ToString();
                            string macc = q.First().MaCC.ToString();
                            var nhacc = (from cc in data.NhaCCs.Where(p => p.MaCC == macc)
                                         select new { cc.TenCC }).ToList();
                            if (nhacc.Count > 0) { repT.ncc.Text = nhacc.First().TenCC.ToString(); }

                            TT = Math.Round(TT, 0);
                            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
                                repT.colThanhTienTong.Text = TT.ToString("###,###");
                            else
                            {
                                string a = string.Format(DungChung.Bien.FormatString[1], TT);
                                repT.colThanhTienTong.Text = a;//TT.ToString("###,###.00");
                            }

                            if (tungay.ToString().Substring(0, 10) == denngay.ToString().Substring(0, 10))
                            {
                                repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                            }
                            else// if(tungay!=denngay)
                            {
                                repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                            }
                            if (chkIn.Checked == true)
                            {
                                repT.InCD.Value = ("Tổ trưởng tổ kiểm nhập").ToUpper();
                            }
                            if (chkIn.Checked == false)
                            {
                                repT.InCD.Value = ("Chủ tịch hội đồng").ToUpper();
                            }

                            if (DungChung.Bien.MaBV == "14018")
                            {
                                //repT.cel_SoLo.Text = "Lô sản xuất";
                                repT.DiaDanh.Value = "";
                            }

                            repT.TongTien.Value = TT;

                            repT.DataSource = q.ToList();
                            repT.BindingData();
                            repT.CreateDocument();
                            frm.prcIN.PrintingSystem = repT.PrintingSystem;

                            frm.ShowDialog();
                            if (File.Exists("TextBBKiemNhap.txt"))
                            {
                                File.Delete("TextBBKiemNhap.txt");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                        #endregion
                    }
                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        BaoCao.Rep_BBKIemNhap_01071 repT = new BaoCao.Rep_BBKIemNhap_01071();
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {

                            dscb += i + ".Họ và tên : " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ".Họ và tên : " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ".Họ và tên : " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ".Họ và tên : " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ".Họ và tên : " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ".Họ và tên : " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ".Họ và tên : " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh2.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh3.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh4.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh5.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh6.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += "Chức vụ: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        repT.TV1goi.Value = dscb.ToString();
                        repT.TV2goi.Value = dscd.ToString();
                        repT.TV1.Value = lupTV1.Text;
                        repT.BenhVien.Value = DungChung.Bien.TenCQ;
                        repT.TV2.Value = lupTV2.Text;
                        repT.TV3.Value = lupTV3.Text;
                        repT.TV4.Value = lupTV4.Text;
                        repT.TV5.Value = lupTV5.Text;
                        repT.TV6.Value = lupTV6.Text;
                        repT.TV7.Value = lupTV7.Text;
                        repT.CD1.Value = txtChucDanh1.Text;
                        repT.CD2.Value = txtChucDanh2.Text;
                        repT.CD3.Value = txtChucDanh3.Text;
                        repT.CD4.Value = txtChucDanh4.Text;
                        repT.CD5.Value = txtChucDanh5.Text;
                        repT.CB1.Value = cbocv1.Text;
                        repT.CB2.Value = cbocv2.Text;
                        repT.CB3.Value = cbocv3.Text;
                        repT.CB4.Value = cbocv4.Text;
                        repT.CB5.Value = cbocv5.Text;
                        repT.CB6.Value = cbocv6.Text;
                        repT.CB7.Value = cbocv7.Text;
                        repT.ThoiGian.Value = "Ngày Lập:" + txtThoiGian.Text;
                        repT.NoiDung.Value = txtNoiDung.Text;
                        repT.YKNX.Value = txtLyDo.Text;
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {

                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapdct, nhapd }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      select new
                                      {
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          dv.SoDK,
                                          dv.NuocSX,
                                          dv.HamLuong,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.DonGiaCT,
                                          nd.nhapdct.SoLo,
                                          DonGia = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.nhapdct.VAT > 0) ? nd.nhapdct.DonGia : (dv.DongY == 1 ? nd.nhapdct.DonGiaDY : nd.nhapdct.DonGia), // dv.DongY == 1 ? nhapdct.DonGiaDY : (( (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023") && nhapdct.VAT > 0) ?nhapdct.DonGiaCT : nhapdct.DonGia),                            
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.MaQD,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.DonGiaCT,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = DungChung.Bien.MaBV == "12122" ? (nd.TenDV + nd.HamLuong) : nd.TenDV,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NuocSX,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.VAT > 0) ? (nd.DonGia * nd.SoLuongN) : nd.ThanhTienN,
                                     }).OrderBy(p => p.IDNhapct).ToList();
                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);
                            TT = Math.Round(TT, 0);
                            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
                                repT.colThanhTienTong.Text = TT.ToString("###,###");
                            else
                            {
                                string a = string.Format(DungChung.Bien.FormatString[1], TT);
                                repT.colThanhTienTong.Text = a;//TT.ToString("###,###.00");
                            }




                            repT.Soct.Value = q.First().SoCT;
                            repT.TongTien.Value = TT;
                            repT.BindingData();
                            repT.DataSource = q.ToList();
                            repT.CreateDocument();
                            frm.prcIN.PrintingSystem = repT.PrintingSystem;

                            frm.ShowDialog();
                            if (File.Exists("TextBBKiemNhap.txt"))
                            {
                                File.Delete("TextBBKiemNhap.txt");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                    }
                    else if (DungChung.Bien.MaBV == "24012")
                    {


                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {

                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapdct, nhapd }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      select new
                                      {
                                          dv.MaTam,
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          dv.SoDK,
                                          dv.NhaSX,
                                          dv.NuocSX,
                                          dv.HamLuong,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = nd.nhapdct.DonGiaCT,
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.MaTam,
                                         nd.MaQD,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = (DungChung.Bien.MaBV == "24012" ? nd.TenDV : nd.TenDV + nd.HamLuong),
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NhaSX,
                                         NuocSX = (DungChung.Bien.MaBV == "24012" ? nd.NhaSX + " - " + nd.NuocSX : nd.NuocSX),
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = nd.DonGia * nd.SoLuongN
                                     }).OrderBy(p => p.IDNhapct).ToList();
                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);
                            TT = Math.Round(TT, 0);
                            string a = string.Format(DungChung.Bien.FormatString[1], TT);

                            if (ckcMau2_24012.Checked)
                            {
                                BaoCao.rep_BbKiemNhap_24012_Mau2 repT = new BaoCao.rep_BbKiemNhap_24012_Mau2();
                                repT.txtHoiDongKyTen.Visible = false;
                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    repT.TieuDe.Value = ("Biên bản kiểm nhập thuốc, hóa chất, vật tư y tế tiêu hao, Y cụ").ToUpper();

                                }
                                else if (DungChung.Bien.MaBV == "24012")
                                {
                                    repT.TieuDe.Value = ("Biên bản kiểm nhập thuốc, vật tư y tế, hóa chất, sinh phẩm y tế").ToUpper();
                                }
                                else
                                {
                                    repT.TieuDe.Value = ("Biên bản kiểm nhập thuốc, hóa chất, vật tư y tế tiêu hao").ToUpper();
                                }
                                if ((cbocv1.Text).Length > 1)
                                {
                                    if (cbocv1.Text == "Trưởng khoa dược")
                                    {
                                        dscd2 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd2 += "- Thành viên" + "\n";
                                    }
                                    dscd += cbocv1.Text.Trim() + "\n";
                                }
                                if ((cbocv3.Text).Length > 1)
                                {
                                    if (cbocv3.Text == "Trưởng khoa dược")
                                    {
                                        dscd2 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd2 += "- Thành viên" + "\n";
                                    }
                                    dscd += cbocv3.Text.Trim() + "\n";
                                }
                                if ((cbocv5.Text).Length > 1)
                                {
                                    if (cbocv5.Text == "Trưởng khoa dược")
                                    {
                                        dscd2 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd2 += "- Thành viên" + "\n";
                                    }
                                    dscd += cbocv5.Text.Trim() + "\n";
                                }
                                if ((cbocv7.Text).Length > 1)
                                {
                                    if (cbocv7.Text == "Trưởng khoa dược")
                                    {
                                        dscd2 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd2 += "- Thành viên" + "\n";
                                    }
                                    dscd += cbocv7.Text.Trim() + "\n";
                                }
                                if ((cbocv2.Text).Length > 1)
                                {
                                    if (cbocv2.Text == "Trưởng khoa dược")
                                    {
                                        dscd3 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd3 += "- Thành viên" + "\n";
                                    }
                                    dscd1 += cbocv2.Text.Trim() + "\n";
                                }
                                if ((cbocv4.Text).Length > 1)
                                {
                                    if (cbocv4.Text == "Trưởng khoa dược")
                                    {
                                        dscd3 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd3 += "- Thành viên" + "\n";
                                    }
                                    dscd1 += cbocv4.Text.Trim() + "\n";
                                }
                                if ((cbocv6.Text).Length > 1)
                                {
                                    if (cbocv6.Text == "Trưởng khoa dược")
                                    {
                                        dscd3 += "- Chủ tịch HĐ" + "\n";
                                    }
                                    else
                                    {
                                        dscd3 += "- Thành viên" + "\n";
                                    }
                                    dscd1 += cbocv6.Text.Trim() + "\n";
                                }

                                if (lupTV11.Text.Length > 0)
                                {
                                    dscb += "1. " + lupTV11.Text + "\n";
                                }
                                if (lupTV33.Text.Length > 0)
                                {
                                    dscb += "3. " + lupTV33.Text + "\n";
                                }
                                if (lupTV55.Text.Length > 0)
                                {
                                    dscb += "5. " + lupTV55.Text + "\n";
                                }
                                if (lupTV77.Text.Length > 0)
                                {
                                    dscb += "7. " + lupTV77.Text + "\n";
                                }
                                if (lupTV22.Text.Length > 0)
                                {
                                    dscb1 += "2. " + lupTV22.Text + "\n";
                                }
                                if (lupTV44.Text.Length > 0)
                                {
                                    dscb1 += "4. " + lupTV44.Text + "\n";
                                }
                                if (lupTV66.Text.Length > 0)
                                {
                                    dscb1 += "6. " + lupTV66.Text + "\n";
                                }


                                repT.TV1goi.Value = dscd.ToString();
                                repT.TV2goi.Value = dscb.ToString();
                                repT.TV3goi.Value = dscb1.ToString();
                                repT.TV4goi.Value = dscd1.ToString();
                                repT.TV5goi.Value = dscd2.ToString();
                                repT.TV6goi.Value = dscd3.ToString();

                                repT.TV1.Value = lupTV11.Text;
                                repT.TV2.Value = lupTV22.Text;
                                repT.TV3.Value = lupTV33.Text;
                                repT.TV4.Value = lupTV44.Text;
                                repT.TV5.Value = lupTV55.Text;
                                repT.TV6.Value = lupTV66.Text;
                                repT.TV7.Value = lupTV77.Text;

                                repT.CVCB1.Value = cbocv1.Text;
                                repT.CVCB2.Value = cbocv2.Text;
                                repT.CVCB3.Value = cbocv3.Text;
                                repT.CVCB4.Value = cbocv4.Text;
                                repT.CVCB5.Value = cbocv5.Text;
                                repT.CVCB6.Value = cbocv6.Text;
                                repT.CVCB7.Value = cbocv7.Text;

                                //Thủ kho
                                if (cbocv1.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV11.Text;
                                }
                                if (cbocv2.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV22.Text;
                                }
                                if (cbocv3.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV33.Text;
                                }
                                if (cbocv4.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV44.Text;
                                }
                                if (cbocv5.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV55.Text;
                                }
                                if (cbocv6.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV66.Text;
                                }
                                if (cbocv7.Text == "Thủ kho")
                                {
                                    repT.TV1.Value = lupTV77.Text;
                                }

                                //Kế toán dược
                                if (cbocv1.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV11.Text;
                                }
                                if (cbocv2.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV22.Text;
                                }
                                if (cbocv3.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV33.Text;
                                }
                                if (cbocv4.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV44.Text;
                                }
                                if (cbocv5.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV55.Text;
                                }
                                if (cbocv6.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV66.Text;
                                }
                                if (cbocv7.Text == "Kế toán dược")
                                {
                                    repT.TV2.Value = lupTV77.Text;
                                }

                                //Trưởng phòng TC - KT
                                if (cbocv1.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV11.Text;
                                }
                                if (cbocv2.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV22.Text;
                                }
                                if (cbocv3.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV33.Text;
                                }
                                if (cbocv4.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV44.Text;
                                }
                                if (cbocv5.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV55.Text;
                                }
                                if (cbocv6.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV66.Text;
                                }
                                if (cbocv7.Text == "Trưởng phòng TC-KT")
                                {
                                    repT.TV3.Value = lupTV77.Text;
                                }

                                //Trưởng khoa dược
                                if (cbocv1.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV11.Text;
                                }
                                if (cbocv2.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV22.Text;
                                }
                                if (cbocv3.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV33.Text;
                                }
                                if (cbocv4.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV44.Text;
                                }
                                if (cbocv5.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV55.Text;
                                }
                                if (cbocv6.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV66.Text;
                                }
                                if (cbocv7.Text == "Trưởng khoa dược")
                                {
                                    repT.TV4.Value = lupTV77.Text;
                                }

                                //
                                if (cbocv1.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB1.Value = "Chủ tịch Hội đồng";
                                }
                                if (cbocv2.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB2.Value = "Chủ tịch Hội đồng";
                                }
                                if (cbocv3.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB3.Value = "Chủ tịch Hội đồng";
                                }
                                if (cbocv4.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB4.Value = "Chủ tịch Hội đồng";
                                }
                                if (cbocv5.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB5.Value = "Chủ tịch Hội đồng";
                                }
                                if (cbocv6.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB6.Value = "Chủ tịch Hội đồng";
                                }
                                if (cbocv7.Text == "Trưởng khoa dược")
                                {
                                    repT.CVCB7.Value = "Chủ tịch Hội đồng";
                                }

                                repT.CD1.Value = txtChucDanh1.Text;
                                repT.CD2.Value = txtChucDanh2.Text;
                                repT.CD3.Value = txtChucDanh3.Text;
                                repT.CD4.Value = txtChucDanh4.Text;
                                repT.CD5.Value = txtChucDanh5.Text;

                                repT.Kho.Value = "Tại kho: " + txtDiaDiem.Text;
                                repT.Thoigian.Value = txtThoiGian.Text;
                                repT.thoigian1.Value = "Hôm nay, " + thoigian;
                                repT.Diadiem.Value = txtDiaDiem.Text;
                                if (txtNoiDung.Text == "")
                                {

                                    repT.NoiDung.Value = "Đã tiến hành kiểm nhập, xem xét chất lượng, hạn dùng thuốc, vật tư y tế, hóa chất, sinh phẩm y tế";
                                }
                                else
                                {
                                    repT.NoiDung.Value = txtNoiDung.Text;
                                }
                                repT.YKNX.Value = txtLyDo.Text;
                                repT.KetThuc.Value = txtKetThuc.Text;
                                if (q.Count > 1)
                                {
                                    string c = q.Count < 10 ? "0" : "";
                                    repT.NoiDung.Value += "\nSố mặt hàng từ 01 đến " + c + q.Count;
                                }

                                repT.TongTien.Value = TT;
                                repT.DataSource = q.ToList();
                                repT.BindingData();
                                repT.CreateDocument();
                                frm.prcIN.PrintingSystem = repT.PrintingSystem;

                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.rep_BbKiemNhap_24012 repT = new BaoCao.rep_BbKiemNhap_24012();
                                repT.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;
                                if ((txtTV1goi.Text + lupTV11.Text).Length > 1)
                                {

                                    dscb += i + ". " + txtTV1goi.Text + " " + lupTV11.Text + "\n";
                                    i++;
                                    //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                                }
                                if ((txtTV2goi.Text + lupTV22.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV2goi.Text + " " + lupTV22.Text + "\n";
                                    i++;

                                }
                                if ((txtTV3goi.Text + lupTV33.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV3goi.Text + " " + lupTV33.Text + "\n";
                                    i++;
                                }
                                if ((txtTV4goi.Text + lupTV44.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV4goi.Text + " " + lupTV44.Text + "\n";
                                    i++;
                                }
                                if ((txtTV5goi.Text + lupTV55.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV5goi.Text + " " + lupTV55.Text + "\n";
                                    i++;
                                }
                                if ((txtTV6goi.Text + lupTV66.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV6goi.Text + " " + lupTV66.Text + "\n";
                                    i++;
                                }
                                if ((txtTV7goi.Text + lupTV77.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV7goi.Text + " " + lupTV77.Text + "\n";
                                    i++;
                                }
                                if ((txtChucDanh1.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                                }
                                if ((txtChucDanh2.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                                }
                                if ((txtChucDanh3.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                                }
                                if ((txtChucDanh4.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                                }
                                if ((txtChucDanh5.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                                }
                                if ((txtChucDanh6.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                                }
                                if ((txtChucDanh7.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                                }

                                if (cmbPL.Text != null)
                                    repT.TieuDe.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                                else
                                    repT.TieuDe.Value = ("Biên bản kiểm nhập ").ToUpper();
                                repT.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;
                                repT.TV1goi.Value = dscb.ToString();
                                repT.TV2goi.Value = dscd.ToString();
                                repT.TV1.Value = lupTV11.Text;
                                repT.TV2.Value = lupTV22.Text;
                                repT.TV3.Value = lupTV33.Text;
                                repT.TV4.Value = lupTV44.Text;
                                repT.TV5.Value = lupTV55.Text;
                                repT.TV6.Value = lupTV66.Text;
                                repT.TV7.Value = lupTV77.Text;
                                repT.CVCB1.Value = cbocv1.Text;
                                repT.CVCB2.Value = cbocv2.Text;
                                repT.CVCB3.Value = cbocv3.Text;
                                repT.CVCB4.Value = cbocv4.Text;
                                repT.CVCB5.Value = cbocv5.Text;
                                repT.CVCB6.Value = cbocv6.Text;
                                repT.CVCB7.Value = cbocv7.Text;
                                repT.CD1.Value = txtChucDanh1.Text;
                                repT.CD2.Value = txtChucDanh2.Text;
                                repT.CD3.Value = txtChucDanh3.Text;
                                repT.CD4.Value = txtChucDanh4.Text;
                                repT.CD5.Value = txtChucDanh5.Text;

                                repT.Thoigian.Value = txtThoiGian.Text;
                                repT.Diadiem.Value = txtDiaDiem.Text;
                                repT.NoiDung.Value = txtNoiDung.Text;
                                repT.YKNX.Value = txtLyDo.Text;
                                repT.KetThuc.Value = txtKetThuc.Text;


                                repT.colThanhTienTong.Text = a;
                                if (tungay.ToString().Substring(0, 10) == denngay.ToString().Substring(0, 10))
                                {
                                    repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                                }
                                else
                                {
                                    repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                                }
                                if (chkIn.Checked == true)
                                {
                                    repT.InCD.Value = ("Tổ trưởng tổ kiểm nhập").ToUpper();
                                }
                                if (chkIn.Checked == false)
                                {
                                    repT.InCD.Value = ("Chủ tịch hội đồng").ToUpper();
                                }

                                repT.TongTien.Value = TT;
                                repT.DataSource = q.ToList();
                                repT.BindingData();
                                repT.CreateDocument();
                                frm.prcIN.PrintingSystem = repT.PrintingSystem;

                                frm.ShowDialog();
                            }

                            if (File.Exists("TextBBKiemNhap.txt"))
                            {
                                File.Delete("TextBBKiemNhap.txt");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                    }
                    else if (DungChung.Bien.MaBV == "30009")
                    {
                        BaoCao.Rep_BBKiemNhap_30009 repT = new BaoCao.Rep_BBKiemNhap_30009(chucdanh);
                        //BaoCao.rep_BbKiemNhap rep = new BaoCao.rep_BbKiemNhap();
                        if (cmbPL.Text != null)
                            repT.TieuDe.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        else
                            repT.TieuDe.Value = ("Biên bản kiểm nhập ").ToUpper();
                        repT.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;

                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {

                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        repT.TV1goi.Value = dscb.ToString();
                        repT.TV2goi.Value = dscd.ToString();
                        repT.TV1.Value = lupTV1.Text;
                        repT.TV2.Value = lupTV2.Text;
                        repT.TV3.Value = lupTV3.Text;
                        repT.TV4.Value = lupTV4.Text;
                        repT.TV5.Value = lupTV5.Text;
                        repT.TV6.Value = lupTV6.Text;
                        repT.TV7.Value = lupTV7.Text;
                        repT.CD1.Value = txtChucDanh1.Text;
                        repT.CD2.Value = txtChucDanh2.Text;
                        repT.CD3.Value = txtChucDanh3.Text;
                        repT.CD4.Value = txtChucDanh4.Text;
                        repT.CD5.Value = txtChucDanh5.Text;
                        repT.Thoigian.Value = txtThoiGian.Text;
                        repT.Diadiem.Value = txtDiaDiem.Text;
                        repT.NoiDung.Value = txtNoiDung.Text;
                        repT.YKNX.Value = txtLyDo.Text;
                        repT.KetThuc.Value = txtKetThuc.Text;

                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {

                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapdct, nhapd }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      //    id in _lid
                                      //join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                                      //join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      //join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                      //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                      //join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                      select new
                                      {
                                          dv.QCPC,
                                          dv.MaTam,
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.SoDK,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          dv.NhaSX,
                                          dv.NuocSX,
                                          dv.HamLuong,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.nhapdct.VAT > 0) ? nd.nhapdct.DonGiaCT : ((DungChung.Bien.MaBV == "30007") && dv.DongY == 1 ? nd.nhapdct.DonGiaDY : nd.nhapdct.DonGia), // dv.DongY == 1 ? nhapdct.DonGiaDY : (( (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023") && nhapdct.VAT > 0) ?nhapdct.DonGiaCT : nhapdct.DonGia),
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.QCPC,
                                         nd.MaTam,
                                         nd.MaQD,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = DungChung.Bien.MaBV == "12122" ? (nd.TenDV + nd.HamLuong) : nd.TenDV,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NuocSX,
                                         nd.NhaSX,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.VAT > 0) ? nd.DonGia * nd.SoLuongN : ((DungChung.Bien.MaBV == "30007") && nd.DongY == 1 ? nd.DonGia * nd.SoLuongN : nd.ThanhTienN)
                                     }).OrderBy(p => p.IDNhapct).ToList();


                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);

                            if ((_lid.Count == 1 && (DungChung.Bien.MaBV == "27023")) || (_lid.Count == 1 && (DungChung.Bien.MaBV == "27022")))
                            {
                                repT.xrTableCell14.Text = "VAT: ";
                                if (q.Count > 0)
                                {
                                    double vat = (double)q.First().VAT;
                                    repT.xrTableCell16.Text = vat.ToString() + "%";
                                    if (vat > 0)
                                    {
                                        TT = TT + TT * (double)vat / 100;

                                    }
                                }
                            }
                            TT = Math.Round(TT, 0);
                            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
                                repT.colThanhTienTong.Text = TT.ToString("###,###");
                            else
                            {
                                string a = string.Format(DungChung.Bien.FormatString[1], TT);
                                repT.colThanhTienTong.Text = a;//TT.ToString("###,###.00");
                            }

                            if (tungay.ToString().Substring(0, 10) == denngay.ToString().Substring(0, 10))
                            {
                                repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                            }
                            else// if(tungay!=denngay)
                            {
                                repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                            }
                            if (chkIn.Checked == true)
                            {
                                repT.InCD.Value = ("Tổ trưởng tổ kiểm nhập").ToUpper();
                            }
                            if (chkIn.Checked == false)
                            {
                                repT.InCD.Value = ("Chủ tịch hội đồng").ToUpper();
                            }

                            if (DungChung.Bien.MaBV == "14018")
                            {
                                //repT.cel_SoLo.Text = "Lô sản xuất";
                                repT.DiaDanh.Value = "";
                            }

                            repT.TongTien.Value = TT;
                            repT.DataSource = q.ToList();
                            repT.BindingData();
                            repT.CreateDocument();
                            frm.prcIN.PrintingSystem = repT.PrintingSystem;

                            frm.ShowDialog();
                            if (File.Exists("TextBBKiemNhap.txt"))
                            {
                                File.Delete("TextBBKiemNhap.txt");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");

                        }
                    }
                    else
                    {
                        #region bv khác
                        BaoCao.rep_BbKiemNhap repT = new BaoCao.rep_BbKiemNhap(chucdanh);
                        //BaoCao.rep_BbKiemNhap rep = new BaoCao.rep_BbKiemNhap();
                        if (cmbPL.Text != null)
                            repT.TieuDe.Value = ("Biên bản kiểm nhập " + cmbPL.Text).ToUpper();
                        else
                            repT.TieuDe.Value = ("Biên bản kiểm nhập ").ToUpper();
                        repT.NoiDung1.Value = "III. Nội dung: Kiểm nhập " + cmbPL.Text;

                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                        {

                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh2.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh3.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh4.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh5.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh6.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh7.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        repT.TV1goi.Value = dscb.ToString();
                        repT.TV2goi.Value = dscd.ToString();
                        repT.TV1.Value = lupTV1.Text;
                        repT.TV2.Value = lupTV2.Text;
                        repT.TV3.Value = lupTV3.Text;
                        repT.TV4.Value = lupTV4.Text;
                        repT.TV5.Value = lupTV5.Text;
                        repT.TV6.Value = lupTV6.Text;
                        repT.TV7.Value = lupTV7.Text;
                        repT.CD1.Value = txtChucDanh1.Text;
                        repT.CD2.Value = txtChucDanh2.Text;
                        repT.CD3.Value = txtChucDanh3.Text;
                        repT.CD4.Value = txtChucDanh4.Text;
                        repT.CD5.Value = txtChucDanh5.Text;
                        repT.Thoigian.Value = txtThoiGian.Text;
                        repT.Diadiem.Value = txtDiaDiem.Text;
                        repT.NoiDung.Value = txtNoiDung.Text;
                        repT.YKNX.Value = txtLyDo.Text;
                        repT.KetThuc.Value = txtKetThuc.Text;

                        // lấy id chọn
                        _lid.Clear();
                        for (int k = 0; k < grvDSCT.RowCount; k++)
                        {
                            if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                            {
                                _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                            }
                        }
                        if (_lid.Count > 0)
                        {

                            var _lnhap = (from nhapd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                          select new { nhapdct, nhapd }).ToList();
                            var q0 = (from nd in _lnhap
                                      join dv in _ldichvu on nd.nhapdct.MaDV equals dv.MaDV
                                      join id in _lid on nd.nhapd.IDNhap equals id.ID
                                      //    id in _lid
                                      //join nhapd in data.NhapDs.Where(p => p.PLoai == 1) on id.ID equals nhapd.IDNhap
                                      //join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      //join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                      //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                      //join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                      select new
                                      {
                                          dv.QCPC,
                                          dv.MaTam,
                                          dv.MaQD,
                                          nd.nhapd.NgayNhap,
                                          nd.nhapdct.IDNhapct,
                                          nd.nhapdct.IDNhap,
                                          nd.nhapdct.VAT,
                                          dv.TenNhom,
                                          dv.TenTN,
                                          nd.nhapd.SoCT,
                                          dv.TenDV,
                                          dv.SoDK,
                                          dv.DongY,
                                          nd.nhapdct.DonVi,
                                          dv.NhaSX,
                                          dv.NuocSX,
                                          dv.HamLuong,
                                          nd.nhapdct.HanDung,
                                          nd.nhapdct.SoLo,
                                          DonGia = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.nhapdct.VAT > 0) ? nd.nhapdct.DonGiaCT : ((DungChung.Bien.MaBV == "30007") && dv.DongY == 1 ? nd.nhapdct.DonGiaDY : nd.nhapdct.DonGia), // dv.DongY == 1 ? nhapdct.DonGiaDY : (( (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023") && nhapdct.VAT > 0) ?nhapdct.DonGiaCT : nhapdct.DonGia),
                                          nd.nhapdct.SoLuongN,
                                          nd.nhapdct.ThanhTienN
                                      }).OrderBy(p => p.IDNhapct).ToList();

                            var q = (from nd in q0
                                     select new
                                     {
                                         nd.QCPC,
                                         nd.MaTam,
                                         nd.MaQD,
                                         nd.NgayNhap,
                                         nd.IDNhapct,
                                         nd.IDNhap,
                                         nd.VAT,
                                         nd.TenNhom,
                                         nd.TenTN,
                                         nd.SoCT,
                                         TenDV = DungChung.Bien.MaBV == "12122" ? (nd.TenDV + nd.HamLuong) : nd.TenDV,
                                         nd.DongY,
                                         nd.DonVi,
                                         nd.SoDK,
                                         nd.NuocSX,
                                         nd.NhaSX,
                                         nd.HanDung,
                                         nd.SoLo,
                                         DonGia = nd.DonGia,
                                         nd.SoLuongN,
                                         ThanhTienN = ((DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") && nd.VAT > 0) ? nd.DonGia * nd.SoLuongN : ((DungChung.Bien.MaBV == "30007") && nd.DongY == 1 ? nd.DonGia * nd.SoLuongN : nd.ThanhTienN)
                                     }).OrderBy(p => p.IDNhapct).ToList();


                            double TT = 0;
                            TT = q.Sum(p => p.ThanhTienN);

                            if ((_lid.Count == 1 && (DungChung.Bien.MaBV == "27023")) || (_lid.Count == 1 && (DungChung.Bien.MaBV == "27022")))
                            {
                                repT.xrTableCell14.Text = "VAT: ";
                                if (q.Count > 0)
                                {
                                    double vat = (double)q.First().VAT;
                                    repT.xrTableCell16.Text = vat.ToString() + "%";
                                    if (vat > 0)
                                    {
                                        TT = TT + TT * (double)vat / 100;

                                    }
                                }
                            }
                            TT = Math.Round(TT, 0);
                            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001")
                                repT.colThanhTienTong.Text = TT.ToString("###,###");
                            else
                            {
                                string a = string.Format(DungChung.Bien.FormatString[1], TT);
                                repT.colThanhTienTong.Text = a;//TT.ToString("###,###.00");
                            }

                            if (tungay.ToString().Substring(0, 10) == denngay.ToString().Substring(0, 10))
                            {
                                repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                            }
                            else// if(tungay!=denngay)
                            {
                                repT.DiaDanh.Value = DungChung.Bien.DiaDanh + ", Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                            }
                            if (chkIn.Checked == true)
                            {
                                repT.InCD.Value = ("Tổ trưởng tổ kiểm nhập").ToUpper();
                            }
                            if (chkIn.Checked == false)
                            {
                                repT.InCD.Value = ("Chủ tịch hội đồng").ToUpper();
                            }

                            if (DungChung.Bien.MaBV == "14018")
                            {
                                //repT.cel_SoLo.Text = "Lô sản xuất";
                                repT.DiaDanh.Value = "";
                            }

                            repT.TongTien.Value = TT;
                            repT.DataSource = q.ToList();
                            repT.BindingData();
                            repT.CreateDocument();
                            frm.prcIN.PrintingSystem = repT.PrintingSystem;

                            frm.ShowDialog();
                            if (File.Exists("TextBBKiemNhap.txt"))
                            {
                                File.Delete("TextBBKiemNhap.txt");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Chưa chọn chứng từ");
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.Message);
            }
        }

        #region in phiếu nhập 20001
        string _macbIn = "";
        string _tencbIn = "";
        private void PassData(string maCB, string tenCB)
        {
            _macbIn = maCB;
            _tencbIn = tenCB;

        }
        #endregion
        #region class DsChungTu
        public class DsChungTu
        {
            public int iddon;
            public string soctl;
            public string ngaynhap;
            public string ghichu;
            public bool check;
            public int IDNhap
            {
                set { iddon = value; }
                get { return iddon; }
            }
            public string NgayNhap
            {
                set { ngaynhap = value; }
                get { return ngaynhap; }
            }
            public string SoCT
            {
                set { soctl = value; }
                get { return soctl; }
            }
            public string GhiChu
            {
                set { ghichu = value; }
                get { return ghichu; }
            }
            public bool Check
            {
                set { check = value; }
                get { return check; }
            }
            private bool Chon;

            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        #endregion
        #region class KHO
        private class KHO
        {
            private int MaKP;
            private string TenKP;
            //   private string TenRG;
            public int makp
            {
                set { MaKP = value; }
                get { return MaKP; }
            }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            //public string tenrg
            //{ set { TenRG = value; } get { return TenRG; } }
        }
        #endregion
        List<KHO> _lkho = new List<KHO>();
        List<DsChungTu> _lDSct = new List<DsChungTu>();
        private void frm_BbKiemNhap_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "27023")
            {
                this.cmbPL.Properties.Items.AddRange(new object[] {
                "Vật tư hành chính"});
            }    
            if (DungChung.Bien.MaBV == "24012")
            {
                ckcMau2_24012.Visible = true;
                cbocv1.Properties.Items.RemoveAt(0);
                cbocv1.Properties.Items.RemoveAt(1);
                cbocv1.Properties.Items.RemoveAt(2);
                cbocv2.Properties.Items.RemoveAt(0);
                cbocv2.Properties.Items.RemoveAt(1);
                cbocv2.Properties.Items.RemoveAt(2);
                cbocv3.Properties.Items.RemoveAt(0);
                cbocv3.Properties.Items.RemoveAt(1);
                cbocv3.Properties.Items.RemoveAt(2);
                cbocv4.Properties.Items.RemoveAt(0);
                cbocv4.Properties.Items.RemoveAt(1);
                cbocv4.Properties.Items.RemoveAt(2);
                cbocv5.Properties.Items.RemoveAt(0);
                cbocv5.Properties.Items.RemoveAt(1);
                cbocv5.Properties.Items.RemoveAt(2);
                cbocv6.Properties.Items.RemoveAt(0);
                cbocv6.Properties.Items.RemoveAt(1);
                cbocv6.Properties.Items.RemoveAt(2);
                cbocv7.Properties.Items.RemoveAt(0);
                cbocv7.Properties.Items.RemoveAt(1);
                cbocv7.Properties.Items.RemoveAt(2);
                lupTV11.Visible = true;
                lupTV22.Visible = true;
                lupTV33.Visible = true;
                lupTV44.Visible = true;
                lupTV55.Visible = true;
                lupTV66.Visible = true;
                lupTV77.Visible = true;
                txtTV1goi.Visible = false;
                txtTV2goi.Visible = false;
                txtTV3goi.Visible = false;
                txtTV4goi.Visible = false;
                txtTV5goi.Visible = false;
                txtTV6goi.Visible = false;
                txtTV7goi.Visible = false;
                txtChucDanh1.Visible = false;
                txtChucDanh2.Visible = false;
                txtChucDanh3.Visible = false;
                txtChucDanh4.Visible = false;
                txtChucDanh5.Visible = false;
                txtChucDanh6.Visible = false;
                txtChucDanh7.Visible = false;
                cbocv1.Location = new Point(558, 17);
                cbocv1.Size = new Size(325, 22);
                cbocv2.Location = new Point(558, 45);
                cbocv2.Size = new Size(325, 22);
                cbocv3.Location = new Point(558, 73);
                cbocv3.Size = new Size(325, 22);
                cbocv4.Location = new Point(558, 101);
                cbocv4.Size = new Size(325, 22);
                cbocv5.Location = new Point(558, 129);
                cbocv5.Size = new Size(325, 22);
                cbocv6.Location = new Point(558, 157);
                cbocv6.Size = new Size(325, 22);
                cbocv7.Location = new Point(558, 185);
                cbocv7.Size = new Size(325, 22);
            }
            if (DungChung.Bien.MaBV == "14018")
            {
                txtLyDo.Text = "Bên tiếp nhận nhận xét như sau:" + Environment.NewLine + "1. Hàng được giao hoàn toàn phù hợp với hợp đồng đã ký." + Environment.NewLine + "2. Số hàng giao trên được Bệnh viện Phục hồi chức năng chấp nhận hoàn toàn";
            }
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now.AddDays(-1);
            lupDenNgay.DateTime = System.DateTime.Now;
            DateTime _ng = System.DateTime.Now;
            var kd = (from khoa in data.KPhongs.Where(p => p.PLoai == "Khoa dược") select new { khoa.TenKP, khoa.MaKP }).ToList();
            if (kd.Count() > 0)
            {
                KHO them1 = new KHO();
                them1.makp = 0;
                them1.tenkp = "Tất cả";
                _lkho.Add(them1);
                foreach (var a in kd)
                {
                    KHO themmoi = new KHO();
                    themmoi.makp = a.MaKP;
                    themmoi.tenkp = a.TenKP;
                    _lkho.Add(themmoi);
                }

                lupKho.Properties.DataSource = _lkho.ToList();
            }
            var dsid = data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= _ng && p.NgayNhap <= _ng).OrderByDescending(p => p.IDNhap).OrderByDescending(p => p.GhiChu).ToList();           
            if (dsid.Count > 0)
            {
                DsChungTu themmoi1 = new DsChungTu();
                themmoi1.SoCT = "";
                themmoi1.IDNhap = 0;
                themmoi1.NgayNhap = "";
                themmoi1.GhiChu =  "Chọn tất cả"  ;
                themmoi1.chon = true;
                _lDSct.Add(themmoi1);
                foreach (var a in dsid)
                {
                    DsChungTu ds = new DsChungTu();
                    ds.IDNhap = a.IDNhap;
                    ds.SoCT = a.SoCT;
                    ds.GhiChu = a.GhiChu;
                    ds.NgayNhap = a.NgayNhap.ToString();
                    //if (a.IDNhap == _id)
                    //    ds.Check = true;
                    //else
                    //    ds.Check = false;
                    ds.chon = true;

                    _lDSct.Add(ds);
                }
            }
            grcDSCT.DataSource = _lDSct.ToList();
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in data.CanBoes
                       select cb).ToList();
            _lcanbo.Add(new CanBo { TenCB = " ", MaCB = "" }); //
            _lcanbo.OrderBy(p => p.TenCB);

            if (_lcanbo.Count > 0)
            {
                lupTV1.Properties.DataSource = _lcanbo;
                lupTV2.Properties.DataSource = _lcanbo;
                lupTV3.Properties.DataSource = _lcanbo;
                lupTV4.Properties.DataSource = _lcanbo;
                lupTV5.Properties.DataSource = _lcanbo;
                lupTV6.Properties.DataSource = _lcanbo;
                lupTV7.Properties.DataSource = _lcanbo;
            }


            if (!File.Exists("TextBBKiemNhap.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextBBKiemNhap.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine(txtTV1goi.Text);
                sWriter.WriteLine(lupTV1.Text);
                sWriter.WriteLine(txtChucDanh1.Text);


                sWriter.WriteLine(txtTV2goi.Text);
                sWriter.WriteLine(lupTV2.Text);
                sWriter.WriteLine(txtChucDanh2.Text);


                sWriter.WriteLine(txtTV3goi.Text);
                sWriter.WriteLine(lupTV3.Text);
                sWriter.WriteLine(txtChucDanh3.Text);


                sWriter.WriteLine(txtTV4goi.Text);
                sWriter.WriteLine(lupTV4.Text);
                sWriter.WriteLine(txtChucDanh4.Text);


                sWriter.WriteLine(txtTV5goi.Text);
                sWriter.WriteLine(lupTV5.Text);
                sWriter.WriteLine(txtChucDanh5.Text);


                sWriter.WriteLine(txtTV6goi.Text);
                sWriter.WriteLine(lupTV6.Text);
                sWriter.WriteLine(txtChucDanh6.Text);


                sWriter.WriteLine(txtTV7goi.Text);
                sWriter.WriteLine(lupTV7.Text);
                sWriter.WriteLine(txtChucDanh7.Text);


                sWriter.WriteLine(txtLyDo.Text);
                sWriter.WriteLine(txtKetThuc.Text);

                sWriter.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
                sWriter.Flush();
                sWriter.Flush();
                fs.Close();
            }
            

            string[] lines = File.ReadAllLines("TextBBKiemNhap.txt");
            if (lines[lines.Length - 1] == "1")
            {
                txtTV1goi.Text = lines[lines.Length - 24];
                lupTV1.SelectedText = lines[lines.Length - 23];
                txtChucDanh1.Text = lines[lines.Length - 22];
                txtTV2goi.Text = lines[lines.Length - 21];
                lupTV2.SelectedText = lines[lines.Length - 20];
                txtChucDanh2.Text = lines[lines.Length - 19];
                txtTV3goi.Text = lines[lines.Length - 18];
                lupTV3.SelectedText = lines[lines.Length - 17];
                txtChucDanh3.Text = lines[lines.Length - 16];
                txtTV4goi.Text = lines[lines.Length - 15];
                lupTV4.SelectedText = lines[lines.Length - 14];
                txtChucDanh4.Text = lines[lines.Length - 13];
                txtTV5goi.Text = lines[lines.Length - 12];
                lupTV5.SelectedText = lines[lines.Length - 11];
                txtChucDanh5.Text = lines[lines.Length - 10];
                txtTV6goi.Text = lines[lines.Length - 9];
                lupTV6.SelectedText = lines[lines.Length - 8];
                txtChucDanh6.Text = lines[lines.Length - 7];
                txtTV7goi.Text = lines[lines.Length - 6];
                lupTV7.SelectedText = lines[lines.Length - 5];
                txtChucDanh7.Text = lines[lines.Length - 4];
                //txtThoiGian.Text = lines[lines.Length - 6];
                //txtDiaDiem.Text = lines[lines.Length - 5];
                //txtNoiDung.Text = lines[lines.Length - 4];
                txtLyDo.Text = lines[lines.Length - 3];
                txtKetThuc.Text = lines[lines.Length - 2];
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (!File.Exists("TextBBKiemNhap.txt"))
            {
                FileStream fs = new FileStream("TextBBKiemNhap.txt", FileMode.Create);
                StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file
                writeFile.WriteLine(txtTV1goi.Text);
                writeFile.WriteLine(lupTV1.Text);
                writeFile.WriteLine(txtChucDanh1.Text);


                writeFile.WriteLine(txtTV2goi.Text);
                writeFile.WriteLine(lupTV2.Text);
                writeFile.WriteLine(txtChucDanh2.Text);


                writeFile.WriteLine(txtTV3goi.Text);
                writeFile.WriteLine(lupTV3.Text);
                writeFile.WriteLine(txtChucDanh3.Text);


                writeFile.WriteLine(txtTV4goi.Text);
                writeFile.WriteLine(lupTV4.Text);
                writeFile.WriteLine(txtChucDanh4.Text);


                writeFile.WriteLine(txtTV5goi.Text);
                writeFile.WriteLine(lupTV5.Text);
                writeFile.WriteLine(txtChucDanh5.Text);


                writeFile.WriteLine(txtTV6goi.Text);
                writeFile.WriteLine(lupTV6.Text);
                writeFile.WriteLine(txtChucDanh6.Text);


                writeFile.WriteLine(txtTV7goi.Text);
                writeFile.WriteLine(lupTV7.Text);
                writeFile.WriteLine(txtChucDanh7.Text);


                writeFile.WriteLine(txtLyDo.Text);
                writeFile.WriteLine(txtKetThuc.Text);

                writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
                writeFile.Flush();
            }
            this.Close();
        }
        int[] _dsct;
        private void grvDSCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            //int soid = 0;
            //if (e.Column.Name == "colCheck")
            //{
            //    for (int i = 0; i < grvDSCT.RowCount; i++)
            //    {
            //        if (grvDSCT.GetRowCellValue(i, colCheck).ToString() == "true" || grvDSCT.GetRowCellValue(i, colCheck).ToString() == "True")
            //        {
            //            soid++;
            //        }
            //    }
            //    if (soid > 5)
            //    {
            //        MessageBox.Show("Bạn không được chọn nhiều hơn 5 chứng từ");
            //    }
            //    else
            //    {
            //        _dsct = new int[5];
            //        for (int i = 0; i < 4; i++) {
            //            _dsct[i] = 0;
            //        }
            //        int j = 0;
            //        for (int i = 0; i < grvDSCT.RowCount; i++)
            //        {
            //            if ( (grvDSCT.GetRowCellValue(i, colCheck).ToString() == "true" || grvDSCT.GetRowCellValue(i, colCheck).ToString() == "True"))
            //            {
            //                if (grvDSCT.GetRowCellValue(i, colIDNhap) != null && grvDSCT.GetRowCellValue(i, colIDNhap).ToString() != "")
            //                {
            //                    _dsct[j] = Convert.ToInt32(grvDSCT.GetRowCellValue(i, colIDNhap));
            //                    j++;
            //                }
            //            }
            //        }
            //    }
            //    string soct = "";
            //    foreach (var a in _dsct)
            //    {
            //        soct += a + " - ";

            //    }
            //    txtID.Text = soct;
            //}
        }

        private void grvDSCT_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                if (grvDSCT.GetFocusedRowCellValue("GhiChu") != null)
                {
                    string Ten = grvDSCT.GetFocusedRowCellValue("GhiChu").ToString();
                    if (Ten == "Chọn tất cả")
                    {
                        if (_lDSct.First().Check == false)
                        {
                            foreach (var a in _lDSct)
                            {
                                a.Check = true;
                            }
                        }
                        else
                        {
                            foreach (var a in _lDSct)
                            {
                                a.Check = false;
                            }
                        }
                        grcDSCT.DataSource = "";
                        grcDSCT.DataSource = _lDSct.ToList();
                    }
                }
            }
        }   
        
        private void lupTV7_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV7.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh7.EditValue = "";
                txtTV7goi.EditValue = "";
            }
        }

        private void lupTV1_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV1.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh1.EditValue = "";
                txtTV1goi.EditValue = "";
            }
        }

        private void lupTV2_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV2.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh2.EditValue = "";
                txtTV2goi.EditValue = "";
            }
        }

        private void lupTV3_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV3.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh3.EditValue = "";
                txtTV3goi.EditValue = "";
            }
        }

        private void lupTV4_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV4.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh4.EditValue = "";
                txtTV4goi.EditValue = "";
            }
        }

        private void lupTV5_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV5.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh5.EditValue = "";
                txtTV5goi.EditValue = "";
            }
        }

        private void lupTV6_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV6.EditValue == "" && DungChung.Bien.MaBV != "24012")
            {
                txtChucDanh6.EditValue = "";
                txtTV6goi.EditValue = "";
            }
        }

        private void lupTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            _lDSct.Clear();
            if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
            {
                if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            DsChungTu themmoi1 = new DsChungTu();
                            themmoi1.SoCT = "";
                            themmoi1.IDNhap = 0;
                            themmoi1.NgayNhap = "";
                            themmoi1.GhiChu = "Chọn tất cả";
                            themmoi1.chon = true;
                            _lDSct.Add(themmoi1);
                            foreach (var a in qn)
                            {
                                DsChungTu themmoi = new DsChungTu();
                                themmoi.SoCT = a.SoCT;
                                themmoi.IDNhap = a.IDNhap;
                                themmoi.NgayNhap = a.NgayNhap.ToString();
                                themmoi.GhiChu = a.GhiChu;
                                themmoi.chon = true;
                                _lDSct.Add(themmoi);
                            }
                        }
                }
            }
            grcDSCT.DataSource = _lDSct.ToList();
        }

        private void lupDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            _lDSct.Clear();
            if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
            {
                if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            DsChungTu themmoi1 = new DsChungTu();
                            themmoi1.SoCT = "";
                            themmoi1.IDNhap = 0;
                            themmoi1.NgayNhap = "";
                            themmoi1.GhiChu = "Chọn tất cả";
                            themmoi1.chon = true;
                            _lDSct.Add(themmoi1);
                            foreach (var a in qn)
                            {
                                DsChungTu themmoi = new DsChungTu();
                                themmoi.SoCT = a.SoCT;
                                themmoi.IDNhap = a.IDNhap;
                                themmoi.NgayNhap = a.NgayNhap.ToString();
                                themmoi.GhiChu = a.GhiChu;
                                themmoi.chon = true;
                                _lDSct.Add(themmoi);
                            }
                        }
                }
            }
            grcDSCT.DataSource = _lDSct.ToList();
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            _lDSct.Clear();
            if (lupKho.EditValue == " " || lupKho.EditValue == null)
            {
                if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
                {
                    if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                    {
                        DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                        DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                        var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                                  select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                        if (qn.Count > 0)
                            if (qn.Count > 0)
                            {
                                DsChungTu themmoi1 = new DsChungTu();
                                themmoi1.SoCT = "";
                                themmoi1.IDNhap = 0;
                                themmoi1.NgayNhap = "";
                                themmoi1.GhiChu = "Chọn tất cả";
                                themmoi1.chon = true;
                                _lDSct.Add(themmoi1);
                                foreach (var a in qn)
                                {
                                    DsChungTu themmoi = new DsChungTu();
                                    themmoi.SoCT = a.SoCT;
                                    themmoi.IDNhap = a.IDNhap;
                                    themmoi.NgayNhap = a.NgayNhap.ToString();
                                    themmoi.GhiChu = a.GhiChu;
                                    themmoi.chon = true;
                                    _lDSct.Add(themmoi);
                                }
                            }
                    }
                }
            }
            else
            {
                int _makp = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
                {
                    if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                    {
                        DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                        DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                        var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn && p.MaKP == _makp).Where(p => p.PLoai == 1)
                                  select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                        if (qn.Count > 0)
                            if (qn.Count > 0)
                            {
                                DsChungTu themmoi1 = new DsChungTu();
                                themmoi1.SoCT = "";
                                themmoi1.IDNhap = 0;
                                themmoi1.NgayNhap = "";
                                themmoi1.GhiChu = "Chọn tất cả";
                                themmoi1.chon = true;
                                _lDSct.Add(themmoi1);
                                foreach (var a in qn)
                                {
                                    DsChungTu themmoi = new DsChungTu();
                                    themmoi.SoCT = a.SoCT;
                                    themmoi.IDNhap = a.IDNhap;
                                    themmoi.NgayNhap = a.NgayNhap.ToString();
                                    themmoi.GhiChu = a.GhiChu;
                                    themmoi.chon = true;
                                    _lDSct.Add(themmoi);
                                }
                            }
                    }
                }

            }
            grcDSCT.DataSource = _lDSct.ToList();
        }
        string thoigian = "";
        int _idn = 0;
        private void grvDSCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDSCT.GetFocusedRowCellValue(colIDNhap) != null && grvDSCT.GetFocusedRowCellValue(colIDNhap).ToString() != "")
            {
                _idn = Convert.ToInt32(grvDSCT.GetFocusedRowCellValue(colIDNhap));
                var q = data.NhapDs.Where(p => p.IDNhap == _idn).Select(p => new { p.NgayNhap, p.MaKP, p.SoCT, p.MaCC }).ToList();
                if (q.Count > 0)
                {
                    if (Convert.ToDateTime(q.First().NgayNhap) != null)
                    {
                        thoigian = "ngày " + q.First().NgayNhap.Value.Day + " tháng " + q.First().NgayNhap.Value.Month + " năm " + q.First().NgayNhap.Value.Year;
                        txtThoiGian.Text = q.First().NgayNhap.Value.Hour + " giờ " + q.First().NgayNhap.Value.Minute + " phút, ngày " + q.First().NgayNhap.Value.Day + " tháng " + q.First().NgayNhap.Value.Month + " năm " + q.First().NgayNhap.Value.Year;
                        txtKetThuc.Text = "Kết thúc hồi ....... giờ ....... phút, ngày " + q.First().NgayNhap.Value.Day + " tháng " + q.First().NgayNhap.Value.Month + " năm " + q.First().NgayNhap.Value.Year;
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            txtThoiGian.Text = "Bắc Giang, ngày " + q.First().NgayNhap.Value.Day + " tháng " + q.First().NgayNhap.Value.Month + " năm " + q.First().NgayNhap.Value.Year;
                        }
                    }
                    else
                    {
                        txtThoiGian.Text = " ...... giờ.......phút, ngày ..........tháng ............ năm .........";
                        txtKetThuc.Text = "Kết thúc hồi ....... giờ ....... phút, ngày ..........tháng ............ năm .........";
                    }
                    if(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                        {
                        txtNoiDung.Text = "Tiến hành kiểm nhập những mặt hàng sau, trong điều kiện bảo quản nhiệt độ.....độ ẩm.......";
                        txtLyDo.Text = "Chủng loại, số lượng đúng theo hóa đơn, chất lượng hàng hóa theo cảm quan tốt";
                        txtChucDanh1.Text = "Trưởng phòng TC-KT";
                        txtChucDanh2.Text = "Kế toán dược";
                        txtChucDanh3.Text = "Trưởng khoa dược";
                        txtChucDanh4.Text = "Thủ kho";
                        txtChucDanh5.Text = "Thư ký";
                    }
                    if (q.First().MaKP != null)
                    {
                        int _makp = Convert.ToInt32(q.First().MaKP);
                        var kp = data.KPhongs.Where(p => p.MaKP == _makp).Select(p => new { p.TenKP }).ToList();
                        if (kp.Count > 0 && kp.First().TenKP != null)
                        {
                            txtDiaDiem.Text = "Tại " + kp.First().TenKP;
                        }
                        else { txtDiaDiem.Text = "Tại ................................................................."; }
                    }
                    string _macc = ""; string _sohd = "";
                    if (q.First().MaCC != null)
                    {
                        _macc = q.First().MaCC.ToString();
                    }
                    var cc = data.NhaCCs.Where(p => p.MaCC == _macc).Select(p => new { p.TenCC }).ToList();

                    if (cc.Count > 0)
                    {
                        string theoHD = DungChung.Bien.MaBV == "24012" ? "Đã kiểm nhận các hóa đơn số: " : "Kiểm nhập theo hóa đơn số ";
                        if (!string.IsNullOrEmpty(q.First().SoCT))
                        {
                            string tencc = !string.IsNullOrEmpty(cc.First().TenCC) ? cc.First().TenCC.ToString().ToUpper() : "................................";
                            txtNoiDung.Text = theoHD + q.First().SoCT + " của: " + tencc;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(cc.First().TenCC))
                            {
                                txtNoiDung.Text = theoHD + "........... của " + cc.First().TenCC.ToString();
                            }
                            else
                            {
                                txtNoiDung.Text = theoHD + ".............của..................................";
                            }
                        }
                    }
                }
            }
        }

        private void cbocv2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbocv4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grcDSCT_Click(object sender, EventArgs e)
        {

        }

        private void cmbPL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "27022")
            {
                if (cmbPL.Text == "Vật tư y tế")
                {
                    DungChung.Bien.CheckinVTYT = true;
                }
                else
                {
                    DungChung.Bien.CheckinVTYT = false;
                }
            }
            
        }

        private void txtChucDanh1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}