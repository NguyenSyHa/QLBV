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
    public partial class Frm_BcTinhHinhBenhTatCD10_TH : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcTinhHinhBenhTatCD10_TH()
        {
            InitializeComponent();
        }
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
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
       
        private void Frm_BcTinhHinhBenhTatCD10_VY_Load(object sender, EventArgs e)
        {
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupTuNgay.Focus();
            
        }
       
        private class ICD10
        {
            private string MaCB;
            private int Ma;
            private string TenCB;
            private string MaICD;
            private string MaICD1;
            private string TenICD;
            private string TenWHO;
            private int I1; private int I2; private int I3; private int I4; private int I5; private int I6; private int I7; private int I8; private int I9; private int I10; private int I11;
            private int I12; private int I13; private int I14; private int I15; private int I16; private int I17; private int I18; private int I19; private int I20; private int I21;
          
            public string macb
            { set { MaCB = value; } get { return MaCB; } }
            public int ma
            { set { Ma = value; } get { return Ma; } }
            public string tencb
            { set { TenCB = value; } get { return TenCB; } }
            public string maicd
            { set { MaICD = value; } get { return MaICD; } }
            public string maicd1
            { set { MaICD1 = value; } get { return MaICD1; } }
            public string tenicd
            { set { TenICD = value; } get { return TenICD; } }
            public string tenwho
            { set { TenWHO = value; } get { return TenWHO; } }
            public int i1
            { set { I1 = value; } get { return I1; } }
            public int i2
            { set { I2 = value; } get { return I2; } }
            public int i3
            { set { I3 = value; } get { return I3; } }
            public int i4
            { set { I4 = value; } get { return I4; } }
            public int i5
            { set { I5 = value; } get { return I5; } }
            public int i6
            { set { I6 = value; } get { return I6; } }
            public int i7
            { set { I7 = value; } get { return I7; } }
            public int i8
            { set { I8 = value; } get { return I8; } }
            public int i9
            { set { I9 = value; } get { return I9; } }
            public int i10
            { set { I10 = value; } get { return I10; } }
            public int i11
            { set { I11 = value; } get { return I11; } }
            public int i12
            { set { I12 = value; } get { return I12; } }
            public int i13
            { set { I13 = value; } get { return I13; } }
            public int i14
            { set { I14 = value; } get { return I14; } }
            public int i15
            { set { I15 = value; } get { return I15; } }
            public int i16
            { set { I16 = value; } get { return I16; } }
            public int i17
            { set { I17 = value; } get { return I17; } }
            public int i18
            { set { I18 = value; } get { return I18; } }
            public int i19
            { set { I19 = value; } get { return I19; } }
            public int i20
            { set { I20 = value; } get { return I20; } }
            public int i21
            { set { I21 = value; } get { return I21; } }
           
        }
     
        List<ICD10> _ICD10 = new List<ICD10>();
      private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
       
            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                 frmIn frm = new frmIn();
                 BaoCao.Rep_BcTinhHinhBenhTatICD10_TH rep = new BaoCao.Rep_BcTinhHinhBenhTatICD10_TH();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region Hiển thị thời gian 
                int _q = Convert.ToInt32(ngayden.ToString().Substring(3, 2));
                string _quy = "";
                if (_q > 0 && _q <= 3) { _quy = "Quý I năm " + ngayden.Year; }
                if (_q > 3 && _q <= 6) { _quy = "Quý II năm " + ngayden.Year; }
                if (_q > 6 && _q <= 10) { _quy = "Quý III năm " + ngayden.Year; }
                if (_q > 10 && _q <= 12) { _quy = "Quý IV năm " + ngayden.Year; }
                if (radIn.SelectedIndex == 0) { rep.NTN.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " đến ngày " + ngayden.ToString().Substring(0,10); }
                if (radIn.SelectedIndex == 1) { rep.NTN.Value = _quy; }
                if (radIn.SelectedIndex == 2) { rep.NTN.Value ="Năm "+ ngayden.Year; }
                if (radIn.SelectedIndex == 3) { rep.NTN.Value = "(6 tháng đầu năm " + ngayden.Year +")"; }
                if (radIn.SelectedIndex == 4) { rep.NTN.Value = "(6 tháng cuối năm " + ngayden.Year+")"; }
            #endregion
              
                _ICD10.Clear();
                var q1 = data.ICD10.Where(p => p.Status == 1).Where(p => p.MaICD.Length == 3).ToList();
                var q2 = (from rv in data.RaViens.Where(p => p.MaICD.Length >= 3).Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden)
                          join icd in data.ICD10 on rv.MaICD equals icd.MaICD
                          select new { icd.TenWHO, icd.TenWHOe, icd.TenCB }).ToList();
                var q3 = (from rv in data.RaViens.Where(p => p.MaICD.Length >= 3).Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden)
                          join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                          join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                          select new {  kp.PLoai,rv.MaBNhan,bn.Tuoi,bn.GTinh,bn.DTuong,rv.MaICD,rv.KetQua,rv.SoNgaydt }).ToList();
                #region Sắp xếp thứ tự tên chương bệnh
                if (chkIn.Checked ==false)
                {
                      var q = (from icd in q1
                             group new { icd } by new { icd.TenWHO, icd.TenWHOe, icd.TenCB } into kq
                             select new { TenWHO = kq.Key.TenWHO, TenWHOe = kq.Key.TenWHOe, TenCB = kq.Key.TenCB }).OrderBy(p => p.TenCB).OrderBy(p => p.TenWHO).ToList();
                    if (q.Count > 0)
                    {
                        foreach (var a in q)
                        {
                            ICD10 themmoi = new ICD10();
                            if (a.TenCB != null)
                            {
                                themmoi.tencb = a.TenCB;
                                if (a.TenCB.Contains("Chương I:")) { themmoi.ma = 1; }
                                if (a.TenCB.Contains("Chương II:")) { themmoi.ma = 2; }
                                if (a.TenCB.Contains("Chương III:")) { themmoi.ma = 3; }
                                if (a.TenCB.Contains("Chương IV:")) { themmoi.ma = 4; }
                                if (a.TenCB.Contains("Chương V:")) { themmoi.ma = 5; }
                                if (a.TenCB.Contains("Chương VI:")) { themmoi.ma = 6; }
                                if (a.TenCB.Contains("Chương VII:")) { themmoi.ma = 7; }
                                if (a.TenCB.Contains("Chương VIII:")) { themmoi.ma = 8; }
                                if (a.TenCB.Contains("Chương IX:")) { themmoi.ma = 9; }
                                if (a.TenCB.Contains("Chương X:")) { themmoi.ma = 10; }
                                if (a.TenCB.Contains("Chương XI:")) { themmoi.ma = 11; }
                                if (a.TenCB.Contains("Chương XII:")) { themmoi.ma = 12; }
                                if (a.TenCB.Contains("Chương XIII:")) { themmoi.ma = 13; }
                                if (a.TenCB.Contains("Chương XIV:")) { themmoi.ma = 14; }
                                if (a.TenCB.Contains("Chương XV:")) { themmoi.ma = 15; }
                                if (a.TenCB.Contains("Chương XVI:")) { themmoi.ma = 16; }
                                if (a.TenCB.Contains("Chương XVII:")) { themmoi.ma = 17; }
                                if (a.TenCB.Contains("Chương XVIII:")) { themmoi.ma = 18; }
                                if (a.TenCB.Contains("Chương XIX:")) { themmoi.ma = 19; }
                                if (a.TenCB.Contains("Chương XX:")) { themmoi.ma = 20; }
                                if (a.TenCB.Contains("Chương XXI:")) { themmoi.ma = 21; }
                                if (a.TenCB.Contains("Chương XXII:")) { themmoi.ma = 22; }
                            }
                            if (a.TenWHO != null&&a.TenWHO.ToString()!="")
                            {
                                themmoi.tenicd = a.TenWHO;
                                themmoi.tenwho = a.TenWHO + " - " + a.TenWHOe;
                            }
                            _ICD10.Add(themmoi);
                        }
                    }
                }
                else
                {
                     var q = (from icd in q2
                             group new { icd } by new { icd.TenWHO, icd.TenWHOe, icd.TenCB } into kq
                             select new { TenWHO = kq.Key.TenWHO, TenWHOe = kq.Key.TenWHOe, TenCB = kq.Key.TenCB }).OrderBy(p => p.TenCB).OrderBy(p => p.TenWHO).ToList();
                    if (q.Count > 0)
                    {
                        foreach (var a in q)
                        {
                            ICD10 themmoi = new ICD10();
                            if (a.TenCB != null)
                            {
                                themmoi.tencb = a.TenCB;
                                if (a.TenCB.Contains("Chương I:")) { themmoi.ma = 1; }
                                if (a.TenCB.Contains("Chương II:")) { themmoi.ma = 2; }
                                if (a.TenCB.Contains("Chương III:")) { themmoi.ma = 3; }
                                if (a.TenCB.Contains("Chương IV:")) { themmoi.ma = 4; }
                                if (a.TenCB.Contains("Chương V:")) { themmoi.ma = 5; }
                                if (a.TenCB.Contains("Chương VI:")) { themmoi.ma = 6; }
                                if (a.TenCB.Contains("Chương VII:")) { themmoi.ma = 7; }
                                if (a.TenCB.Contains("Chương VIII:")) { themmoi.ma = 8; }
                                if (a.TenCB.Contains("Chương IX:")) { themmoi.ma = 9; }
                                if (a.TenCB.Contains("Chương X:")) { themmoi.ma = 10; }
                                if (a.TenCB.Contains("Chương XI:")) { themmoi.ma = 11; }
                                if (a.TenCB.Contains("Chương XII:")) { themmoi.ma = 12; }
                                if (a.TenCB.Contains("Chương XIII:")) { themmoi.ma = 13; }
                                if (a.TenCB.Contains("Chương XIV:")) { themmoi.ma = 14; }
                                if (a.TenCB.Contains("Chương XV:")) { themmoi.ma = 15; }
                                if (a.TenCB.Contains("Chương XVI:")) { themmoi.ma = 16; }
                                if (a.TenCB.Contains("Chương XVII:")) { themmoi.ma = 17; }
                                if (a.TenCB.Contains("Chương XVIII:")) { themmoi.ma = 18; }
                                if (a.TenCB.Contains("Chương XIX:")) { themmoi.ma = 19; }
                                if (a.TenCB.Contains("Chương XX:")) { themmoi.ma = 20; }
                                if (a.TenCB.Contains("Chương XXI:")) { themmoi.ma = 21; }
                                if (a.TenCB.Contains("Chương XXII:")) { themmoi.ma = 22; }
                            }
                            if (a.TenWHO != null && a.TenWHO.ToString() != "")
                            {
                                themmoi.tenicd = a.TenWHO;
                                themmoi.tenwho = a.TenWHO + " - " + a.TenWHOe;
                            }
                            _ICD10.Add(themmoi);
                        }
                    }
                }
                #endregion
                #region Lấy tên ICD
                var qten = (from icd in q1
                         group new { icd } by new {icd.MaICD, icd.TenWHO, icd.TenCB } into kq
                         select new {MaICD=kq.Key.MaICD, TenWHO = kq.Key.TenWHO, TenCB = kq.Key.TenCB }).OrderBy(p => p.TenCB).OrderBy(p=>p.MaICD).OrderBy(p => p.TenWHO).ToList();
                if (qten.Count > 0)
                {

                   foreach (var a in _ICD10)
                    {
                        string _ma = "";
                        foreach (var b in qten)
                        {
                            if (b.TenCB == a.tencb)
                            {
                                if (b.TenWHO != null && b.TenWHO == a.tenicd)
                                {

                                    _ma += b.MaICD.ToString() + ",";
                                    a.maicd1 += b.MaICD.ToString() + ", ";
                                    
                                }
                            }
                           
                        }
                        a.maicd = getFstString(_ma);
                    }
                }
                #endregion
                #region Lấy DL hoạt động Khám bệnh
                var qkb = (from q in q3.Where(p=>p.PLoai=="Phòng khám")
                           group new { q } by q.MaICD.Substring(0, 3) into kq
                           select new
                           {
                               MaICD = kq.Key,
                               I1 = kq.Select(p => p.q.MaBNhan).Count(),
                               I2 = kq.Where(p => p.q.GTinh == 0).Select(p => p.q.MaBNhan).Count(),
                               I3 = kq.Where(p => p.q.DTuong=="BHYT").Select(p => p.q.MaBNhan).Count(),
                               I4 = kq.Where(p => p.q.DTuong=="Dịch vụ").Select(p => p.q.MaBNhan).Count(),
                               I5 = kq.Where(p => p.q.Tuoi < 15).Select(p => p.q.MaBNhan).Count(),
                               I6 = kq.Where(p => p.q.Tuoi < 6).Select(p => p.q.MaBNhan).Count(),
                               I7 = kq.Where(p =>p.q.Tuoi< 5).Select(p => p.q.MaBNhan).Count(),
                               I8 = kq.Where(p =>p.q.Tuoi >60).Select(p => p.q.MaBNhan).Count(),
                               I9 = kq.Where(p =>p.q.KetQua=="Tử vong").Select(p => p.q.MaBNhan).Count(),
                                 }).ToList();

                if (qkb.Count > 0)
                {
                    int i = 0;
                    foreach (var b in _ICD10)
                    {
                        
                        foreach (var n in qkb)
                        {
                            if (n.MaICD.Length >= 3)
                            {

                                if (b.maicd1 != null && b.maicd1.Contains(n.MaICD.ToString().Substring(0, 3)))
                                {
                                      b.i1 =Convert.ToInt32(b.i1)+Convert.ToInt32(n.I1); 
                                      b.i2 = Convert.ToInt32(b.i2)+Convert.ToInt32(n.I2); 
                                      b.i3 = Convert.ToInt32(b.i3)+Convert.ToInt32(n.I3); 
                                      b.i4 = Convert.ToInt32(b.i4)+Convert.ToInt32(n.I4); 
                                      b.i5 = Convert.ToInt32(b.i5)+Convert.ToInt32(n.I5); 
                                      b.i6 = Convert.ToInt32(b.i6)+Convert.ToInt32(n.I6); 
                                      b.i7 = Convert.ToInt32(b.i7)+Convert.ToInt32(n.I7); 
                                      b.i8 = Convert.ToInt32(b.i8)+Convert.ToInt32(n.I8); 
                                      b.i9 = Convert.ToInt32(b.i9)+Convert.ToInt32(n.I9); 
                                   
                                }
                            }
                        }
                        
                    }
                }
