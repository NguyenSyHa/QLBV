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
    public partial class frm_THHoatDongChuyenMon : DevExpress.XtraEditors.XtraForm
    {
        public frm_THHoatDongChuyenMon()
        {
            InitializeComponent();
        }
        public class MyObject
        {
            public int Value { set; get; }
        }
        private void frm_THHoatDongChuyenMon_Load(object sender, EventArgs e)
        {
            //load ds năm
            int namHT = DateTime.Now.Year;
            List<MyObject> _list = new List<MyObject>();
            for( int i = namHT-10;i <namHT+11; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _list.Add(obj);
            }
            cbNam.DisplayMember = "Value";
            cbNam.ValueMember = "Value";
            cbNam.DataSource = _list;
            cbNam.SelectedValue  = namHT;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int mackhoa(string tenck, List<ChuyenKhoa> lck)
        {
            if (tenck == null)
                return 99;

            var ten = lck.Where(p => p.TenCK == tenck.ToString()).Select(p => p.MaCK).ToList();
            if (ten.Count > 0)
                return ten.First();
            else return 99;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);           
            var ravien = data.RaViens.ToList();
            var vaovien = data.VaoViens.ToList();

            
            // năm hiện tại
            DateTime tungayHT =  new DateTime(Convert.ToInt32(cbNam.SelectedValue), 1, 1);
            DateTime denngayHT = DungChung.Ham.NgayDen(tungayHT.AddYears(1).AddDays(-1));
            //năm trước
            DateTime tungayNT = tungayHT.AddYears(-1);
            DateTime denngayNT = denngayHT.AddYears(-1);
             List<ChuyenKhoa> lck = data.ChuyenKhoas.ToList();

            if (ckIn6thang.Checked)
            {
                if (rd6Thang.SelectedIndex == 0)// 6 tháng đầu năm
                {
                    denngayHT = DungChung.Ham.NgayDen(tungayHT.AddMonths(6).AddDays(-1));
                }
                else if (rd6Thang.SelectedIndex == 1) // 6 tháng cuối năm
                    tungayHT = tungayHT.AddMonths(6);
            }

            var benhnhan = (from bn in data.BenhNhans                      
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan into kq2
                       from kq1 in kq.DefaultIfEmpty()
                       from kq3 in kq2.DefaultIfEmpty()
                       //where (kq3 != null && kq3.NgayVao >= tungayHT && kq3.NgayVao <= denngayHT) || (kq1 != null && kq1.NgayRa >= tungayHT && kq1.NgayRa <= denngayHT)

                            where bn.NoiTru == 0 ? (bn.NNhap >= tungayHT && bn.NNhap <= denngayHT) : (
                            (kq3 != null && kq3.NgayVao < tungayHT && kq1 != null && kq1.NgayRa > denngayHT) ||
                            (kq3 != null && kq3.NgayVao >= tungayHT && kq3.NgayVao <= denngayHT) ||
                            (kq1 != null && (kq1.NgayRa >= tungayHT && kq1.NgayRa <= denngayHT)))// || (kq3 != null && kq3.NgayVao < tungayHT && kq3.NgayVao > denngayHT))
                       select new
                       {

                           bn.MaBNhan,
                           bn.TuyenDuoi,
                           bn.Tuoi,
                           bn.NNhap,
                           bn.NgaySinh,
                           bn.ThangSinh,
                           bn.NamSinh,
                           bn.GTinh,
                           bn.SThe,
                           bn.NoiTru,
                           bn.DTNT,
                           MaCSKCB = bn.TuyenDuoi == 0 ? DungChung.Bien.MaBV : (bn.ChuyenKhoa == null ? "" : bn.ChuyenKhoa),
                           NgayRa = kq1 != null ? kq1.NgayRa : null,
                           MaCK = kq1 != null ? kq1.MaCK : 99,
                           ckhoa = kq3 != null ? kq3.ChuyenKhoa : "",
                           SoNgaydt = kq1 != null ? kq1.SoNgaydt : 0,
                           KetQua = kq1 != null ? kq1.KetQua : "",                          
                           vvien = kq3 != null ? kq3.MaBNhan : 0,
                           Status = kq1 == null ? 0 : kq1.Status??0,
                       }
                          ).ToList();

             var q0 = (from bn in benhnhan
                          select new
                          {
                              bn.MaBNhan,
                              bn.TuyenDuoi,
                              bn.Tuoi,
                              bn.NNhap,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.NamSinh,
                              bn.GTinh,
                              bn.SThe,
                              bn.NoiTru,
                              bn.MaCSKCB,
                              bn.NgayRa,
                              MaCK = bn.MaCK == 99 ? mackhoa(bn.ckhoa, lck) : bn.MaCK,
                              bn.SoNgaydt,
                              bn.KetQua,                            
                              bn.vvien
                          }
                         ).ToList();

            var bnCoThe_Kb = benhnhan.Where(p=>p.NoiTru == 0).Where(p => p.SThe != null && p.SThe != "").ToList();
            var bnThanhToanTT_Kb = benhnhan.Where(p => p.NoiTru == 0).Where(p => p.SThe == null || p.SThe == "").ToList();

            var bnCoThe_NoiTru = benhnhan.Where(p => p.NoiTru == 1).Where(p => p.SThe != null && p.SThe != "").ToList();
            var bnThanhToanTT_NoiTru = benhnhan.Where(p => p.NoiTru == 1).Where(p => p.SThe == null || p.SThe == "").ToList();
          //  var bnThanhToanTT = (from bn in benhnhan join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan select bn).Distinct().ToList();

            List<Content> _listContent = new List<Content>();

            #region BCHDCM
            if (ckIn6thang.Checked == false)
            {
                Content moi = new Content();
                _listContent.Add(new Content { SttNhom = 1, Tennhom = "1. Tổng số giường kế hoạch", Chiso = "" });
                _listContent.Add(new Content { SttNhom = 2, Tennhom = "2. Tổng số giường thực kê", Chiso = "2. Trong đó : Tổng số giường Tự nguyện/ Theo yêu cầu/ Xã hội hóa/ Hoặc doc các tổ chức tặng" });
                _listContent.Add(new Content { SttNhom = 3, Tennhom = "3. Công suất sử dụng giường bệnh", Chiso = "3a. Tính theo giường bệnh kế hoạch" });
                _listContent.Add(new Content { SttNhom = 3, Tennhom = "3. Công suất sử dụng giường bệnh", Chiso = "3a. Tính theo giường bệnh thực kê" });

                #region 4. Lấy tổng số lượt khám bệnh

                moi.SttNhom = 4;
                moi.Tennhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ): " + Environment.NewLine + "  Trong đó:";
                moi.Chiso = "4a. Tổng số lượt khám bệnh thu phí trực tiếp";
                moi.SLNamSau = bnThanhToanTT_Kb.Count();
                // moi.SLNamTruoc = benhnhan.Where(p => p.NNhap > tungayNT && p.NNhap <= denngayNT).Count();
                // moi.Tyle = (double)moi.SLNamSau * 100 / moi.SLNamTruoc;
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 4;
                moi.Tennhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ): " + Environment.NewLine + "  Trong đó:";
                moi.Chiso = "4b. Tổng số lượt khám bệnh cho người được BHYT chi trả (tất cả các đối tượng có thẻ BHYT)";
                moi.SLNamSau = bnCoThe_Kb.Count;
                // moi.SLNamTruoc = benhnhan.Where(p => p.SThe != null && p.SThe != "").Count();
                moi.Tyle = (double)moi.SLNamSau * 100 / moi.SLNamTruoc;
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 4;
                moi.Tennhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ): " + Environment.NewLine + "  Trong đó:";
                moi.Chiso = "4c. Tổng số lượt khám bệnh cho người nghèo (Không sử dụng thẻ BHYT nhưng vẫn được quyết toán theo thực thanh thực chi)";
                // moi.SLNamSau = benhnhan.Where(p => p.SThe != null && p.SThe != "").Count();
                // moi.SLNamTruoc = benhnhan.Where(p => p.SThe != null && p.SThe != "").Count();
                // moi.Tyle = (double)moi.SLNamSau * 100 / moi.SLNamTruoc;
                _listContent.Add(moi);

                _listContent.Add(new Content { SttNhom = 4, Tennhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ): " + Environment.NewLine + " Trong đó:", Chiso = "4c. Tổng số lượt khám bệnh cho người nghèo (Không sử dụng thẻ BHYT nhưng vẫn được quyết toán theo thực thanh thực chi)" });
                _listContent.Add(new Content { SttNhom = 4, Tennhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ): " + Environment.NewLine + " Trong đó:", Chiso = "4d. Tổng số lượt khám viện phí cho các dối tượng (cận nghèo, khó khăn...) do BV quyết định" });
                _listContent.Add(new Content { SttNhom = 4, Tennhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ): " + Environment.NewLine + " Trong đó:", Chiso = "4e. Tổng số lượt khám giảm viện phí do Bv quyết định" });

                #endregion

                #region 5. Tổng số khám chữa bệnh trẻ em dưới 6 tuổi
                moi = new Content();
                moi.SttNhom = 5;
                moi.Tennhom = "5. Tổng số khám chữa bệnh trẻ em dưới 6 tuổi (các đối tượng) " + Environment.NewLine + " Trong đó:";
                moi.Chiso = "- Số trẻ dưới 6 tuổi có thẻ BHYT, hoặc thẻ khám, chữa bệnh cho trẻ em dưới 6 tuổi:";
                moi.SLNamSau = bnCoThe_Kb.Where(p => p.Tuoi == null || p.Tuoi.Value < 6).Count();
                // moi.SLNamTruoc = benhnhan.Where(p => p.NNhap > tungayNT && p.NNhap <= denngayNT).Where(p => p.SThe != null && p.SThe != "").Count();
                // moi.Tyle = (double)moi.SLNamSau * 100 / moi.SLNamTruoc;
                _listContent.Add(moi);
                var q5_2 = bnThanhToanTT_Kb.Where(p => p.Tuoi == null || p.Tuoi < 6).ToList();
                moi = new Content();
                moi.SttNhom = 5;
                moi.Tennhom = "5. Tổng số khám chữa bệnh trẻ em dưới 6 tuổi (các đối tượng) " + Environment.NewLine + " Trong đó:";
                moi.Chiso = "- Số trẻ dưới 6 tuổi thu phí trực tiếp";
                moi.SLNamSau = q5_2.Count;
                _listContent.Add(moi);
                #endregion

                #region 6. Khám cho người bệnh cao tuổi
                moi = new Content();
                moi.SttNhom = 6;
                moi.Tennhom = "6. Tổng số khám cho người bệnh cao tuổi >= 60 tuổi (tất cả các đối tượng): ";
                moi.Chiso = "Trong đó:" + Environment.NewLine + "6a. Số lượt khám bệnh cho người cao tuổi có thẻ BHYT, hoặc đối tượng chính sách khác được miễn viện phí";
                moi.SLNamSau = bnCoThe_Kb.Where(p => p.Tuoi >= 60).Count();
                _listContent.Add(moi);
                var q6_2 = bnThanhToanTT_Kb.Where(p => p.Tuoi >= 60).ToList();
                moi = new Content();
                moi.SttNhom = 6;
                moi.Tennhom = "6. Tổng số khám cho người bệnh cao tuổi >= 60 tuổi (tất cả các đối tượng): ";
                moi.Chiso = "6b. Số lượt khám bệnh cho người cao tuổi thu phí trực tiếp";
                moi.SLNamSau = q6_2.Count;
                _listContent.Add(moi);
                #endregion

                #region 7. Tổng số lượt khám cho người nước ngoài
                var q7_1 = (from bn in benhnhan.Where(p => p.NoiTru == 0) join ttbx in data.TTboXungs.Where(p => p.NgoaiKieu != "" && p.NgoaiKieu != "Việt Nam" && p.NgoaiKieu != "VN" && p.NgoaiKieu != null) on bn.MaBNhan equals ttbx.MaBNhan select bn).ToList();
                moi = new Content();
                moi.SttNhom = 7;
                moi.Tennhom = "7. Tổng số lượt khám cho người nước ngoài ";
                moi.Chiso = "";
                moi.SLNamSau = q7_1.Count;
                _listContent.Add(moi);
                #endregion

                #region 8. Tổng số lượt chuyển khám
                var q8_1 = benhnhan.Where(p => p.Status == 1).ToList();
                moi = new Content();
                moi.SttNhom = 8;
                moi.Tennhom = "8. Tổng số lượt chuyển khám ";
                moi.Chiso = "";
                moi.SLNamSau = q8_1.Count;
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 8;
                moi.Tennhom = "8. Tổng số lượt chuyển khám ";
                moi.Chiso = "8a. Chuyển khám BV tuyến trên";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 8;
                moi.Tennhom = "8. Tổng số lượt chuyển khám ";
                moi.Chiso = "8b. Chuyển khám BV chuyên khoa ( do không thuộc chức năng nhiệm vụ chủa BV)";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 8;
                moi.Tennhom = "8. Tổng số lượt chuyển khám ";
                moi.Chiso = "8c. Chuyển khám vì lý do khác (không thuộc 2 TH trên)";
                _listContent.Add(moi);
                #endregion

                #region 9. Tổng số lượt điều trị ngoại trú, điều trị ban ngày
                var q9_1 = (from a in benhnhan.Where(p => p.NoiTru == 0).Where(p => p.DTNT == true) join rv in data.RaViens on a.MaBNhan equals rv.MaBNhan select new { a, rv.SoNgaydt }).ToList();
                moi = new Content();
                moi.SttNhom = 9;
                moi.Tennhom = "9. Tổng số lượt người bệnh điều trị ngoại trú, điều trị ban ngày ";
                moi.Chiso = "";
                moi.SLNamSau = q9_1.Count;
                _listContent.Add(moi);
                #endregion

                #region 10. Tổng số ngày điều trị ngoại trú, điều trị ban ngày

                moi = new Content();
                moi.SttNhom = 10;
                moi.Tennhom = "10. Tổng số ngày điều trị của người bệnh điều trị ngoại trú, điều trị ban ngày. (Trong suốt đợt điều trị, mỗi lần bệnh nhân quay lại Bv xử trí được tính 1 ngày";
                moi.Chiso = "";
                moi.SLNamSau = q9_1.Sum(p => p.SoNgaydt);
                _listContent.Add(moi);
                #endregion

                #region 11. Tổng số lượt điều trị nội trú
                //  var q11_a = benhnhan.Where(p=>p.NoiTru == 1).ToList();
                //moi = new Content();
                //moi.SttNhom = 11;
                //moi.Tennhom = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (11 = 11a + 11b + 11c + 11d):";
                //moi.Chiso = "";
                //moi.SLNamSau = bn.Count; ;
                //_listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 11;
                moi.Tennhom = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (11 = 11a + 11b + 11c + 11d):";
                moi.Chiso = "11a. Tổng số lượt điều trị nội trú thu viện phí trực tiếp.";
                moi.SLNamSau = bnThanhToanTT_NoiTru.Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 11;
                moi.Tennhom = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (11 = 11a + 11b + 11c + 11d):";
                moi.Chiso = "11b. Tổng số lượt người điều trị nội trú được BHYT chi trả ( các đối tượng có thẻ BHYT)";
                moi.SLNamSau = bnCoThe_NoiTru.Count();
                _listContent.Add(moi);

                _listContent.Add(new Content { SttNhom = 11, Tennhom = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (11 = 11a + 11b + 11c + 11d):", Chiso = "11c. Tổng số lượt người điều trị cho người nghèo (không có thẻ BHYT, hoặc có thẻ khám chữa bệnh cho người nghèo được quyết toán theo thực thanh thực chi)" });
                _listContent.Add(new Content { SttNhom = 11, Tennhom = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (11 = 11a + 11b + 11c + 11d):", Chiso = "11d. Tổng số lượt người bệnh điều trị nội trú được miễn viện phí do BV quyết định" });
                _listContent.Add(new Content { SttNhom = 11, Tennhom = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (11 = 11a + 11b + 11c + 11d):", Chiso = "11đ. Tổng số lượt người điều trị nội trú được giảm do BV quyết định" });


                #endregion

                #region 12. Tổng số lượt người bệnh điều trị nội trú bằng YHCT, hoặc có kết hợp YHCT
               // var kpdongy = (from kp in data.KPhongs join ck in data.ChuyenKhoas.Where(p => p.TenCK == "Đông y") on kp.MaCK equals ck.MaCK select kp).ToList();
                var q12 = (from bn in q0.Where(p => p.NoiTru == 1) join ck in data.ChuyenKhoas.Where(p => p.TenCK == "Đông y") on bn.MaCK equals ck.MaCK select bn).ToList();
                moi = new Content();
                moi.SttNhom = 12;
                moi.Tennhom = "12. Tổng số lượt người bệnh điều trị nội trú bằng YHCT, hoặc có kết hợp YHCT";
                moi.Chiso = "";
                moi.SLNamSau = q12.Count;
                _listContent.Add(moi);
                #endregion

                #region 13. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú
                var q13_1 = bnCoThe_NoiTru.Where(p => p.Tuoi == null || p.Tuoi < 6).ToList();
                moi = new Content();
                moi.SttNhom = 13;
                moi.Tennhom = "13. Tổng số trẻ em dưới 6 tuổi điều trị nội trú";
                moi.Chiso = "Trong đó:" + Environment.NewLine + "13a. Số lượt điều trị cho trẻ em dưới 6 tuổi có thẻ BHYT, hoặc thẻ kkhám chữa bệnh cho trẻ em dưới 6 tuổi";
                moi.SLNamSau = q13_1.Count;
                _listContent.Add(moi);

                var q13_2 = bnThanhToanTT_NoiTru.Where(p => p.Tuoi == null || p.Tuoi < 6).ToList();
                moi = new Content();
                moi.SttNhom = 13;
                moi.Tennhom = "13. Tổng số trẻ em dưới 6 tuổi điều trị nội trú";
                moi.Chiso = "13b. Số lượt điều trị cho trẻ em dưới 6 tuổi thu phí trực tiếp";
                moi.SLNamSau = q13_2.Count; ;
                _listContent.Add(moi);
                #endregion

                #region 14. Tổng số lượt điều trị người bệnh cao tuổi (>=60 tuổi)
                var q14_1 = bnCoThe_NoiTru.Where(p => p.Tuoi >= 60).ToList();
                moi = new Content();
                moi.SttNhom = 14;
                moi.Tennhom = "14. Tổng số lượt điều trị người bệnh cao tuổi (>=60 tuổi)";
                moi.Chiso = "Trong đó:" + Environment.NewLine + "14a. Số lượt điều trị cho người cao tuổi có thẻ BHYT, hoặc thẻ chính sách khác được miễn giảm viện phí";
                moi.SLNamSau = q14_1.Count; ;
                _listContent.Add(moi);

                var q14_2 = bnThanhToanTT_NoiTru.Where(p => p.Tuoi >= 60).ToList();
                moi = new Content();
                moi.SttNhom = 14;
                moi.Tennhom = "14. Tổng số lượt điều trị người bệnh cao tuổi (>=60 tuổi)";
                moi.Chiso = "14b. Số lượt điều trị người tuổi thu phí trực tiếp";
                moi.SLNamSau = q14_2.Count; ;
                _listContent.Add(moi);
                #endregion

                #region 15. Tổng số lượt điều trị cho người nước ngoài
                var q15_1 = (from bn in benhnhan.Where(p => p.NoiTru == 1) join ttbx in data.TTboXungs.Where(p => p.NgoaiKieu != "" && p.NgoaiKieu != "Việt Nam" && p.NgoaiKieu != "VN" && p.NgoaiKieu != null) on bn.MaBNhan equals ttbx.MaBNhan select bn).ToList();
                moi = new Content();
                moi.SttNhom = 15;
                moi.Tennhom = "15. Tổng số lượt điều trị cho người nước ngoài ";
                moi.Chiso = "";
                moi.SLNamSau = q15_1.Count;
                _listContent.Add(moi);
                #endregion

                #region 16. Kết quả điều trị nội trú
                var q16 = benhnhan.Where(p => p.NoiTru == 1).ToList();
                moi = new Content();
                moi.SttNhom = 16;
                moi.Tennhom = "16. Kết quả điều trị nội trú";
                moi.Chiso = "16a. Số lượt người bệnh được điều trị khỏi";
                moi.SLNamSau = q16.Where(p => p.KetQua == "Khỏi").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 16;
                moi.Tennhom = "16. Kết quả điều trị nội trú";
                moi.Chiso = "16b. Số lượt người bệnh đỡ/giảm";
                moi.SLNamSau = q16.Where(p => p.KetQua == "Đỡ|Giảm").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 16;
                moi.Tennhom = "16. Kết quả điều trị nội trú";
                moi.Chiso = "16c. Số lượt người bệnh kết quả điều trị không thay đổi";
                moi.SLNamSau = q16.Where(p => p.KetQua == "Không T.đổi").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 16;
                moi.Tennhom = "16. Kết quả điều trị nội trú";
                moi.Chiso = "16d. Số lượt người bệnh nặng hơn";
                moi.SLNamSau = q16.Where(p => p.KetQua == "Nặng hơn").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 16;
                moi.Tennhom = "16. Kết quả điều trị nội trú";
                moi.Chiso = "16e. Số lượt người bệnh tiên lượng tử vong gia đình xin về";
                moi.SLNamSau = q16.Where(p => p.KetQua == "Tử vong").Count();
                _listContent.Add(moi);
                #endregion

                #region 17. Tổng số điều trị nội trú chuyển viện
                var q17_a = benhnhan.Where(p => p.NoiTru == 1).Where(p => p.Status == 1).ToList();
                moi = new Content();
                moi.SttNhom = 17;
                moi.Tennhom = "17. Tổng số điều trị nội trú chuyển viện";
                moi.Chiso = "";
                moi.SLNamSau = q17_a.Count; ;
                _listContent.Add(moi);

                _listContent.Add(new Content { SttNhom = 17, Tennhom = "17. Tổng số điều trị nội trú chuyển viện", Chiso = "17a. Chuyển bệnh viện tuyến trên" });
                _listContent.Add(new Content { SttNhom = 17, Tennhom = "17. Tổng số điều trị nội trú chuyển viện", Chiso = "17b. Chuyển bệnh viện chuyên khoa (không thuộc CNNV)" });
                _listContent.Add(new Content { SttNhom = 17, Tennhom = "17. Tổng số điều trị nội trú chuyển viện", Chiso = "17c. Chuyển tuyến dưới" });
                _listContent.Add(new Content { SttNhom = 17, Tennhom = "17. Tổng số điều trị nội trú chuyển viện", Chiso = "17e. Chuyển viện khác (không thuộc 3 trường hợp trên" });



                #endregion

                #region 18. Tổng số ngày điều trị của người bệnh nội trú

                moi = new Content();
                moi.SttNhom = 18;
                moi.Tennhom = "18. Tổng số ngày điều trị của người bệnh nội trú";
                moi.Chiso = "";
                moi.SLNamSau = q16.Sum(p => p.SoNgaydt);
                _listContent.Add(moi);
                #endregion

                #region 19. Số ngày điều trị trung bình của người bệnh nội trú
                moi = new Content();
                moi.SttNhom = 19;
                moi.Tennhom = "19. Số ngày điều trị trung bình của người bệnh nội trú";
                moi.Chiso = "";
                moi.SLNamSau = Math.Round((float)q16.Sum(p => p.SoNgaydt) / q16.Count, 2);
                _listContent.Add(moi);
                #endregion

                #region 20. Tổng số người bệnh tử vong tại BV (20 = 20a + 20b)
                var q20 = benhnhan.Where(p => p.KetQua == "Tử vong").ToList();
                moi = new Content();
                moi.SttNhom = 20;
                moi.Tennhom = "20. Tổng số người bệnh tử vong tại BV (20 = 20a + 20b)";
                moi.Chiso = "";
                moi.SLNamSau = q20.Count();
                _listContent.Add(moi);

                _listContent.Add(new Content { SttNhom = 20, Tennhom = "20. Tổng số người bệnh tử vong tại BV (20 = 20a + 20b)", Chiso = "20a. Số tử vong trong vòng 24 giờ đầu nhập viện" });
                _listContent.Add(new Content { SttNhom = 20, Tennhom = "20. Tổng số người bệnh tử vong tại BV (20 = 20a + 20b)", Chiso = "20a. Số tử vong sau 24 giờ đầu nhập viện" });
                #endregion

                #region 21. Tổng số phẫu thuật thực hiện tại bệnh viện
                var q21 = (from cd in data.ChiDinhs.Where(p => p.Status == 1)
                           join cls in data.CLS.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT) on cd.IdCLS equals cls.IdCLS
                           join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan// có thể ko cần
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new { tn.TenRG, dv.Loai }).ToList();
                var qtt= (from dt in data.DThuocs
                          join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan// có thể ko cần
                          join dtct in data.DThuoccts.Where(p=>p.NgayNhap >= tungayHT && p.NgayNhap <= denngayHT).Where(p=>p.IDCD == null) on dt.IDDon equals dtct.IDDon 
                              join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                          join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Phẫu thuật" || p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                          select new {tn.TenRG, dv.Loai }).ToList();
            //    q21.Union(qtt);

                moi = new Content();
                moi.SttNhom = 21;
                moi.Tennhom = "21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG.ToLower() == "phẫu thuật").Count() + qtt.Where(p => p.TenRG.ToLower() == "phẫu thuật").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 21;
                moi.Tennhom = "21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)";
                moi.Chiso = "21a. Số phẫu thuật loại đặc biệt";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 21;
                moi.Tennhom = "21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)";
                moi.Chiso = "21b. Số phẫu thuật loại 1";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 21;
                moi.Tennhom = "21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)";
                moi.Chiso = "21c. Số phẫu thuật loại 2";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 21;
                moi.Tennhom = "21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)";
                moi.Chiso = "21d. Số phẫu thuật loại 3";
                _listContent.Add(moi);

                #endregion

                #region 21.Trong đó

                moi = new Content();
                moi.SttNhom = 22;
                moi.Tennhom = "Trong đó" + Environment.NewLine + "  Tổng số phẫu thuật loại 3 trở lên thu phí toàn bộ:";
                moi.Chiso = "- Số phẫu thuật loại đặc biệt thu phí toàn bộ";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 22;
                moi.Tennhom = "Trong đó" + Environment.NewLine + "  Tổng số phẫu thuật loại 3 trở lên thu phí toàn bộ:";
                moi.Chiso = "- Số phẫu thuật loại 1 thu phí toàn bộ";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 22;
                moi.Tennhom = "Trong đó" + Environment.NewLine + "  Tổng số phẫu thuật loại 3 trở lên thu phí toàn bộ:";
                moi.Chiso = "- Số phẫu thuật loại 2 thu phí toàn bộ";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 22;
                moi.Tennhom = "Trong đó" + Environment.NewLine + "  Tổng số phẫu thuật loại 3 trở lên thu phí toàn bộ:";
                moi.Chiso = "- Số phẫu thuật loại 3 thu phí toàn bộ";
                _listContent.Add(moi);
                #endregion

                #region 22. Phân tích cơ cấu phẫu thuật

                moi = new Content();
                moi.SttNhom = 23;
                moi.Tennhom = "22. Phân tích cơ cấu phẫu thuật";
                moi.Chiso = "22a. Số phẫu thuật nội soi";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 23;
                moi.Tennhom = "22. Phân tích cơ cấu phẫu thuật";
                moi.Chiso = "22b. Số phẫu thuật vi phẫu";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 23;
                moi.Tennhom = "22. Phân tích cơ cấu phẫu thuật";
                moi.Chiso = "22c. Số phẫu thuật la-ze";
                _listContent.Add(moi);


                #endregion

                #region 23. Tổng số thủ thuật thực hiện tại BV

                moi = new Content();
                moi.SttNhom = 24;
                moi.Tennhom = "23. Tổng số thủ thuật thực hiện tại BV";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "Thủ thuật").Count() + qtt.Where(p=>p.TenRG == "Thủ thuật").Count();
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 24;
                moi.Tennhom = "23. Tổng số thủ thuật thực hiện tại BV";
                moi.Chiso = "23a. Số thủ thuật loại đặc biệt";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 24;
                moi.Tennhom = "23. Tổng số thủ thuật thực hiện tại BV";
                moi.Chiso = "23b. Số thủ thuật loại 1";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 24;
                moi.Tennhom = "23. Tổng số thủ thuật thực hiện tại BV";
                moi.Chiso = "23c. Số thủ thuật loại 2";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 24;
                moi.Tennhom = "23. Tổng số thủ thuật thực hiện tại BV";
                moi.Chiso = "23d. số thủ thuật loại 3";
                _listContent.Add(moi);
                #endregion

                #region 24. Tổng số ca đẻ/sinh tại BV (bao gồm cả đẻ /sinh thường và can thiệp)

                moi = new Content();
                moi.SttNhom = 25;
                moi.Tennhom = "24. Tổng số ca đẻ/sinh tại BV (bao gồm cả đẻ /sinh thường và can thiệp)" + Environment.NewLine + "Trong đó";
                moi.Chiso = "24a. Số ca phẫu thuật lấy thai";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 25;
                moi.Tennhom = "24. Tổng số ca đẻ/sinh tại BV (bao gồm cả đẻ /sinh thường và can thiệp)" + Environment.NewLine + "Trong đó";
                moi.Chiso = "24b. Số ca tử vong mẹ";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 25;
                moi.Tennhom = "24. Tổng số ca đẻ/sinh tại BV (bao gồm cả đẻ /sinh thường và can thiệp)" + Environment.NewLine + "Trong đó";
                moi.Chiso = "24c. Số ca tử vong trẻ sơ sinh";
                _listContent.Add(moi);
                #endregion

                #region 25. Tổng số lượng máu đã sử dụng tại BV (đơn vị tính = lít)

                moi = new Content();
                moi.SttNhom = 26;
                moi.Tennhom = "25.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
                moi.Chiso = "25a. Số lượng máu tiếp nhận từ người hiến máu tình nguyện (đơn vị tính = lít)";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 26;
                moi.Tennhom = "25.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
                moi.Chiso = "25b. Số lượng máu tiếp nhận từ các trung tâm Huyết học truyền máu (đơn vị tính = lít)";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 26;
                moi.Tennhom = "25.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
                moi.Chiso = "25c. Số lượng máu tiếp nhận từ các nguồn khác (người nhà, tự thân, người cho máu .v.v.) (đơn vị tính = lít)";
                _listContent.Add(moi);
                #endregion

                #region 26. Tổng số xét nghiệm về Sinh hóa
                moi = new Content();
                moi.SttNhom = 27;
                moi.Tennhom = "26. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (26 = 26a + 26b + 26c)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "XN hóa sinh máu").Count();
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 27;
                moi.Tennhom = "26. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (26 = 26a + 26b + 26c)";
                moi.Chiso = "26a. Số XN sinh hóa cho người bệnh nội trú";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 27;
                moi.Tennhom = "26. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (26 = 26a + 26b + 26c)";
                moi.Chiso = "26b. Số XN sinh hóa cho NB khám và điều trị ngoại trú";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 27;
                moi.Tennhom = "26. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (26 = 26a + 26b + 26c)";
                moi.Chiso = "26c. Số XN sinh hóa phục vụ những đối tượng không khám, chữa bệnh tại Bv, Khám sức khỏe, NCKH.";
                _listContent.Add(moi);
                #endregion

                #region 27. Tổng số xét nghiệm huyết học thực hiện tại BV
                moi = new Content();
                moi.SttNhom = 28;
                moi.Tennhom = "27. Tổng số xét nghiệm về Huyết học thực hiện tại BV (27 = 27a + 27b + 27c)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "XN huyết học").Count();
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 28;
                moi.Tennhom = "27. Tổng số xét nghiệm về Huyết học thực hiện tại BV (27 = 27a + 27b + 27c)";
                moi.Chiso = "27a. Số XN huyết học cho người bệnh nội trú";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 28;
                moi.Tennhom = "27. Tổng số xét nghiệm về Huyết học thực hiện tại BV (27 = 27a + 27b + 27c)";
                moi.Chiso = "27b. Số XN huyết học cho NB khám và điều trị ngoại trú";
                _listContent.Add(moi);
                moi = new Content();
                moi.SttNhom = 28;
                moi.Tennhom = "27. Tổng số xét nghiệm về Huyết học thực hiện tại BV (27 = 27a + 27b + 27c)";
                moi.Chiso = "27c. Số XN Huyết học phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe, NCKH";
                _listContent.Add(moi);

                #endregion

                #region 28. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (28 = 28a + 28b + 28c)


                moi = new Content();
                moi.SttNhom = 29;
                moi.Tennhom = "28. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (28 = 28a + 28b + 28c)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "XN vi sinh").Count();// chưa có
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 29;
                moi.Tennhom = "28. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (28 = 28a + 28b + 28c)";
                moi.Chiso = "28a. Số XN về Vi sinh cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 29;
                moi.Tennhom = "28. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (28 = 28a + 28b + 28c)";
                moi.Chiso = "28b. Số XN về Vi sinh cho người bệnh khám và điều trị ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 29;
                moi.Tennhom = "28. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (28 = 28a + 28b + 28c)";
                moi.Chiso = "28b. Số XN về Vi sinh cho những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH.";
                _listContent.Add(moi);
                #endregion

                #region 29. Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (29 = 29a + 29b + 29c)
                moi = new Content();
                moi.SttNhom = 30;
                moi.Tennhom = "29.  Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (29 = 29a + 29b + 29c)";
                moi.Chiso = "29a. Số Giải phẫu bệnh lý cho người bệnh nội trú";
                // moi.SLNamSau = q21.Where(p => p.TenRG == "XN vi sinh").Count();// chưa có
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 29;
                moi.Tennhom = "29.  Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (29 = 29a + 29b + 29c)";
                moi.Chiso = "29b. Số Giải phẫu bệnh lý cho người bệnh khám và điều trị ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 29;
                moi.Tennhom = "29.  Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (29 = 29a + 29b + 29c)";
                moi.Chiso = "29b. Số Giải phẫu bệnh lý cho những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH.";
                _listContent.Add(moi);
                #endregion

                #region 30. Tổng số chụp X Quang (30 = 30a + 30b + 30c) mỗi vị trí được tính 1 film
                moi = new Content();
                moi.SttNhom = 31;
                moi.Tennhom = "30. Tổng số chụp X Quang (30 = 30a + 30b + 30c) mỗi vị trí được tính 1 film";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "X-Quang").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 31;
                moi.Tennhom = "30. Tổng số chụp X Quang (30 = 30a + 30b + 30c) mỗi vị trí được tính 1 film";
                moi.Chiso = "30a. Số chụp XQ cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 31;
                moi.Tennhom = "30. Tổng số chụp X Quang (30 = 30a + 30b + 30c) mỗi vị trí được tính 1 film";
                moi.Chiso = "30b. Số chụp XQ cho người bệnh khám và ĐT ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 31;
                moi.Tennhom = "30. Tổng số chụp X Quang (30 = 30a + 30b + 30c) mỗi vị trí được tính 1 film";
                moi.Chiso = "30c. Số chụp XQ phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
                _listContent.Add(moi);


                #endregion

                #region 31. Tổng số chụp CT Scan (31 = 31a + 31b +31c)
                moi = new Content();
                moi.SttNhom = 32;
                moi.Tennhom = "31. Tổng số chụp CT Scan (31 = 31a + 31b +31c)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "X-Quang CT").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 32;
                moi.Tennhom = "31. Tổng số chụp CT Scan (31 = 31a + 31b +31c)";
                moi.Chiso = "31a. Số chụp CT Scan cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 32;
                moi.Tennhom = "31. Tổng số chụp CT Scan (31 = 31a + 31b +31c)";
                moi.Chiso = "31b. Số chụp CT Scan cho người bệnh khám và ĐT ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 32;
                moi.Tennhom = "31. Tổng số chụp CT Scan (31 = 31a + 31b +31c)";
                moi.Chiso = "31c. Số chụp CT Scan phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
                _listContent.Add(moi);


                #endregion

                #region 32. Tổng số chụp MRI (32 = 32a + 32b +32c)

                moi = new Content();
                moi.SttNhom = 33;
                moi.Tennhom = "32. Tổng số chụp MRI (32 = 32a + 32b +32c)";
                moi.Chiso = "32a. Số chụp MRI cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 33;
                moi.Tennhom = "32. Tổng số chụp MRI (32 = 32a + 32b +32c)";
                moi.Chiso = "32b. Số chụp MRI cho người bệnh khám và ĐT ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 33;
                moi.Tennhom = "32. Tổng số chụp MRI (32 = 32a + 32b +32c)";
                moi.Chiso = "32c. Số chụp MRI phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
                _listContent.Add(moi);


                #endregion

                #region 33. Tổng số chụp Pet/CT (33 = 33a + 33b +33c)

                moi = new Content();
                moi.SttNhom = 34;
                moi.Tennhom = "33. Tổng số chụp Pet/CT (33 = 33a + 33b +33c)";
                moi.Chiso = "33a. Số chụp Pet/CT cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 34;
                moi.Tennhom = "33. Tổng số chụp Pet/CT (33 = 33a + 33b +33c)";
                moi.Chiso = "33b. Số chụp Pet/CT cho người bệnh khám và ĐT ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 34;
                moi.Tennhom = "33. Tổng số chụp Pet/CT (33 = 33a + 33b +33c)";
                moi.Chiso = "33c. Số chụp Pet/CT phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
                _listContent.Add(moi);


                #endregion

                #region 34. Tổng số siêu âm (34 = 34a + 34b +34c)
                moi = new Content();
                moi.SttNhom = 35;
                moi.Tennhom = "34. Tổng số siêu âm chẩn đoán và điều trị (34 = 34a + 34b +34c)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG.Contains("Siêu âm")).Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 35;
                moi.Tennhom = "34. Tổng số siêu âm chẩn đoán và điều trị (34 = 34a + 34b +34c)";
                moi.Chiso = "34a. Số siêu âm cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 35;
                moi.Tennhom = "34. Tổng số siêu âm chẩn đoán và điều trị (34 = 34a + 34b +34c)";
                moi.Chiso = "34b. Số siêu âm cho người bệnh khám và ĐT ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 35;
                moi.Tennhom = "34. Tổng số siêu âm chẩn đoán và điều trị (34 = 34a + 34b +34c)";
                moi.Chiso = "34c. Số siêu âm phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
                _listContent.Add(moi);


                #endregion

                #region 35. Tổng số nội soi (35 = 35a + 35b +35c)
                moi = new Content();
                moi.SttNhom = 36;
                moi.Tennhom = "35. Tổng số nội soi chẩn đoán và can thiệp (35 = 35a + 35b +35c)";
                moi.Chiso = "";
                moi.SLNamSau = q21.Where(p => p.TenRG == "Nội soi").Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 36;
                moi.Tennhom = "35. Tổng số nội soi chẩn đoán và can thiệp (35 = 35a + 35b +35c)";
                moi.Chiso = "35a. Số nội soi cho người bệnh nội trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 36;
                moi.Tennhom = "35. Tổng số nội soi chẩn đoán và can thiệp (35 = 35a + 35b +35c)";
                moi.Chiso = "35b. Số nội soi cho người bệnh khám và ĐT ngoại trú";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 36;
                moi.Tennhom = "35. Tổng số nội soi chẩn đoán và can thiệp (35 = 35a + 35b +35c)";
                moi.Chiso = "35c. Số nội soi phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
                _listContent.Add(moi);

                #endregion

                #region 36. Tổng số tai biến trong điều trị phát hiện được

                moi = new Content();
                moi.SttNhom = 37;
                moi.Tennhom = "36. Tổng số tai biến trong điều trị phát hiện được (36 = 36a + 36b + 36c + 36d +36đ)";
                moi.Chiso = "36a. Số tai biến do sử dụng thuốc";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 37;
                moi.Tennhom = "36. Tổng số tai biến trong điều trị phát hiện được (36 = 36a + 36b + 36c + 36d +36đ)";
                moi.Chiso = "- Số tai biến do phản ứng có hại của thuốc (ADR)";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 37;
                moi.Tennhom = "36. Tổng số tai biến trong điều trị phát hiện được (36 = 36a + 36b + 36c + 36d +36đ)";
                moi.Chiso = "36b. Số tai biến do truyền máu";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 37;
                moi.Tennhom = "36. Tổng số tai biến trong điều trị phát hiện được (36 = 36a + 36b + 36c + 36d +36đ)";
                moi.Chiso = "36c. Số tai biến do phẫu thuật";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 37;
                moi.Tennhom = "36. Tổng số tai biến tỏng điều trị phát hiện được (36 = 36a + 36b + 36c + 36d +36đ)";
                moi.Chiso = "36d. Số tai biến do thủ thuật";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 37;
                moi.Tennhom = "36. Tổng số tai biến tỏng điều trị phát hiện được (36 = 36a + 36b + 36c + 36d +36đ)";
                moi.Chiso = "36đ. Số tai biến khác (ghi cụ thể)";
                _listContent.Add(moi);




                #endregion

                #region 37. Tổng số tai biến sản, phụ khoa

                moi = new Content();
                moi.SttNhom = 38;
                moi.Tennhom = "37. Tổng số tai biến sản, phụ khoa";
                moi.Chiso = "";
                _listContent.Add(moi);

                #endregion

                #region 38. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)

                moi = new Content();
                moi.SttNhom = 39;
                moi.Tennhom = "38. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
                moi.Chiso = "38a. Số kỹ thuật lâm sàng mới được BV tuyến trên về chuyển giao tại BV";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 39;
                moi.Tennhom = "38. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
                moi.Chiso = "38b. Số kỹ thuật lâm sàng mới do BV cử cán bộ đi học về triển khai";
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 39;
                moi.Tennhom = "38. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
                moi.Chiso = "38c. Kể tên những kỹ thuật lâm sàng MỚI được triển khai trong năm " + cbNam.Text;
                _listContent.Add(moi);

                #endregion
            }
            
            #endregion
            #region Kết quả KCB 6 tháng đầu năm
            else
            {

                Content moi = new Content();
                _listContent.Add(new Content { SttNhom = 1, Tennhom = "1. Tổng số giường kế hoạch", Chiso = "" });
                _listContent.Add(new Content { SttNhom = 2, Tennhom = "2. Tổng số giường thực kê", Chiso = "" });
                _listContent.Add(new Content { SttNhom = 3, Tennhom = "3. Công suất sử dụng giường bệnh (theo KH)", Chiso = "" });               

                #region 4. Lấy tổng số lượt khám bệnh

                moi.SttNhom = 4;
                moi.Tennhom = "4. Tổng số lượt khám bệnh";
                moi.Chiso = "Trong đó:";                
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 4;
                moi.Tennhom = "4. Tổng số lượt khám bệnh";
                moi.Chiso = "+ Khám cho người ≥ 60 tuổi";
                moi.SLNamTruoc = benhnhan.Where(p=>p.NoiTru == 0).Where(p=>p.Tuoi >= 60).Count();
                moi.SLNamSau = bnCoThe_Kb.Where(p => p.Tuoi >= 60).Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 4;
                moi.Tennhom = "4. Tổng số lượt khám bệnh";
                moi.Chiso = "+ Khám cho trẻ em < 6 tuổi";
                moi.SLNamTruoc = benhnhan.Where(p=>p.NoiTru == 0).Where(p=>p.Tuoi == null || p.Tuoi < 6).Count();
                moi.SLNamSau = bnCoThe_Kb.Where(p=>p.Tuoi == null || p.Tuoi < 6).Count();              
                _listContent.Add(moi);
                #endregion

                #region 5. Tổng số lượt điều trị nội trú
                moi = new Content();
                moi.SttNhom = 5;
                moi.Tennhom = "5. Tổng số lượt điều trị nội trú";
                moi.Chiso = "Trong đó:";
               _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 5;
                moi.Tennhom = "5. Tổng số lượt điều trị nội trú";
                moi.Chiso = "+ Điều trị cho người ≥ 60 tuổi";
                moi.SLNamTruoc = benhnhan.Where(p => p.NoiTru == 1).Where(p => p.Tuoi >= 60).Count();
                moi.SLNamSau = bnCoThe_NoiTru.Where(p => p.Tuoi >= 60).Count();                
                _listContent.Add(moi);
              
                moi = new Content();
                moi.SttNhom = 5;
                moi.Tennhom = "5. Tổng số lượt điều trị nội trú";
                moi.Chiso = "Điều trị cho trẻ em < 6 tuổi";
                moi.SLNamTruoc = benhnhan.Where(p => p.NoiTru == 1).Where(p => p.Tuoi == null || p.Tuoi < 6).Count();
                moi.SLNamSau = bnCoThe_NoiTru.Where(p => p.Tuoi == null || p.Tuoi < 6).Count();   
                _listContent.Add(moi);
                #endregion

                #region 6. Số lượt BN chuyển viện             
                moi = new Content();
                moi.SttNhom = 6;
                moi.Tennhom = "6. Tổng số BN chuyển viện ";
                moi.Chiso = "Trong đó";               
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 6;
                moi.Tennhom = "6. Tổng số BN chuyển viện ";
                moi.Chiso = "Chuyển viện ngoại trú: ";
                moi.SLNamTruoc = benhnhan.Where(p => p.NoiTru == 0).Where(p =>p.Status ==1).Count();
                moi.SLNamSau = bnCoThe_Kb.Where(p => p.Status == 1).Count();   
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 6;
                moi.Tennhom = "6. Tổng số BN chuyển viện ";
                moi.Chiso = "Chuyển viện nộ trú: ";
                moi.SLNamTruoc = benhnhan.Where(p => p.NoiTru == 1).Where(p => p.Status == 1).Count();
                moi.SLNamSau = bnCoThe_NoiTru.Where(p => p.Status == 1).Count();
                _listContent.Add(moi);
                #endregion

                #region 7. Tỷ lệ chuyển viện chung                
                moi = new Content();
                moi.SttNhom = 7;
                moi.Tennhom = "7. Tỷ lệ chuyển viện chung";
                moi.Chiso = "+ Tỷ lệ chuyển viện ngoại trú";               
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 7;
                moi.Tennhom = "7. Tỷ lệ chuyển viện chung";
                moi.Chiso = "+ Tỷ lệ chuyển viện nội trú";
                _listContent.Add(moi);
                #endregion

                #region 8. Tổng số ngày điều trị ngoại trú, điều trị ban ngày
                moi = new Content();
                moi.SttNhom = 8;
                moi.Tennhom = "Tổng số ngày điều trị nội trú";
                moi.Chiso = "";
                moi.SLNamTruoc  = benhnhan.Where(p=>p.NoiTru == 1).Sum(p => p.SoNgaydt);
                moi.SLNamSau = bnCoThe_NoiTru.Sum(p => p.SoNgaydt);
                _listContent.Add(moi);
                #endregion

                #region 9. Số ngày điều tị trung bình               
                moi = new Content();
                moi.SttNhom = 9;
                moi.Tennhom = "9. Số ngày điều trị trung bình";
                moi.Chiso = "";               
                _listContent.Add(moi);
                #endregion

                #region 10. Tổng số phẫu thuật thực hiện tại bệnh viện
                var q21 = (from cd in data.ChiDinhs.Where(p => p.Status == 1)
                           join cls in data.CLS.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT) on cd.IdCLS equals cls.IdCLS
                           join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan// có thể ko cần
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new {cls.MaBNhan, tn.TenRG, dv.Loai }).ToList();
                var qtt = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT)
                           join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan// có thể ko cần
                           join dtct in data.DThuoccts.Where(p => p.IDCD == null) on dt.IDDon equals dtct.IDDon
                           join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                           join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Phẫu thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new {dt.MaBNhan, tn.TenRG, dv.Loai }).ToList();
              // q21.Union(qtt);

                var q21bh = (from dv in q21 join bn in benhnhan.Where(p => p.SThe != null && p.SThe != "") on dv.MaBNhan equals bn.MaBNhan select dv).ToList();
                var qttbh = (from dv in qtt join bn in benhnhan.Where(p => p.SThe != null && p.SThe != "") on dv.MaBNhan equals bn.MaBNhan select dv).ToList();
                moi = new Content();
                moi.SttNhom = 10;
                moi.Tennhom = "10. Tổng số phẫu thuật:";
                moi.Chiso = "";
                moi.SLNamTruoc = q21.Where(p => p.TenRG.ToLower() == "phẫu thuật").Where(p => p.Loai != 4 && p.Loai != 1).Count() + qtt.Where(p => p.Loai != 4 && p.Loai != 1).Count();
                moi.SLNamSau = q21bh.Where(p => p.TenRG.ToLower() == "phẫu thuật").Where(p => p.Loai != 4 && p.Loai != 1).Count() + qttbh.Where(p => p.Loai != 4 && p.Loai != 1).Count();
                _listContent.Add(moi);

                moi = new Content();
                moi.SttNhom = 10;
                moi.Tennhom = "10. Tổng số phẫu thuật:";
                moi.Chiso = "+ Trong đó phẫu thuật loại I trở lên";
                moi.SLNamTruoc = q21.Where(p => p.TenRG.ToLower() == "phẫu thuật").Where(p => p.Loai == 4 && p.Loai == 1).Count() + qtt.Where(p => p.Loai == 4 && p.Loai == 1).Count();
                moi.SLNamSau = q21bh.Where(p => p.TenRG.ToLower() == "phẫu thuật").Where(p => p.Loai == 4 && p.Loai == 1).Count() + qttbh.Where(p => p.Loai == 4 && p.Loai == 1).Count();
                _listContent.Add(moi);
                #endregion

                #region 11. Tổng số ca đẻ
                moi = new Content();
                moi.SttNhom = 11;
                moi.Tennhom = "11. Tổng số ca đẻ:";
                moi.Chiso = "+ Trong đó phẫu thuật lấy thai";
                _listContent.Add(moi);
                #endregion

                #region 12. Tổng số siêu âm
                moi = new Content();
                moi.SttNhom = 12;
                moi.Tennhom = "12. Tổng số lần siêu âm";
                moi.Chiso = "";
                moi.SLNamTruoc = q21.Where(p => p.TenRG.Contains("Siêu âm")).Count();
                moi.SLNamSau = q21bh.Where(p => p.TenRG.Contains("Siêu âm")).Count();
                _listContent.Add(moi);

               
                #endregion

                #region 13. Tổng số chụp X Quang 
                moi = new Content();
                moi.SttNhom = 13;
                moi.Tennhom = "13. Tổng số lần chụp X Quang";
                moi.Chiso = "";
                moi.SLNamTruoc = q21.Where(p => p.TenRG == "X-Quang").Count();
                moi.SLNamSau = q21bh.Where(p => p.TenRG == "X-Quang").Count();
                _listContent.Add(moi);
                #endregion

                #region 14. Tổng số chụp CT Scan 
                moi = new Content();
                moi.SttNhom = 14;
                moi.Tennhom = "14. Tổng số chụp CT Scanner";
                moi.Chiso = "";
                moi.SLNamTruoc = q21.Where(p => p.TenRG == "X-Quang CT").Count();
                moi.SLNamSau = q21bh.Where(p => p.TenRG == "X-Quang CT").Count();
                _listContent.Add(moi);
                #endregion

                #region 15. Tổng số lần nội soi tiêu hóa
                moi = new Content();
                moi.SttNhom = 15;
                moi.Tennhom = "15. Tổng số lần nội soi tiêu hóa";
                moi.Chiso = "";
                moi.SLNamTruoc = q21.Where(p => p.TenRG == "Nội soi").Count();
                moi.SLNamSau = q21bh.Where(p => p.TenRG == "Nội soi").Count();
                _listContent.Add(moi);
                #endregion

                #region 16. Số kỹ thuật mới được triển khai (Ghi cụ thể:...)
                moi = new Content();
                moi.SttNhom = 16;
                moi.Tennhom = "16. Số kỹ thuật mới được triển khai (Ghi cụ thể:...)";
                moi.Chiso = "";
                _listContent.Add(moi);

                #endregion
            }
            #endregion

            BaoCao.Rep_ThongtinveHDCMcuaBV_24009 rep = new BaoCao.Rep_ThongtinveHDCMcuaBV_24009();
            frmIn frm = new frmIn();
            if (ckIn6thang.Checked)
            {
                if(rd6Thang.SelectedIndex == 0)
                rep.lblTit.Text = "Bảng1: Một số kết quả KCB 6 tháng đầu năm " + cbNam.Text;
                else
                    rep.lblTit.Text = "Bảng1: Một số kết quả KCB 6 tháng cuối năm " + cbNam.Text;
                rep.celChiSo.Text = "Chỉ tiêu";
                rep.celNamTruocTit.Text = "Tổng số";
                rep.celNamSauTit.Text = "Trong đó: Người bệnh có thẻ BHYT";
                rep.celSoSanh.Text =  "So sánh cùng kỳ năm" + (Convert.ToInt32(cbNam.SelectedValue) - 1).ToString();
                rep.ckSoSanh.Visible = false;
                
            }
            else
            {
                rep.celNamSauTit.Text = cbNam.Text;
                rep.celNamTruocTit.Text = (Convert.ToInt32(cbNam.SelectedValue) - 1).ToString();
            }
            rep.DataSource = _listContent;           
            rep.BindingData();
            rep.CreateDocument();        
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        class Content
        {
            public int SttNhom{set;get;}
            public string Tennhom { set; get; }
            public string Chiso { set; get; }
            public int? SLNamTruoc { set; get; }
            public double? SLNamSau { set; get; }
            public double? Tyle { set; get; }
        }

        private void ckIn6thang_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (ckIn6thang.Checked)
                rd6Thang.Enabled = false;
            else
                rd6Thang.Enabled = true;
        }
    }
}