using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLBV_Database;

namespace QLBV.FormThamSo
{
    public partial class frm_BCBenhNhanNoiNgoaiTru_01071 : Form
    {
        QLBV_Database.QLBVEntities DataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_BCBenhNhanNoiNgoaiTru_01071()
        {
            InitializeComponent();
        }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }
        

        private void frm_BCBenhNhanNoiNgoaiTru_01071_Load(object sender, EventArgs e)
        {
            
            var q = (from KhoaPhong in DataContext.KPhongs
                     where (KhoaPhong.Status == 1 && (KhoaPhong.PLoai == ("Lâm sàng") || KhoaPhong.PLoai == ("Phòng khám")))
                     select KhoaPhong).OrderByDescending(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
            if (q.Count > 0)
            {
                List<ListKP> lkp = new List<ListKP>();
                ListKP allKP = new ListKP();
                allKP.MaKP = 0;
                allKP.TenKP = "Tất cả";
                lkp.Add(allKP);
                foreach (var item in q)
                {
                    ListKP kp = new ListKP();
                    kp.MaKP = item.MaKP;
                    kp.TenKP = item.TenKP;
                    lkp.Add(kp);
                }
                lupKhoaPhong.Properties.DataSource = lkp.ToList();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (lupKhoaPhong.EditValue == null)
            {
                MessageBox.Show("Chưa chọn khoa phòng!");
                return;
            }
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.Value);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.Value);
            int MaKP = Convert.ToInt32(lupKhoaPhong.EditValue);
            string TenKP = lupKhoaPhong.Text;
            int cot1BHYT, cot2BHYT, cot3BHYT, cot4BHYT, cot5BHYT, cot6BHYT, cot7BHYT, cot8BHYT, cot9BHYT, cot10BHYT, cot11BHYT, cot12BHYT, cot13BHYT, cot14BHYT;
            int cot1DV, cot2DV, cot3DV, cot4DV, cot5DV, cot6DV, cot7DV, cot8DV, cot9DV, cot10DV, cot11DV, cot12DV, cot13DV, cot14DV;
            var q1 = (from bnkb in DataContext.BNKBs
                  join bn in DataContext.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                  join vv in DataContext.VaoViens on bnkb.MaBNhan equals vv.MaBNhan
                  where (vv.NgayVao < tungay && bn.NoiTru == 1 && bnkb.PhuongAn == 4 && bn.Status != 3)
                  select new ListBenhNhan
                  {
                      MaBN = bn.MaBNhan,
                      CapCuu = bn.CapCuu,
                      DTNT = bn.DTNT,
                      IDDTBN = bn.IDDTBN,
                      MaICD = bnkb.MaICD,
                      MaKP = bn.MaKP,
                      NoiTru = bn.NoiTru,
                      Status = bn.Status,
                      DTuong = bn.DTuong
            }).ToList();

            var q2 = (from vv in DataContext.VaoViens
                      join bn in DataContext.BenhNhans on vv.MaBNhan equals bn.MaBNhan
                      where (vv.NgayVao > tungay && vv.NgayVao < denngay && bn.NoiTru == 1)
                      select new ListBenhNhan
                      {
                          MaBN = bn.MaBNhan,
                          CapCuu = bn.CapCuu,
                          DTNT = bn.DTNT,
                          IDDTBN = bn.IDDTBN,
                          MaKP = bn.MaKP,
                          NoiTru = bn.NoiTru,
                          Status = bn.Status,
                          DTuong = bn.DTuong
                      }).ToList();

            var q3 = (from rv in DataContext.RaViens
                      join bn in DataContext.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                      where (rv.NgayRa > tungay && rv.NgayRa < denngay && bn.NoiTru == 1)
                      select new ListBenhNhan
                      {
                          MaBN = rv.MaBNhan,
                          CapCuu = bn.CapCuu,
                          DTNT = bn.DTNT,
                          IDDTBN = bn.IDDTBN,
                          MaKP = bn.MaKP,
                          NoiTru = bn.NoiTru,
                          StatusRV = rv.Status,
                          DTuong = bn.DTuong
                      }).ToList();

            var q4 = (from bnkb in DataContext.BNKBs
                    join bn in DataContext.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                    where (bnkb.NgayNghi > tungay && bnkb.NgayNghi < denngay && bn.NoiTru == 0  && (bnkb.PhuongAn == 0 || bnkb.PhuongAn == 2))
                    select new ListBenhNhan
                    {
                        MaBN = bn.MaBNhan,
                        CapCuu = bn.CapCuu,
                        DTNT = bn.DTNT,
                        IDDTBN = bn.IDDTBN,
                        MaKP = bnkb.MaKP,
                        NoiTru = bn.NoiTru,
                        MaICD = bnkb.MaICD,
                        DTuong = bn.DTuong
                    }).Distinct().ToList();

            var yhct = (from bn in q4 // YHCT
                      join rv in DataContext.RaViens on bn.MaBN equals rv.MaBNhan
                      where (rv.MaCK == 7 && bn.DTNT == true && bn.NoiTru == 0 && bn.PhuongAn == 0)
                      select new ListBenhNhan
                      {
                          MaBN = bn.MaBN,
                          MaKP = bn.MaKP,
                          DTuong = bn.DTuong
                      }).Distinct().ToList();

            var rhm = (from bn in q4 // RHM
                       join kp1 in DataContext.KPhongs on bn.MaKP equals kp1.MaKP
                       where (kp1.TenKP.Contains("RHM") && bn.DTNT == true && bn.PhuongAn == 0)
                       select new ListBenhNhan
                        {
                            MaBN = bn.MaBN,
                            MaKP = bn.MaKP,
                            DTuong = bn.DTuong
                        }).Distinct().ToList();

            var pt = (from dt in DataContext.DThuocs   // phau thuat
                       join dtct in DataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                       join dv in DataContext.DichVus on dtct.MaDV equals dv.MaDV
                       join bn in DataContext.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                       where (dtct.NgayNhap > tungay && dtct.NgayNhap < denngay && dv.IdTieuNhom == 23)
                       select new ListBenhNhan
                       {
                           MaBN = bn.MaBNhan,
                           MaKP = bn.MaKP,
                           DTuong = bn.DTuong
                       }).Distinct().ToList();

            if (MaKP == 0)
            {
                cot1BHYT = q1.Where(p => p.DTuong == "BHYT").Count();
                cot2BHYT = q2.Where(p => p.DTuong == "BHYT").Count();
                cot3BHYT = q3.Where(p => p.StatusRV == 2 && p.DTuong == "BHYT").Count(); //bn ra vien
                cot4BHYT = q3.Where(p => p.StatusRV == 1 && p.DTuong == "BHYT").Count(); //bn chuyen di
                cot5BHYT = q3.Where(p => p.CapCuu == 2 && p.DTuong == "BHYT").Count(); //bn chet
                cot6BHYT = cot1BHYT + cot2BHYT - cot3BHYT - cot4BHYT - cot5BHYT; // tong
                cot7BHYT = q4.Where(p => p.DTNT == false && p.DTuong == "BHYT").Count(); // bnkb
                cot8BHYT = q4.Where(p => p.DTNT == true && p.DTuong == "BHYT" && (p.MaICD.Contains("E11") || p.MaICD.Contains("I10"))).Count();
                cot9BHYT = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.MaKP == 15 && p.DTNT == true && p.DTuong == "BHYT").Count() : 0;
                cot10BHYT = yhct.Where(p => p.DTuong == "BHYT").Count();
                cot11BHYT = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.DTNT == true && p.MaKP == 7 && p.DTuong == "BHYT").Count() : 0;  // PHCN
                cot12BHYT = rhm.Where(p => p.DTuong == "BHYT").Count();
                cot13BHYT = cot7BHYT + cot8BHYT + cot9BHYT + cot10BHYT + cot11BHYT + cot12BHYT;
                cot14BHYT = pt.Where(p => p.DTuong == "BHYT").Count();

