using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace QLBV.FormThamSo
{
    public partial class frm_BCTH_NoiTru_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTH_NoiTru_30007()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);

            #region báo cáo tổng hợp nội trú
            //
            if (radBieu.SelectedIndex == 0)
            {
                var q1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1 && p.TuyenDuoi == 0)
                          join dtuong in data.DTBNs on bn.IDDTBN equals dtuong.IDDTBN
                          join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                          join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                          where rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden
                          select new { bn.MaBNhan, bn.Tuoi, bn.CapCuu, bn.IDDTBN, bn.NoiTru, bn.DTNT, rv.Status, rv.KetQua, rv.SoNgaydt, rv.MaKP, dtuong.DTBN1, kp.TenKP }).ToList();
                var q2 = (from a in q1
                          group a by new { a.MaKP, a.TenKP } into kq
                          select new
                          {
                              kq.Key.TenKP,
                              TSBNrv = kq.Count(),// tổng số bn ra viện
                              TSBNrv_ngaydt = kq.Sum(p => p.SoNgaydt),// tổng số ngày đt của bn ra viện
                              BNBHYT = kq.Where(p => p.DTBN1 == "BHYT").Count(), // tổng số bn bảo hiểm y tế
                              BNBHYT_ngaydt = kq.Where(p => p.DTBN1 == "BHYT").Sum(p => p.SoNgaydt),
                              BNVP = kq.Where(p => p.DTBN1 != "BHYT").Count(),// tổng số bệnh nhân viện phí
                              BNVP_ngaydt = kq.Where(p => p.DTBN1 != "BHYT").Sum(p => p.SoNgaydt),
                              BN_Khoi = kq.Where(p => p.KetQua == "Khỏi").Count(),
                              BN_Khoi_ngaydt = kq.Where(p => p.KetQua == "Khỏi").Sum(p => p.SoNgaydt),
                              BN_Dogiam = kq.Where(p => p.KetQua == "Đỡ|Giảm").Count(), // đỡ giảm
                              BN_Dogiam_ngaydt = kq.Where(p => p.KetQua == "Đỡ|Giảm").Sum(p => p.SoNgaydt),
                              BN_KhongThayDoi = kq.Where(p => p.KetQua == "Không T.đổi").Count(),
                              BN_KhongThayDoi_ngaydt = kq.Where(p => p.KetQua == "Không T.đổi").Sum(p => p.SoNgaydt),
                              BN_NangHon = kq.Where(p => p.KetQua == "Nặng hơn").Count(),
                              BN_NangHon_ngaydt = kq.Where(p => p.KetQua == "Nặng hơn").Sum(p => p.SoNgaydt),
                              BN_TuVong = kq.Where(p => p.KetQua == "Tử vong").Count(),
                              BN_TuVong_ngaydt = kq.Where(p => p.KetQua == "Tử vong").Sum(p => p.SoNgaydt),
                              BN6 = kq.Where(p => p.Tuoi < 6 || p.Tuoi == null).Count(),
                              BN15 = kq.Where(p => p.Tuoi < 15 || p.Tuoi == null).Count(),
                              BN60 = kq.Where(p => p.Tuoi >= 60).Count(),
                              BNChuyenV = kq.Where(p => p.Status == 1).Count(),
                              BNCapCuu = kq.Where(p => p.CapCuu == 1).Count()

                          }).ToList();
                #region xuất excel
                string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                string[] _tieude = { "Tên khoa", "TS BN ra viện", "TS ngày đt", "BN có BHYT", "Ngày điều trị BHYT", "BN viện phí", "Ngày điều trị BN VP", "BN đt khỏi", "Ngày đt BN khỏi", "BN đt đỡ giảm", "Ngày đt BN đỡ giảm", "BN đt không thay đổi", "Ngày đt bn KTĐ", "BN nặng", "Ngày đt BN nặng", "BN tử vong", "BN <6 tuổi", "BN < 15 tuổi", "BN >= 60 tuổi", "BN chuyển v", "BN cấp cứu"};
                string _filePath = "D:\\BCTH_NoiTru_30007.xls";
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[q2.Count + 1, 21];
                for (int i = 0; i < 21; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in q2)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = r.TenKP;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TSBNrv;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.TSBNrv_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.BNBHYT;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.BNBHYT_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.BNVP;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.BNVP_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.BN_Khoi;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.BN_Khoi_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.BN_Dogiam;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.BN_Dogiam_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.BN_KhongThayDoi;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.BN_KhongThayDoi_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.BN_NangHon;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.BN_NangHon_ngaydt;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.BN_TuVong;
                    DungChung.Bien.MangHaiChieu[num, 16] = r.BN6;
                    DungChung.Bien.MangHaiChieu[num, 17] = r.BN15;
                    DungChung.Bien.MangHaiChieu[num, 18] = r.BN60;
                    DungChung.Bien.MangHaiChieu[num, 19] = r.BNChuyenV;
                    DungChung.Bien.MangHaiChieu[num, 20] = r.BNCapCuu; 
                    num++;
                }
                #endregion
                BaoCao.rep_BCTH_NoiTru_30007 rep = new BaoCao.rep_BCTH_NoiTru_30007();
                if (txtNgayThang.Text != "")
                    rep.lab_tungaydenngay.Text = txtNgayThang.Text;
                else
                    rep.lab_tungaydenngay.Text = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupngayden.DateTime.ToShortDateString();

                double sothang = Math.Round((((ngayden - ngaytu).TotalDays + 15) / 30), 0);

                if (txtTieude.Text != "")
                    rep.lab_Tieude.Text = txtTieude.Text.ToUpper();
                else
                    rep.lab_Tieude.Text = "BÁO CÁO " + sothang + " THÁNG NĂM " + ngayden.Year.ToString();
                rep.lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.cel_NLB.Text = DungChung.Bien.NguoiLapBieu;
                rep.cel_gd.Text = DungChung.Bien.GiamDoc;
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                rep.DataSource = q2;
                rep.BindingData();
                rep.CreateDocument();                
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region báo cáo tổng hợp khám bệnh - cũ
            else if (radBieu.SelectedIndex == 1 && radioGroup1.SelectedIndex == 0)
            {
                #region Lấy dữ liệu chung
                var q1 = (from bn in data.BenhNhans
                          join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                          join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                          into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new { bn.MaBNhan, bn.Tuoi, dtbn.DTBN1, QuocTich = kq1 == null ? "" : kq1.NgoaiKieu }).ToList();
                var q2 = (from rv in data.RaViens
                          join vp in data.VienPhis on rv.MaBNhan equals vp.MaBNhan
                          join ck in data.ChuyenKhoas on rv.MaCK equals ck.MaCK
                          where rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden
                          select new { rv.MaBNhan, rv.Status, ck.TenCK, rv.MaBVC }).ToList();
                var q3 = (from rv in q2
                          join bv in data.BenhViens on rv.MaBVC equals bv.MaBV into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new { rv.MaBNhan, rv.TenCK, ChuyenTuyenTren = (rv.Status == null || rv.Status != 1 || kq1 == null) ? 0 : GetChuyenTuyenTren(kq1.TuyenBV) }).ToList();

                var q4 = (from bn in q1
                          join ck in q3 on bn.MaBNhan equals ck.MaBNhan
                          group new { ck, bn } by ck.TenCK into kq
                          select new
                          {
                              TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                              TS = kq.Count(),
                              TSTructiep = kq.Where(p => p.bn.DTBN1 != "BHYT").Count(),
                              TSBHYT = kq.Where(p => p.bn.DTBN1 == "BHYT").Count(),
                              TS6 = kq.Where(p => p.bn.Tuoi < 6).Count(),
                              TSBHYT6 = kq.Where(p => p.bn.DTBN1 == "BHYT").Where(p => p.bn.Tuoi < 6).Count(),
                              TSTructiep6 = kq.Where(p => p.bn.DTBN1 != "BHYT").Where(p => p.bn.Tuoi < 6).Count(),
                              TS60 = kq.Where(p => p.bn.Tuoi >= 60).Count(),
                              TSBHYT60 = kq.Where(p => p.bn.DTBN1 == "BHYT").Where(p => p.bn.Tuoi >= 60).Count(),
                              TSTructiep60 = kq.Where(p => p.bn.DTBN1 != "BHYT").Where(p => p.bn.Tuoi >= 60).Count(),
                              TSNuocNgoai = kq.Where(p => p.bn.QuocTich != null && p.bn.QuocTich != "" && p.bn.QuocTich.Trim().ToLower() != "việt nam").Count(),
                              TSChuyenTuyen = kq.Sum(p=>p.ck.ChuyenTuyenTren) //kq.Where(p => p.ck.Status == 1).Count()
                          }).ToList();
                #endregion
                List<string> lkhoa = q4.Select(p => p.TenCK).Distinct().ToList();// danh sách chuyên khoa
                string a = String.Join(",", lkhoa.Skip(10).ToList());
                if(lkhoa.Count>10)
                MessageBox.Show("Số chuyên khoa nhiều hơn 10, các khoa không được hiển thị dữ liệu: " + a);
                List<KbKhoa> _list = new List<KbKhoa>();
                Type _type = Type.GetType("Namespace.FormThamSo.frm_BCTH_NoiTru_30007.KbKhoa");
                string propertyName = "Khoa";
                int count = lkhoa.Count;
                int? value = 0;
                #region đổ dữ liệu vào list
                for (int k = 1; k < 12; k++)// k là số dòng
                {
                    KbKhoa moi = new KbKhoa();
                    int? tong = 0;

                    for (int i = 1; i <= count; i++)
                    {
                        propertyName = "Khoa" + i.ToString();  // thuộc tính của đối tượng   KbKhoa                   
                        var propertyInfo = moi.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        if( k == 1)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p =>p.TS);
                        else if(k==2)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep);
                        else if (k == 3)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT);
                        else if (k == 4)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS6);
                        else if (k == 5)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT6);
                        else if (k == 6)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep6);
                        else if (k == 7)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS60);
                        else if (k == 8)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT60);
                        else if (k == 9)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep60);
                        else if (k == 10)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSNuocNgoai);
                        else if (k == 11)
                            value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSChuyenTuyen);

                        propertyInfo.SetValue(moi, value, null);
                        tong += value;
                        if (i == 10)
                            break;
                    }
                    #region hiển thị số thứ tự và nội dung
                    switch (k)
                    {
                        case 1:
                            moi.Stt = "1";
                            moi.Nd = "Tổng số lượt khám bệnh (= 1a + 1b)";
                            break;
                        case 2:
                            moi.Stt = "1a";
                            moi.Nd = "TS lượt khám bệnh thu phí trực tiếp";
                            break;
                        case 3:
                            moi.Stt = "1b";
                            moi.Nd = "TS lượt KB cho người được BHYT chi trả (tất cả các đối tượng có BHYT)";
                            break;
                        case 4:
                            moi.Stt = "2";
                            moi.Nd = "Tổng số khám chữa bệnh trẻ em dưới 6 tuổi (= 2a +2b)";
                            break;
                        case 5:
                            moi.Stt = "2a";
                            moi.Nd = "Số lượt khám bệnh cho trẻ em < 6 tuổi có thẻ BHYT";
                            break;
                        case 6:
                            moi.Stt = "2b";
                            moi.Nd = "Số lượt khám bệnh cho trẻ em < 6 tuổi thu phí";
                            break;
                        case 7:
                            moi.Stt = "3";
                            moi.Nd = "Tổng số khám cho người bệnh cao tuổi";
                            break;
                        case 8:
                            moi.Stt = "3a";
                            moi.Nd = "Số lượt khám bệnh cho người cao tuổi có thẻ BHYT";
                            break;
                        case 9:
                            moi.Stt = "3b";
                            moi.Nd = "Số lượt khám bệnh cho người cao tuổi thu phí trực tiếp";
                            break;
                        case 10:
                            moi.Stt = "4";
                            moi.Nd = "Tổng số lượt khám bệnh cho người nước ngoài";
                            break;
                        case 11:
                            moi.Stt = "5";
                            moi.Nd = "Tổng số lượt chuyển khám bv tuyến trên";
                            break;
                    }
                    #endregion
                    moi.Tong = tong;
                    _list.Add(moi);
                }
                #endregion
                #region xuất excel
                string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@"};               
                string _filePath = "D:\\BCTH_NoiTru_30007.xls";
                int[] _arrWidth = new int[] { };
                //DungChung.Bien.MangHaiChieu = new Object[q3.Count + 1, 13];
                DungChung.Bien.MangHaiChieu = new Object[_list.Count + 1, 13];
                for (int i = 0; i < 13; i++)
                {
                    if(i==0)
                        DungChung.Bien.MangHaiChieu[0, i] = "TT";
                      
                    else if(i== 1)
                        DungChung.Bien.MangHaiChieu[0, i] = "Nội dung";                       
                    else if(i==12)
                        DungChung.Bien.MangHaiChieu[0, i] = "Tổng";
                    else
                    {
                        if (i < (lkhoa.Count + 2))
                            DungChung.Bien.MangHaiChieu[0, i] = lkhoa[i - 2];
                        else
                            DungChung.Bien.MangHaiChieu[0, i] = "";
                    }
                }
                int num = 1;
                foreach (var r in _list)
                {
                    
                    DungChung.Bien.MangHaiChieu[num, 0] = r.Stt;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.Nd;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.Khoa1;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.Khoa2;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.Khoa3;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.Khoa4;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.Khoa5;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.Khoa6;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.Khoa7;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.Khoa8;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.Khoa9;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.Khoa10;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.Tong;                   
                    num++;
                }

                #endregion
                BaoCao.rep_BCTH_KhamBenh_30007 rep = new BaoCao.rep_BCTH_KhamBenh_30007();
                if (txtNgayThang.Text != "")
                    rep.lab_tungaydenngay.Text = txtNgayThang.Text;
                else
                    rep.lab_tungaydenngay.Text = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupngayden.DateTime.ToShortDateString();

                double sothang = Math.Round((((ngayden - ngaytu).TotalDays + 15) / 30), 0);

                if (txtTieude.Text != "")
                    rep.lab_Tieude.Text = txtTieude.Text.ToUpper();
                else
                    rep.lab_Tieude.Text = "TỔNG HỢP KHÁM BỆNH " + sothang + " THÁNG NĂM " + ngayden.Year.ToString();
                rep.lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.cel_NLB.Text = DungChung.Bien.NguoiLapBieu;
                rep.cel_gd.Text = DungChung.Bien.GiamDoc;
                #region hiển thị tên khoa lên báo cáo
                for ( int i = 1; i<lkhoa.Count +1; i++)
                { 
                    switch(i)
                        {
                        case 1:
                                rep.celTenKhoa1.Text = lkhoa[i - 1];
                                break;
                        case 2:
                                rep.celTenKhoa2.Text = lkhoa[i - 1];
                                break;
                        case 3:
                                rep.celTenKhoa3.Text = lkhoa[i - 1];
                                break;
                        case 4:
                                rep.celTenKhoa4.Text = lkhoa[i - 1];
                                break;
                        case 5:
                                rep.celTenKhoa5.Text = lkhoa[i - 1];
                                break;
                        case 6:
                                rep.celTenKhoa6.Text = lkhoa[i - 1];
                                break;
                        case 7:
                                rep.celTenKhoa7.Text = lkhoa[i - 1];
                                break;
                        case 8:
                                rep.celTenKhoa8.Text = lkhoa[i - 1];
                                break;
                        case 9:
                                rep.celTenKhoa9.Text = lkhoa[i - 1];
                                break;
                        case 10:
                                rep.celTenKhoa10.Text = lkhoa[i - 1];
                                break;
                    }
                }
                #endregion
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                rep.DataSource = _list;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region báo cáo tổng hợp khám bệnh - mới
            else if (radBieu.SelectedIndex == 1 && radioGroup1.SelectedIndex == 1)
            {
                #region báo cáo tổng hợp khám bệnh - mới - qua chỉ định
                if (radioGroup2.SelectedIndex == 0)
                {
                    #region Lấy dữ liệu chung
                    var q1 = (from bn in data.BenhNhans
                              join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                              join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                              into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new { bn.MaBNhan, bn.Tuoi, dtbn.DTBN1, QuocTich = kq1 == null ? "" : kq1.NgoaiKieu }).ToList();
                    var q2 = (from rv in data.RaViens
                              join ck in data.ChuyenKhoas on rv.MaCK equals ck.MaCK
                              where rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden
                              select new { rv.MaBNhan, rv.Status, ck.TenCK, rv.MaBVC }).ToList();
                    var q3 = (from rv in q2
                              join bv in data.BenhViens on rv.MaBVC equals bv.MaBV into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new { rv.MaBNhan, rv.TenCK, ChuyenTuyenTren = (rv.Status == null || rv.Status != 1 || kq1 == null) ? 0 : GetChuyenTuyenTren(kq1.TuyenBV), rv.Status }).ToList();
                    var q5 = (from vv in data.VaoViens.Where(p => p.NgayVao >= ngaytu && p.NgayVao <= ngayden)
                              join ck in data.ChuyenKhoas on vv.MaCK equals ck.MaCK
                              select new { vv.MaBNhan, ck.TenCK }).ToList();
                    var vv1 = (from a1 in q5
                               group a1 by a1.TenCK into kq
                               select new
                               {
                                   TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                                   TSVV = kq.Count()
                               }).ToList();
                    var cd = (from a1 in data.CLS
                              join b in data.ChiDinhs.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden).Where(p => p.Status == 1) on a1.IdCLS equals b.IdCLS
                              join c in data.KPhongs on a1.MaKP equals c.MaKP
                              join d in data.ChuyenKhoas on c.MaCK equals d.MaCK
                              select new { b.MaDV, b.NgayTH, d.TenCK }).ToList();
                    var dc = (from a1 in cd
                              join b in data.DichVus on a1.MaDV equals b.MaDV
                              join c in data.TieuNhomDVs on b.IdTieuNhom equals c.IdTieuNhom
                              group new { a1, b, c } by a1.TenCK into kq
                              select new
                              {
                                  TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                                  TS1 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count(),
                                  TS2 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao).Count(),
                                  TS3 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count(),
                                  TS4 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Count(),
                                  TS5 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count(),
                                  TS6 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count(),
                                  TS7 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count(),
                                  TS8 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count(),
                                  TS9 = 0
                              }).ToList();
                    var q4 = (from bn in q1
                              join ck in q3 on bn.MaBNhan equals ck.MaBNhan
                              group new { ck, bn } by ck.TenCK into kq
                              select new
                              {
                                  TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                                  TS = kq.Count(),
                                  TSTructiep = kq.Where(p => p.bn.DTBN1 != "BHYT").Count(),
                                  TSBHYT = kq.Where(p => p.bn.DTBN1 == "BHYT").Count(),
                                  TS6 = kq.Where(p => p.bn.Tuoi < 6).Count(),
                                  TSBHYT6 = kq.Where(p => p.bn.DTBN1 == "BHYT").Where(p => p.bn.Tuoi < 6).Count(),
                                  TSTructiep6 = kq.Where(p => p.bn.DTBN1 != "BHYT").Where(p => p.bn.Tuoi < 6).Count(),
                                  TSRV = kq.Where(p => p.ck.Status == 1).Count(),
                                  TS60 = kq.Where(p => p.bn.Tuoi >= 60).Count(),
                                  TSBHYT60 = kq.Where(p => p.bn.DTBN1 == "BHYT").Where(p => p.bn.Tuoi >= 60).Count(),
                                  TSTructiep60 = kq.Where(p => p.bn.DTBN1 != "BHYT").Where(p => p.bn.Tuoi >= 60).Count(),
                              }).ToList();
                    #endregion
                    List<string> _l1 = new List<string>();
                    _l1 = q4.Select(p => p.TenCK).Distinct().ToList();
                    List<string> _l2 = dc.Select(p => p.TenCK).Distinct().ToList();
                    List<string> l = new List<string>();
                    l.InsertRange(0, _l1);
                    l.InsertRange(_l1.Count, _l2);
                    List<string> lkhoa = new List<string>();
                    lkhoa.InsertRange(0, l);
                    foreach (var item in _l1)
                    {

                        foreach (var item1 in _l2)
                        {
                            if (item == item1)
                            {
                                lkhoa.Remove(item1);
                            }
                        }

                    }
                    string a = String.Join(",", lkhoa.Skip(10).ToList());
                    if (lkhoa.Count > 10)
                        MessageBox.Show("Số chuyên khoa nhiều hơn 10, các khoa không được hiển thị dữ liệu: " + a);
                    List<KbKhoa> _list = new List<KbKhoa>();
                    Type _type = Type.GetType("Namespace.FormThamSo.frm_BCTH_NoiTru_30007.KbKhoa");
                    string propertyName = "Khoa";
                    int count = lkhoa.Count;
                    int? value = 0;
                    #region đổ dữ liệu vào list
                    for (int k = 1; k < 21; k++)// k là số dòng
                    {
                        KbKhoa moi = new KbKhoa();
                        int? tong = 0;

                        for (int i = 1; i <= count; i++)
                        {
                            propertyName = "Khoa" + i.ToString();  // thuộc tính của đối tượng   KbKhoa                   
                            var propertyInfo = moi.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (k == 1)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS);
                            else if (k == 2)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep);
                            else if (k == 3)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT);
                            else if (k == 4)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS6);
                            else if (k == 5)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT6);
                            else if (k == 6)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep6);
                            else if (k == 7)
                                value = vv1.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSVV);
                            else if (k == 8)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSRV);
                            else if (k == 9)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS60);
                            else if (k == 10)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT60);
                            else if (k == 11)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep60);
                            else if (k == 12)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS1);
                            else if (k == 13)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS2);
                            else if (k == 14)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS3);
                            else if (k == 15)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS4);
                            else if (k == 16)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS5);
                            else if (k == 17)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS6);
                            else if (k == 18)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS7);
                            else if (k == 19)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS8);
                            else if (k == 20)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS9);

                            propertyInfo.SetValue(moi, value, null);
                            tong += value;
                            if (i == 10)
                                break;
                        }
                        #region hiển thị số thứ tự và nội dung
                        switch (k)
                        {
                            case 1:
                                moi.Stt = "1";
                                moi.Nd = "Tổng số lượt khám bệnh (= 1a + 1b)";
                                break;
                            case 2:
                                moi.Stt = "1a";
                                moi.Nd = "TS lượt khám bệnh thu phí trực tiếp";
                                break;
                            case 3:
                                moi.Stt = "1b";
                                moi.Nd = "TS lượt KB cho người được BHYT chi trả (tất cả các đối tượng có BHYT)";
                                break;
                            case 4:
                                moi.Stt = "2";
                                moi.Nd = "Tổng số khám chữa bệnh trẻ em dưới 6 tuổi (= 2a +2b)";
                                break;
                            case 5:
                                moi.Stt = "2a";
                                moi.Nd = "Số lượt khám bệnh cho trẻ em < 6 tuổi có thẻ BHYT";
                                break;
                            case 6:
                                moi.Stt = "2b";
                                moi.Nd = "Số lượt khám bệnh cho trẻ em < 6 tuổi thu phí";
                                break;
                            case 7:
                                moi.Stt = "3";
                                moi.Nd = "TS BN vào viện";
                                break;
                            case 8:
                                moi.Stt = "4";
                                moi.Nd = "TS BN chuyển viện";
                                break;
                            case 9:
                                moi.Stt = "5";
                                moi.Nd = "BN > 60 tuổi ( 6a+6b)";
                                break;
                            case 10:
                                moi.Stt = "5a";
                                moi.Nd = "BN > 60 tuổi có thẻ BHYT";
                                break;
                            case 11:
                                moi.Stt = "5b";
                                moi.Nd = "BN> 60 tuổi không có thẻ BHYT";
                                break;
                            case 12:
                                moi.Stt = "6";
                                moi.Nd = "Đo Mật độ xương";
                                break;
                            case 13:
                                moi.Stt = "7";
                                moi.Nd = "Điện não đồ";
                                break;
                            case 14:
                                moi.Stt = "8";
                                moi.Nd = "Đo chức năng hô hấp";
                                break;
                            case 15:
                                moi.Stt = "9";
                                moi.Nd = "TS Nội soi Tai mũi họng";
                                break;
                            case 16:
                                moi.Stt = "10";
                                moi.Nd = "TS Nội soi thực quản dạ dày";
                                break;
                            case 17:
                                moi.Stt = "11";
                                moi.Nd = "TS xét nghiệm";
                                break;
                            case 18:
                                moi.Stt = "12";
                                moi.Nd = "TS X quang";
                                break;
                            case 19:
                                moi.Stt = "13";
                                moi.Nd = "TS siêu âm";
                                break;
                            case 20:
                                moi.Stt = "14";
                                moi.Nd = "TS sinh thiết tế bào";
                                break;
                        }
                        #endregion
                        moi.Tong = tong;
                        _list.Add(moi);
                    }
                    #endregion
                    #region xuất excel
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string _filePath = "D:\\BCTH_NoiTru_30007.xls";
                    int[] _arrWidth = new int[] { };
                    //DungChung.Bien.MangHaiChieu = new Object[q3.Count + 1, 13];
                    DungChung.Bien.MangHaiChieu = new Object[_list.Count + 1, 13];
                    for (int i = 0; i < 13; i++)
                    {
                        if (i == 0)
                            DungChung.Bien.MangHaiChieu[0, i] = "TT";

                        else if (i == 1)
                            DungChung.Bien.MangHaiChieu[0, i] = "Nội dung";
                        else if (i == 12)
                            DungChung.Bien.MangHaiChieu[0, i] = "Toàn viện";
                        else
                        {
                            if (i < (lkhoa.Count + 2))
                                DungChung.Bien.MangHaiChieu[0, i] = lkhoa[i - 2];
                            else
                                DungChung.Bien.MangHaiChieu[0, i] = "";
                        }
                    }
                    int num = 1;
                    foreach (var r in _list)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = r.Stt;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.Nd;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.Khoa1;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.Khoa2;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.Khoa3;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.Khoa4;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.Khoa5;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.Khoa6;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.Khoa7;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.Khoa8;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.Khoa9;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.Khoa10;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.Tong;
                        num++;
                    }

                    #endregion
                    BaoCao.rep_BCTH_KhamBenh_30007_New rep = new BaoCao.rep_BCTH_KhamBenh_30007_New();
                    if (txtNgayThang.Text != "")
                        rep.lab_tungaydenngay.Text = txtNgayThang.Text;
                    else
                        rep.lab_tungaydenngay.Text = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupngayden.DateTime.ToShortDateString();

                    double sothang = Math.Round((((ngayden - ngaytu).TotalDays + 15) / 30), 0);

                    if (txtTieude.Text != "")
                        rep.lab_Tieude.Text = txtTieude.Text.ToUpper();
                    else
                        rep.lab_Tieude.Text = "TỔNG HỢP KHÁM BỆNH " + sothang + " THÁNG NĂM " + ngayden.Year.ToString();
                    rep.lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.cel_NLB.Text = DungChung.Bien.NguoiLapBieu;
                    rep.cel_gd.Text = DungChung.Bien.GiamDoc;
                    #region hiển thị tên khoa lên báo cáo
                    for (int i = 1; i < lkhoa.Count + 1; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.celTenKhoa1.Text = lkhoa[i - 1];
                                break;
                            case 2:
                                rep.celTenKhoa2.Text = lkhoa[i - 1];
                                break;
                            case 3:
                                rep.celTenKhoa3.Text = lkhoa[i - 1];
                                break;
                            case 4:
                                rep.celTenKhoa4.Text = lkhoa[i - 1];
                                break;
                            case 5:
                                rep.celTenKhoa5.Text = lkhoa[i - 1];
                                break;
                            case 6:
                                rep.celTenKhoa6.Text = lkhoa[i - 1];
                                break;
                            case 7:
                                rep.celTenKhoa7.Text = lkhoa[i - 1];
                                break;
                            case 8:
                                rep.celTenKhoa8.Text = lkhoa[i - 1];
                                break;
                            case 9:
                                rep.celTenKhoa9.Text = lkhoa[i - 1];
                                break;
                            case 10:
                                rep.celTenKhoa10.Text = lkhoa[i - 1];
                                break;
                        }
                    }
                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    rep.DataSource = _list;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region báo cáo tổng hợp khám bệnh - mới - không qua chỉ định
                else if (radioGroup2.SelectedIndex == 1)
                {
                    #region Lấy dữ liệu chung
                    var q1 = (from bn in data.BenhNhans
                              join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                              join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                              into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new { bn.MaBNhan, bn.Tuoi, dtbn.DTBN1, QuocTich = kq1 == null ? "" : kq1.NgoaiKieu }).ToList();
                    var q2 = (from rv in data.RaViens
                              join ck in data.ChuyenKhoas on rv.MaCK equals ck.MaCK
                              where rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden
                              select new { rv.MaBNhan, rv.Status, ck.TenCK, rv.MaBVC }).ToList();
                    var q3 = (from rv in q2
                              join bv in data.BenhViens on rv.MaBVC equals bv.MaBV into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new { rv.MaBNhan, rv.TenCK, ChuyenTuyenTren = (rv.Status == null || rv.Status != 1 || kq1 == null) ? 0 : GetChuyenTuyenTren(kq1.TuyenBV), rv.Status }).ToList();
                    var q5 = (from vv in data.VaoViens.Where(p => p.NgayVao >= ngaytu && p.NgayVao <= ngayden)
                              join ck in data.ChuyenKhoas on vv.MaCK equals ck.MaCK
                              select new { vv.MaBNhan, ck.TenCK }).ToList();
                    var vv1 = (from a1 in q5
                               group a1 by a1.TenCK into kq
                               select new
                               {
                                   TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                                   TSVV = kq.Count()
                               }).ToList();
                    var cd = (from a1 in data.DThuocs
                              join b in data.DThuoccts.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => p.IDCD == null) on a1.IDDon equals b.IDDon
                              join c in data.KPhongs on a1.MaKP equals c.MaKP
                              join d in data.ChuyenKhoas on c.MaCK equals d.MaCK
                              select new { b.MaDV, b.NgayNhap, d.TenCK, b.SoLuong }).ToList();
                    var dc = (from a1 in cd
                              join b in data.DichVus on a1.MaDV equals b.MaDV
                              join c in data.TieuNhomDVs on b.IdTieuNhom equals c.IdTieuNhom
                              group new { a1, b, c } by a1.TenCK into kq
                              select new
                              {
                                  TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                                  TS1 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.a1.SoLuong),
                                  TS2 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao).Sum(p => p.a1.SoLuong),
                                  TS3 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Sum(p => p.a1.SoLuong),
                                  TS4 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Sum(p => p.a1.SoLuong),
                                  TS5 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Sum(p => p.a1.SoLuong),
                                  TS6 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.a1.SoLuong),
                                  TS7 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.a1.SoLuong),
                                  TS8 = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.a1.SoLuong),
                                  TS9 = 0
                              }).ToList();
                    var q4 = (from bn in q1
                              join ck in q3 on bn.MaBNhan equals ck.MaBNhan
                              group new { ck, bn } by ck.TenCK into kq
                              select new
                              {
                                  TenCK = kq.Key == "Đông y" ? "YHCT - PHCN" : kq.Key,
                                  TS = kq.Count(),
                                  TSTructiep = kq.Where(p => p.bn.DTBN1 != "BHYT").Count(),
                                  TSBHYT = kq.Where(p => p.bn.DTBN1 == "BHYT").Count(),
                                  TS6 = kq.Where(p => p.bn.Tuoi < 6).Count(),
                                  TSBHYT6 = kq.Where(p => p.bn.DTBN1 == "BHYT").Where(p => p.bn.Tuoi < 6).Count(),
                                  TSTructiep6 = kq.Where(p => p.bn.DTBN1 != "BHYT").Where(p => p.bn.Tuoi < 6).Count(),
                                  TSRV = kq.Where(p => p.ck.Status == 1).Count(),
                                  TS60 = kq.Where(p => p.bn.Tuoi >= 60).Count(),
                                  TSBHYT60 = kq.Where(p => p.bn.DTBN1 == "BHYT").Where(p => p.bn.Tuoi >= 60).Count(),
                                  TSTructiep60 = kq.Where(p => p.bn.DTBN1 != "BHYT").Where(p => p.bn.Tuoi >= 60).Count(),
                              }).ToList();
                    #endregion
                    List<string> _l1 = new List<string>();
                    _l1 = q4.Select(p => p.TenCK).Distinct().ToList();
                    List<string> _l2 = dc.Select(p => p.TenCK).Distinct().ToList();
                    List<string> l = new List<string>();
                    l.InsertRange(0, _l1);
                    l.InsertRange(_l1.Count, _l2);
                    List<string> lkhoa = new List<string>();
                    lkhoa.InsertRange(0, l);                   
                    foreach(var item in _l1)
                    {
                        
                        foreach (var item1 in _l2)
                        {
                            if(item == item1)
                            {
                                lkhoa.Remove(item1);
                            }          
                        }
                        
                    }
                    string a = String.Join(",", lkhoa.Skip(10).ToList());
                    if (lkhoa.Count > 10)
                        MessageBox.Show("Số chuyên khoa nhiều hơn 10, các khoa không được hiển thị dữ liệu: " + a);
                    List<KbKhoa> _list = new List<KbKhoa>();
                    Type _type = Type.GetType("Namespace.FormThamSo.frm_BCTH_NoiTru_30007.KbKhoa");
                    string propertyName = "Khoa";
                    int count = lkhoa.Count;
                    int? value = 0;
                    #region đổ dữ liệu vào list
                    for (int k = 1; k < 21; k++)// k là số dòng
                    {
                        KbKhoa moi = new KbKhoa();
                        int? tong = 0;
                        
                        for (int i = 1; i <= count; i++)
                        {
                            propertyName = "Khoa" + i.ToString();  // thuộc tính của đối tượng   KbKhoa                   
                            var propertyInfo = moi.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (k == 1)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS);
                            else if (k == 2)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep);
                            else if (k == 3)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT);
                            else if (k == 4)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS6);
                            else if (k == 5)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT6);
                            else if (k == 6)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep6);
                            else if (k == 7)
                                value = vv1.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSVV);
                            else if (k == 8)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSRV);
                            else if (k == 9)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS60);
                            else if (k == 10)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSBHYT60);
                            else if (k == 11)
                                value = q4.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TSTructiep60);
                            else if (k == 12)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS1));
                            else if (k == 13)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS2));
                            else if (k == 14)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS3));
                            else if (k == 15)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS4));
                            else if (k == 16)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS5));
                            else if (k == 17)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS6));
                            else if (k == 18)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS7));
                            else if (k == 19)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => Convert.ToInt32(p.TS8));
                            else if (k == 20)
                                value = dc.Where(p => p.TenCK == lkhoa[i - 1]).Sum(p => p.TS9);

                            propertyInfo.SetValue(moi, value, null);
                            tong += value;
                            if (i == 10)
                                break;
                        }
                        #region hiển thị số thứ tự và nội dung
                        switch (k)
                        {
                            case 1:
                                moi.Stt = "1";
                                moi.Nd = "Tổng số lượt khám bệnh (= 1a + 1b)";
                                break;
                            case 2:
                                moi.Stt = "1a";
                                moi.Nd = "TS lượt khám bệnh thu phí trực tiếp";
                                break;
                            case 3:
                                moi.Stt = "1b";
                                moi.Nd = "TS lượt KB cho người được BHYT chi trả (tất cả các đối tượng có BHYT)";
                                break;
                            case 4:
                                moi.Stt = "2";
                                moi.Nd = "Tổng số khám chữa bệnh trẻ em dưới 6 tuổi (= 2a +2b)";
                                break;
                            case 5:
                                moi.Stt = "2a";
                                moi.Nd = "Số lượt khám bệnh cho trẻ em < 6 tuổi có thẻ BHYT";
                                break;
                            case 6:
                                moi.Stt = "2b";
                                moi.Nd = "Số lượt khám bệnh cho trẻ em < 6 tuổi thu phí";
                                break;
                            case 7:
                                moi.Stt = "3";
                                moi.Nd = "TS BN vào viện";
                                break;
                            case 8:
                                moi.Stt = "4";
                                moi.Nd = "TS BN chuyển viện";
                                break;
                            case 9:
                                moi.Stt = "5";
                                moi.Nd = "BN > 60 tuổi ( 6a+6b)";
                                break;
                            case 10:
                                moi.Stt = "5a";
                                moi.Nd = "BN > 60 tuổi có thẻ BHYT";
                                break;
                            case 11:
                                moi.Stt = "5b";
                                moi.Nd = "BN> 60 tuổi không có thẻ BHYT";
                                break;
                            case 12:
                                moi.Stt = "6";
                                moi.Nd = "Đo Mật độ xương";
                                break;
                            case 13:
                                moi.Stt = "7";
                                moi.Nd = "Điện não đồ";
                                break;
                            case 14:
                                moi.Stt = "8";
                                moi.Nd = "Đo chức năng hô hấp";
                                break;
                            case 15:
                                moi.Stt = "9";
                                moi.Nd = "TS Nội soi Tai mũi họng";
                                break;
                            case 16:
                                moi.Stt = "10";
                                moi.Nd = "TS Nội soi thực quản dạ dày";
                                break;
                            case 17:
                                moi.Stt = "11";
                                moi.Nd = "TS xét nghiệm";
                                break;
                            case 18:
                                moi.Stt = "12";
                                moi.Nd = "TS X quang";
                                break;
                            case 19:
                                moi.Stt = "13";
                                moi.Nd = "TS siêu âm";
                                break;
                            case 20:
                                moi.Stt = "14";
                                moi.Nd = "TS sinh thiết tế bào";
                                break;
                        }
                        #endregion
                        moi.Tong = tong;
                        _list.Add(moi);
                    }
                    #endregion
                    #region xuất excel
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string _filePath = "D:\\BCTH_NoiTru_30007.xls";
                    int[] _arrWidth = new int[] { };
                    //DungChung.Bien.MangHaiChieu = new Object[q3.Count + 1, 13];
                    DungChung.Bien.MangHaiChieu = new Object[_list.Count + 1, 13];
                    for (int i = 0; i < 13; i++)
                    {
                        if (i == 0)
                            DungChung.Bien.MangHaiChieu[0, i] = "TT";

                        else if (i == 1)
                            DungChung.Bien.MangHaiChieu[0, i] = "Nội dung";
                        else if (i == 12)
                            DungChung.Bien.MangHaiChieu[0, i] = "Toàn viện";
                        else
                        {
                            if (i < (lkhoa.Count + 2))
                                DungChung.Bien.MangHaiChieu[0, i] = lkhoa[i - 2];
                            else
                                DungChung.Bien.MangHaiChieu[0, i] = "";
                        }
                    }
                    int num = 1;
                    foreach (var r in _list)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = r.Stt;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.Nd;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.Khoa1;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.Khoa2;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.Khoa3;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.Khoa4;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.Khoa5;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.Khoa6;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.Khoa7;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.Khoa8;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.Khoa9;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.Khoa10;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.Tong;
                        num++;
                    }

                    #endregion
                    BaoCao.rep_BCTH_KhamBenh_30007_New rep = new BaoCao.rep_BCTH_KhamBenh_30007_New();
                    if (txtNgayThang.Text != "")
                        rep.lab_tungaydenngay.Text = txtNgayThang.Text;
                    else
                        rep.lab_tungaydenngay.Text = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupngayden.DateTime.ToShortDateString();

                    double sothang = Math.Round((((ngayden - ngaytu).TotalDays + 15) / 30), 0);

                    if (txtTieude.Text != "")
                        rep.lab_Tieude.Text = txtTieude.Text.ToUpper();
                    else
                        rep.lab_Tieude.Text = "TỔNG HỢP KHÁM BỆNH " + sothang + " THÁNG NĂM " + ngayden.Year.ToString();
                    rep.lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.cel_NLB.Text = DungChung.Bien.NguoiLapBieu;
                    rep.cel_gd.Text = DungChung.Bien.GiamDoc;
                    #region hiển thị tên khoa lên báo cáo
                    for (int i = 1; i < lkhoa.Count + 1; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.celTenKhoa1.Text = lkhoa[i - 1];
                                break;
                            case 2:
                                rep.celTenKhoa2.Text = lkhoa[i - 1];
                                break;
                            case 3:
                                rep.celTenKhoa3.Text = lkhoa[i - 1];
                                break;
                            case 4:
                                rep.celTenKhoa4.Text = lkhoa[i - 1];
                                break;
                            case 5:
                                rep.celTenKhoa5.Text = lkhoa[i - 1];
                                break;
                            case 6:
                                rep.celTenKhoa6.Text = lkhoa[i - 1];
                                break;
                            case 7:
                                rep.celTenKhoa7.Text = lkhoa[i - 1];
                                break;
                            case 8:
                                rep.celTenKhoa8.Text = lkhoa[i - 1];
                                break;
                            case 9:
                                rep.celTenKhoa9.Text = lkhoa[i - 1];
                                break;
                            case 10:
                                rep.celTenKhoa10.Text = lkhoa[i - 1];
                                break;
                        }
                    }
                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    rep.DataSource = _list;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
            }
            #endregion
        }
        /// <summary>
        /// trả về 1 nều là tuyến trên, nếu ko thì trả về 0
        /// </summary>
        /// <param name="tuyenBV"></param>
        /// <returns></returns>
        private int GetChuyenTuyenTren(string tuyenBV)
        {
            if (String.IsNullOrEmpty(tuyenBV))
                return 0;
            else
            {
                int tuyen = 0; // tuyến của bệnh viện chuyển
                switch (tuyenBV)
                {
                    case "A":
                        tuyen = 1;
                        break;
                    case "B":
                        tuyen = 2;
                        break;
                    case "C":
                        tuyen = 3;
                        break;
                    case "D":
                        tuyen = 4;
                        break;
                }
                if (tuyen < _Tuyen)
                    return 1;// chuyển lên BV tuyến trên
                else return 0;

            }
        }

        private int GetTuyen(string maBV)
        {
            int tuyen = -1;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string hangBV =(from bv in data.BenhViens.Where(p => p.MaBV == maBV) select new {bv.TuyenBV}).Select(p=>p.TuyenBV).FirstOrDefault();

            if (hangBV != null)
                hangBV = hangBV.Trim();
                           
            switch (hangBV)
            {
                case "A":
                    tuyen = 1;
                    break;
                case "B":
                    tuyen = 2;
                    break;
                case "C":
                    tuyen = 3;
                    break;
                case "D":
                    tuyen = 4;
                    break;
            }

            return tuyen;
        }
        class KbKhoa
        {
            string stt;


            public string Stt
            {
                get { return stt; }
                set { stt = value; }
            }
            string nd;


            public string Nd
            {
                get { return nd; }
                set { nd = value; }
            }
            string tenkhoa;


            public string Tenkhoa
            {
                get { return tenkhoa; }
                set { tenkhoa = value; }
            }

            private int? khoa1;


            public int? Khoa1
            {
                get { return khoa1; }
                set { khoa1 = value; }
            }
            private int? khoa2;


            public int? Khoa2
            {
                get { return khoa2; }
                set { khoa2 = value; }
            }
            private int? khoa3;


            public int? Khoa3
            {
                get { return khoa3; }
                set { khoa3 = value; }
            }
            private int? khoa4;


            public int? Khoa4
            {
                get { return khoa4; }
                set { khoa4 = value; }
            }
            private int? khoa5;


            public int? Khoa5
            {
                get { return khoa5; }
                set { khoa5 = value; }
            }
            private int? khoa6;


            public int? Khoa6
            {
                get { return khoa6; }
                set { khoa6 = value; }
            }
            private int? khoa7;


            public int? Khoa7
            {
                get { return khoa7; }
                set { khoa7 = value; }
            }
            private int? khoa8;


            public int? Khoa8
            {
                get { return khoa8; }
                set { khoa8 = value; }
            }
            private int? khoa9;


            public int? Khoa9
            {
                get { return khoa9; }
                set { khoa9 = value; }
            }

            private int? khoa10;


            public int? Khoa10
            {
                get { return khoa10; }
                set { khoa10 = value; }
            }
            private int? tong;


            public int? Tong
            {
                get { return tong; }
                set { tong = value; }
            }

        }
        QLBV_Database.QLBVEntities data;
        int _Tuyen = 0;
        private void frm_BCTH_NoiTru_30007_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            _Tuyen = GetTuyen(DungChung.Bien.MaBV);
            radioGroup1.Enabled = false;
            radioGroup2.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radBieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radBieu.SelectedIndex == 1)
                radioGroup1.Enabled = true;
            else radioGroup1.Enabled = false;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 1)
                radioGroup2.Enabled = true;
            else 
                radioGroup2.Enabled = false;
        }
    }
}