#endregion
                #region Lấy DL khoa điều trị
                var qdtm = (from q in q3.Where(p => p.PLoai == "Lâm sàng")
                             group new { q } by q.MaICD.Substring(0, 3) into kq
                            select new
                            {
                                MaICD = kq.Key,
                                I10 = kq.Select(p => p.q.MaBNhan).Count(),
                                I11 = kq.Where(p => p.q.GTinh == 0).Select(p => p.q.MaBNhan).Count(),
                                I12 = kq.Where(p =>p.q.KetQua=="Tử vong").Select(p => p.q.MaBNhan).Count(),
                                I13 = kq.Sum(p=>p.q.SoNgaydt) == null ? 0 : kq.Sum(p=>p.q.SoNgaydt),
                                I14 = kq.Where(p => p.q.Tuoi<15).Select(p => p.q.MaBNhan).Count(),
                                I15 = kq.Where(p => p.q.Tuoi<5).Select(p => p.q.MaBNhan).Count(),
                                I16 =  kq.Where(p => p.q.Tuoi<15&&p.q.KetQua=="Tử vong").Select(p => p.q.MaBNhan).Count(),
                                I17 = kq.Where(p => p.q.Tuoi<15).Sum(p=>p.q.SoNgaydt) == null ? 0 : kq.Where(p => p.q.Tuoi<15).Sum(p=>p.q.SoNgaydt),
                                I18 = kq.Where(p => p.q.Tuoi<5).Sum(p=>p.q.SoNgaydt) == null ? 0 : kq.Where(p => p.q.Tuoi<5).Sum(p=>p.q.SoNgaydt),
                                I19 = kq.Where(p => p.q.Tuoi>60).Select(p => p.q.MaBNhan).Count() ,
                                I20 = kq.Where(p => p.q.Tuoi>60&&p.q.KetQua=="Tử vong").Select(p => p.q.MaBNhan).Count(),
                               I21 = kq.Where(p => p.q.Tuoi>60).Sum(p=>p.q.SoNgaydt) == null ? 0 : kq.Where(p => p.q.Tuoi>60).Sum(p=>p.q.SoNgaydt),
                                
                            }).ToList();
                if (qdtm.Count > 0)
                {
                    foreach (var b in _ICD10)
                    {
                        foreach (var n in qdtm)
                        {
                            if (n.MaICD.Length >= 3)
                            {
                                if (b.maicd1 != null && b.maicd1.Contains(n.MaICD.ToString().Substring(0, 3)))
                                {

                                    b.i10 =Convert.ToInt32(b.i10)+Convert.ToInt32(n.I10); 
                                    b.i11 =Convert.ToInt32(b.i11)+Convert.ToInt32(n.I11); 
                                    b.i12 =Convert.ToInt32(b.i12)+Convert.ToInt32(n.I12); 
                                    b.i13 =Convert.ToInt32(b.i13)+Convert.ToInt32(n.I13); 
                                    b.i14 =Convert.ToInt32(b.i14)+Convert.ToInt32(n.I14); 
                                    b.i15 =Convert.ToInt32(b.i15)+Convert.ToInt32(n.I15); 
                                    b.i16 =Convert.ToInt32(b.i16)+Convert.ToInt32(n.I16); 
                                    b.i17 =Convert.ToInt32(b.i17)+Convert.ToInt32(n.I17); 
                                    b.i18 =Convert.ToInt32(b.i18)+Convert.ToInt32(n.I18); 
                                    b.i19 =Convert.ToInt32(b.i19)+Convert.ToInt32(n.I19); 
                                    b.i20 =Convert.ToInt32(b.i20)+Convert.ToInt32(n.I20); 
                                    b.i21 =Convert.ToInt32(b.i21)+Convert.ToInt32(n.I21); 
                                 
                             
                                }
                            }
                        }
                    }
                }

      #endregion
                rep.DataSource = _ICD10.OrderBy(p => p.ma).OrderBy(p => p.maicd1).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
        }
      private string getFstString(string a)
      {
          string b = "";
          if (a == "")
              b = "";
          else
          {
              #region lấy ra list các ký tự đầu tiên của các phần tử của chuỗi
              string[] strArr = a.Split(',');// mảng các phần tử của chuỗi        
              List<string> chStr = new List<string>();//list các ký tự đầu tiên của các phần tử của chuỗi
              foreach (string s in strArr)
              {
                  if (s != "")
                  {
                      string ch = s.Substring(0, 1).ToUpper(); // ký tự đầu tiên của 1 phần tử
                      if (chStr.Count > 0)
                      {
                          int count = 0;
                          foreach (string c in chStr)
                          {
                              if (c== (ch))
                                  count++;
                          }
                          if (count == 0)// ký tự này chưa tồn tại trong mảng chStr => add ch vào mảng chStr
                              chStr.Add(ch);
                      }
                      else
                          chStr.Add(ch);
                  }
              #endregion
              }
              chStr.Sort(); // sắp xếp các phần tử trong list
              foreach (string c in chStr)
              {
                  if (c != "")
                      b += getEndtring(c, a) + ";";
              }
              if (b.LastIndexOf(";") == b.Length - 1)
                  b = b.Substring(0, b.Length - 1);
          }
          return b;
      }
      private string getEndtring(string kytu, string chuoi)
      {
          string trave = "";
          #region lấy ra list các phần cuối các phần tử của chuỗi
          string[] strArr = chuoi.Split(',');// mảng các phần tử của chuỗi        
          List<int> chInt = new List<int>();//list các ký tự cuối của các phần tử của chuỗi
          foreach (string s in strArr)
          {
              if (s != "")
              {
                  if ((s.Substring(0, 1).ToLower())== (kytu.ToLower()))
                  {
                      string ch = s.Substring(1, s.Length - 1); // phần đuôi của 1 phần tử
                      int rs;
                      if (Int32.TryParse(ch, out rs))
                      {

                          if (chInt.Count > 0)
                          {
                              int count = 0;
                              foreach (int i in chInt)
                              {
                                  if (i == Convert.ToInt32(ch))
                                      count++;
                              }
                              if (count == 0)// ký tự này chưa tồn tại trong mảng chStr => add ch vào mảng chStr
                                  chInt.Add(Convert.ToInt32(ch));
                          }
                          else
                              chInt.Add(Convert.ToInt32(ch));
                      }
                  }
              }
          }
          #endregion
          chInt.Sort(); // sắp xếp các phần tử trong list
          #region gộp chuỗi
          int n = -2;
          for (int so = 0; so < chInt.Count; so++)
          {
              if (chInt[so] != chInt[chInt.Count - 1])// s không phải phần tử cuối
              {
                  string trf;
                  if (chInt[so] < 10)
                      trf = String.Format("{0:00}", chInt[so]);
                  else
                      trf = chInt[so].ToString();

                  if (n == -2)// phần tử đầu tiên
                  {
                      trave = kytu + trf;
                  }
                  else
                  {
                      if (chInt[so] == n + 1) // có tăng
                      {

                          if (trave.Substring(trave.Length - 1)!= ("-"))
                              trave = trave + "-";
                      }
                      else //không tăng
                      {

                          if (trave.Substring(trave.Length - 1)!= ("-"))
                              trave = trave + "," + kytu + trf;
                          else
                          {
                              if (n < 10)
                                  trave = trave + kytu + String.Format("{0:00}", n) + "," + kytu + trf;
                              else
                                  trave = trave + kytu + n.ToString() + "," + kytu + trf;
                          }
                      }
                  }
                  n = chInt[so];
              }
              else// là phần tử cuối
              {
                  string trf;
                  if (chInt[so] < 10)
                      trf = String.Format("{0:00}", chInt[so]);
                  else
                      trf = chInt[so].ToString();
                  if (n == -2)// phần tử đầu tiên
                  {
                      trave = kytu + trf;
                  }
                  else
                  {
                      if (chInt[so] == n + 1) // có tăng
                      {

                          if (trave.Substring(trave.Length - 1)!= ("-"))
                              trave = trave + "-" + kytu + trf;
                          else
                              trave = trave + kytu + trf;
                      }
                      else //không tăng
                      {
                          if (trave.Substring(trave.Length - 1)!= ("-"))
                              trave = trave + "," + kytu + trf;
                          else
                          {
                              if (n < 10)
                                  trave = trave + kytu + String.Format("{0:00}", n) + "," + kytu + trf;
                              else
                                  trave = trave + kytu + n.ToString() + "," + kytu + trf;
                          }
                      }
                  }
                  n = chInt[so];
              }
          }
          return trave;
          #endregion
      }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}