                cot1DV = q1.Where(p => p.DTuong == "Dịch vụ").Count();
                cot2DV = q2.Where(p => p.DTuong == "Dịch vụ").Count();
                cot3DV = q3.Where(p => p.StatusRV == 2 && p.DTuong == "Dịch vụ").Count(); //bn ra vien
                cot4DV = q3.Where(p => p.StatusRV == 1 && p.DTuong == "Dịch vụ").Count(); //bn chuyen di
                cot5DV = q3.Where(p => p.CapCuu == 2 && p.DTuong == "Dịch vụ").Count(); //bn chet
                cot6DV = cot1DV + cot2DV - cot3DV - cot4DV - cot5DV; // tong
                cot7DV = q4.Where(p => p.DTNT == false && p.DTuong == "Dịch vụ").Count(); // bnkb
                cot8DV = q4.Where(p => p.DTNT == true && p.DTuong == "Dịch vụ" && (p.MaICD.Contains("E11") || p.MaICD.Contains("I10"))).Count();
                cot9DV = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.MaKP == 15 && p.DTNT == true && p.DTuong == "Dịch vụ").Count() : 0;
                cot10DV = yhct.Where(p => p.DTuong == "Dịch vụ").Count();
                cot11DV = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.DTNT == true && p.MaKP == 7 && p.DTuong == "Dịch vụ").Count() : 0;  // PHCN
                cot12DV = rhm.Where(p => p.DTuong == "Dịch vụ").Count();
                cot13DV = cot7DV + cot8DV + cot9DV + cot10DV + cot11DV + cot12DV;
                cot14DV = pt.Where(p => p.DTuong == "Dịch vụ").Count();

            }
            else
            {
                cot1BHYT = q1.Where(p => p.DTuong == "BHYT" && p.MaKP == MaKP).Count();
                cot2BHYT = q2.Where(p => p.DTuong == "BHYT" && p.MaKP == MaKP).Count();
                cot3BHYT = q3.Where(p => p.StatusRV == 2 && p.DTuong == "BHYT" && p.MaKP == MaKP).Count(); //bn ra vien
                cot4BHYT = q3.Where(p => p.StatusRV == 1 && p.DTuong == "BHYT" && p.MaKP == MaKP).Count(); //bn chuyen di
                cot5BHYT = q3.Where(p => p.CapCuu == 2 && p.DTuong == "BHYT" && p.MaKP == MaKP).Count(); //bn chet
                cot6BHYT = cot1BHYT + cot2BHYT - cot3BHYT - cot4BHYT - cot5BHYT; // tong
                cot7BHYT = q4.Where(p => p.DTNT == false && p.DTuong == "BHYT" && p.MaKP == MaKP).Count(); // bnkb
                cot8BHYT = q4.Where(p => p.DTNT == true && p.DTuong == "BHYT" && (p.MaICD.Contains("E11") || p.MaICD.Contains("I10")) && p.MaKP == MaKP).Count();
                cot9BHYT = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.MaKP == 15 && p.DTNT == true && p.DTuong == "BHYT" && p.MaKP == MaKP).Count() : 0;
                cot10BHYT = yhct.Where(p => p.DTuong == "BHYT" && p.MaKP == MaKP).Count();
                cot11BHYT = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.DTNT == true && p.MaKP == 7 && p.DTuong == "BHYT" && p.MaKP == MaKP).Count() : 0;  // PHCN
                cot12BHYT = rhm.Where(p => p.DTuong == "BHYT" && p.MaKP == MaKP).Count();
                cot13BHYT = cot7BHYT + cot8BHYT + cot9BHYT + cot10BHYT + cot11BHYT + cot12BHYT;
                cot14BHYT = pt.Where(p => p.DTuong == "BHYT" && p.MaKP == MaKP).Count();

                cot1DV = q1.Where(p => p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count();
                cot2DV = q2.Where(p => p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count();
                cot3DV = q3.Where(p => p.StatusRV == 2 && p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count(); //bn ra vien
                cot4DV = q3.Where(p => p.StatusRV == 1 && p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count(); //bn chuyen di
                cot5DV = q3.Where(p => p.CapCuu == 2 && p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count(); //bn chet
                cot6DV = cot1DV + cot2DV - cot3DV - cot4DV - cot5DV; // tong
                cot7DV = q4.Where(p => p.DTNT == false && p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count(); // bnkb
                cot8DV = q4.Where(p => p.DTNT == true && p.DTuong == "Dịch vụ" && (p.MaICD.Contains("E11") || p.MaICD.Contains("I10")) && p.MaKP == MaKP).Count();
                cot9DV = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.MaKP == 15 && p.DTNT == true && p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count() : 0;
                cot10DV = yhct.Where(p => p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count();
                cot11DV = DungChung.Bien.MaBV == "01071" ? q4.Where(p => p.DTNT == true && p.MaKP == 7 && p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count() : 0;  // PHCN
                cot12DV = rhm.Where(p => p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count();
                cot13DV = cot7DV + cot8DV + cot9DV + cot10DV + cot11DV + cot12DV;
                cot14DV = pt.Where(p => p.DTuong == "Dịch vụ" && p.MaKP == MaKP).Count();
            }


            List<RpData> RpData = new List<RpData>();
            RpData data = new RpData();
            data.c1BHYT = Convert.ToString(cot1BHYT);
            data.c2BHYT = Convert.ToString(cot2BHYT);
            data.c3BHYT = Convert.ToString(cot3BHYT);
            data.c4BHYT = Convert.ToString(cot4BHYT);
            data.c5BHYT = Convert.ToString(cot5BHYT);
            data.c6BHYT = Convert.ToString(cot6BHYT);
            data.c7BHYT = Convert.ToString(cot7BHYT);
            data.c8BHYT = Convert.ToString(cot8BHYT);
            data.c9BHYT = Convert.ToString(cot9BHYT);
            data.c10BHYT = Convert.ToString(cot10BHYT);
            data.c11BHYT = Convert.ToString(cot11BHYT);
            data.c12BHYT = Convert.ToString(cot12BHYT);
            data.c13BHYT = Convert.ToString(cot13BHYT);
            data.c14BHYT = Convert.ToString(cot14BHYT);

            data.c1DV = Convert.ToString(cot1DV);
            data.c2DV = Convert.ToString(cot2DV);
            data.c3DV = Convert.ToString(cot3DV);
            data.c4DV = Convert.ToString(cot4DV);
            data.c5DV = Convert.ToString(cot5DV);
            data.c6DV = Convert.ToString(cot6DV);
            data.c7DV = Convert.ToString(cot7DV);
            data.c8DV = Convert.ToString(cot8DV);
            data.c9DV = Convert.ToString(cot9DV);
            data.c10DV = Convert.ToString(cot10DV);
            data.c11DV = Convert.ToString(cot11DV);
            data.c12DV = Convert.ToString(cot12DV);
            data.c13DV = Convert.ToString(cot13DV);
            data.c14DV = Convert.ToString(cot14DV);
            RpData.Add(data);

            Dictionary<string, object> _dic = new Dictionary<string, object>();
            string tenKhoa = MaKP != 0 ? TenKP : "";
            _dic.Add("TenKhoa", tenKhoa);
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);
            string s = "Từ ngày " + tungay.Day.ToString() + " tháng " + tungay.Month.ToString() + " năm " + DateTime.Now.Year.ToString() + ", đến ngày " + denngay.Day.ToString() + " tháng " + denngay.Month.ToString() + " năm " + denngay.Year.ToString();
            _dic.Add("Ngaythang", s);

            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BNNoiNgoaiTru_01071_01049, RpData, _dic, false);


        }
    }
    public class ListKP
    {
        public int MaKP { get; set; }
        public string TenKP { get; set; }
    }
    public class ListBenhNhan
    {
        public int? MaBN { get; set; }
        public int? IDDTBN { get; set; }
        public int? NoiTru { get; set; }
        public int? MaKP { get; set; }
        public int? CapCuu { get; set; }
        public int? Status { get; set; }
        public int? StatusRV { get; set; }
        public bool? DTNT { get; set; }
        public string MaICD { get; set; }
        public int? PhuongAn { get; set; }
        public string MaKPdt { get; set; }
        public DateTime? NgayVao { get; set; }
        public DateTime? NgayRa { get; set; }
        public string DTuong { get; set; }
    }
    public class RpData
    {
        private string _c1BHYT;
        public string c1BHYT
        {
            get { return _c1BHYT; }
            set { _c1BHYT = value; }
        }

        private string _c2BHYT;
        public string c2BHYT
        {
            get { return _c2BHYT; }
            set { _c2BHYT = value; }
        }

        private string _c3BHYT;
        public string c3BHYT
        {
            get { return _c3BHYT; }
            set { _c3BHYT = value; }
        }

        private string _c4BHYT;
        public string c4BHYT
        {
            get { return _c4BHYT; }
            set { _c4BHYT = value; }
        }
        private string _c5BHYT;
        public string c5BHYT
        {
            get { return _c5BHYT; }
            set { _c5BHYT = value; }
        }
        private string _c6BHYT;
        public string c6BHYT
        {
            get { return _c6BHYT; }
            set { _c6BHYT = value; }
        }

        private string _c7BHYT;
        public string c7BHYT
        {
            get { return _c7BHYT; }
            set { _c7BHYT = value; }
        }

        private string _c8BHYT;
        public string c8BHYT
        {
            get { return _c8BHYT; }
            set { _c8BHYT = value; }
        }

        private string _c9BHYT;
        public string c9BHYT
        {
            get { return _c9BHYT; }
            set { _c9BHYT = value; }
        }

        private string _c10BHYT;
        public string c10BHYT
        {
            get { return _c10BHYT; }
            set { _c10BHYT = value; }
        }

        private string _c11BHYT;
        public string c11BHYT
        {
            get { return _c11BHYT; }
            set { _c11BHYT = value; }
        }

        private string _c12BHYT;
        public string c12BHYT
        {
            get { return _c12BHYT; }
            set { _c12BHYT = value; }
        }

        private string _c13BHYT;
        public string c13BHYT
        {
            get { return _c13BHYT; }
            set { _c13BHYT = value; }
        }

        private string _c14BHYT;
        public string c14BHYT
        {
            get { return _c14BHYT; }
            set { _c14BHYT = value; }
        }

        private string _c1DV;
        public string c1DV
        {
            get { return _c1DV; }
            set { _c1DV = value; }
        }

        private string _c2DV;
        public string c2DV
        {
            get { return _c2DV; }
            set { _c2DV = value; }
        }

        private string _c3DV;
        public string c3DV
        {
            get { return _c3DV; }
            set { _c3DV = value; }
        }

        private string _c4DV;
        public string c4DV
        {
            get { return _c4DV; }
            set { _c4DV = value; }
        }
        private string _c5DV;
        public string c5DV
        {
            get { return _c5DV; }
            set { _c5DV = value; }
        }
        private string _c6DV;
        public string c6DV
        {
            get { return _c6DV; }
            set { _c6DV = value; }
        }

        private string _c7DV;
        public string c7DV
        {
            get { return _c7DV; }
            set { _c7DV = value; }
        }

        private string _c8DV;
        public string c8DV
        {
            get { return _c8DV; }
            set { _c8DV = value; }
        }

        private string _c9DV;
        public string c9DV
        {
            get { return _c9DV; }
            set { _c9DV = value; }
        }

        private string _c10DV;
        public string c10DV
        {
            get { return _c10DV; }
            set { _c10DV = value; }
        }

        private string _c11DV;
        public string c11DV
        {
            get { return _c11DV; }
            set { _c11DV = value; }
        }

        private string _c12DV;
        public string c12DV
        {
            get { return _c12DV; }
            set { _c12DV = value; }
        }

        private string _c13DV;
        public string c13DV
        {
            get { return _c13DV; }
            set { _c13DV = value; }
        }

        private string _c14DV;
        public string c14DV
        {
            get { return _c14DV; }
            set { _c14DV = value; }
        }

    }
}
