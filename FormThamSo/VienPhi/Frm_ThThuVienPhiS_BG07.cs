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
    public partial class Frm_ThThuVienPhiS_BG07 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThThuVienPhiS_BG07()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            else return true;
        }
        private void Frm_ThThuVienPhiS_BG07_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            if (!File.Exists("TextBL.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextBL.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("TextBL.txt");
            if (lines[lines.Length - 1] == "1")
            {
                txtBL.Text = lines[lines.Length - 2];
             
            }

        }
        private class VP
        {
            private DateTime NTN;
            private string NThang;
            private int MaBN;
            private string TenBN;
            private string DChi;
            private string DTuong;
            private double CK;
            private double SA;
            private double DT;
            private double XQ;
            private double KhacCD;
            private double Mau;
            private double PTM;
            private double NT;
            private double SHM;
            private double VGB;
            private double HIV;
            private double Aminra;
            private double khacXN;
            private double BoBot;
            private double NaoThai;
            private double NSDD;
            private double NSTMH;
            private double Khac;
            private double TamBe;
            private double BHYT;
            private double GDSK;
            private double KSK;
            private double VienPhi;
            private double Tong;
            private double BHYTT;
            private double GDSKT;
            private double KSKT;
            private double NG;
            private double VienPhiT;
            private double TongT;
            private double TongTien;
            private int Tam;
            public DateTime ntn
            { set { NTN = value; } get { return NTN; } }
            public string nthang
            { set { NThang = value; } get { return NThang; } }
            public int mabn
            { set { MaBN = value; } get { return MaBN; } }
            public string tenbn
            { set { TenBN = value; } get { return TenBN; } }
            public string dtuong
            { set { DTuong = value; } get { return DTuong; } }
            public string dchi
            { set { DChi = value; } get { return DChi; } }
            public double ck
            { set { CK = value; } get { return CK; } }
            public double sa
            { set { SA = value; } get { return SA; } }
            public double dt
            { set { DT = value; } get { return DT; } }
            public double xq
            { set { XQ = value; } get { return XQ; } }
            public double khaccd
            { set { KhacCD = value; } get { return KhacCD; } }
            public double mau
            { set { Mau = value; } get { return Mau; } }
            public double ptm
            { set { PTM = value; } get { return PTM; } }
            public double nt
            { set { NT = value; } get { return NT; } }
            public double shm
            { set { SHM = value; } get { return SHM; } }
            public double vgb
            { set { VGB = value; } get { return VGB; } }
            public double hiv
            { set { HIV = value; } get { return HIV; } }
            public double aminra
            { set { Aminra = value; } get { return Aminra; } }
            public double khacxn
            { set { khacXN = value; } get { return khacXN; } }
            public double bobot
            { set { BoBot = value; } get { return BoBot; } }
            public double naothai
            { set { NaoThai = value; } get { return NaoThai; } }
            public double nsdd
            { set { NSDD = value; } get { return NSDD; } }
            public double nstmh
            { set { NSTMH = value; } get { return NSTMH; } }
            public double khac
            { set { Khac = value; } get { return Khac; } }
            public double tambe
            { set { TamBe = value; } get { return TamBe; } }
            public double bhyt
            { set { BHYT = value; } get { return BHYT; } }
            public double gdsk
            { set { GDSK = value; } get { return GDSK; } }
            public double ksk
            { set { KSK = value; } get { return KSK; } }
            public double vp
            { set { VienPhi = value; } get { return VienPhi; } }
            public double tong
            { set { Tong = value; } get { return Tong; } }
            public double bhytt
            { set { BHYTT = value; } get { return BHYTT; } }
            public double gdskt
            { set { GDSKT = value; } get { return GDSKT; } }
            public double kskt
            { set { KSKT = value; } get { return KSKT; } }
            public double ng
            { set { NG = value; } get { return NG; } }
            public double vpt
            { set { VienPhiT = value; } get { return VienPhiT; } }
            public double tongt
            { set { TongT = value; } get { return TongT; } }

            public double tongtien
            { set { TongTien = value; } get { return TongTien; } }
            public int tam
            { set { Tam = value; } get { return Tam; } }
        }
        List<VP> _VP = new List<VP>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
             if (kt())
             {
                 FileStream fs = new FileStream("TextBL.txt", FileMode.Append);
                 StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file
                 writeFile.WriteLine(txtBL.Text);
               
                 writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
                 writeFile.Flush();

                 writeFile.Close();

                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                _VP.Clear();
                BaoCao.Rep_ThThuVienPhis_BG07 rep = new BaoCao.Rep_ThThuVienPhis_BG07();
                rep.TenBC.Value = ("bảng tổng hợp viện phí tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                rep.TuNgayDenNgay.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " đến ngày " + ngayden.ToString().Substring(0, 10);
                int idck = -1, idcdha = -1, idxn = -1, idttpt = -1, ng = -1;

                var nhom = dataContext.NhomDVs.ToList();
                foreach (var a in nhom)
                {
                    switch (a.TenNhomCT)
                    {
                        case "Khám bệnh":
                            idck = a.IDNhom;
                            break;
                        case "Chẩn đoán hình ảnh":
                            idcdha = a.IDNhom;
                            break;
                        case "Xét nghiệm":
                            idxn = a.IDNhom;
                            break;
                        case "Thủ thuật, phẫu thuật":
                            idttpt = a.IDNhom;
                            break;
                        case "Giường điều trị nội trú":
                            ng = a.IDNhom;
                            break;
                    }

                }
              
                 int _nt=-1;int _ngt =-1;
                 if(radBN.SelectedIndex==0){_nt=1;_ngt=0;}
                 if(radBN.SelectedIndex==1){_nt=1;_ngt=-1;}
                 if(radBN.SelectedIndex==2){_nt=-1;_ngt=0;}
                 var q = ((from bn in dataContext.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == "Dịch vụ" || p.DTuong == "KSK")
                           join cls in dataContext.CLS.Where(p => p.NgayThang >= ngaytu).Where(p => p.NgayThang <= ngayden) on bn.MaBNhan equals cls.MaBNhan
                           join cd in dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in dataContext.DichVus on cd.MaDV equals dv.MaDV
                           select new { cls.MaBNhan, cls.NgayThang, bn.TenBNhan, bn.DChi, bn.DTuong, dv.DonGia }).ToList()).Select(p => new { p.MaBNhan, NgayThang = p.NgayThang.ToString().Substring(0, 10), p.TenBNhan, p.DChi, p.DTuong, p.DonGia });
                 var qbn = (from a in q
                            group new { a } by new { a.MaBNhan, a.NgayThang, a.TenBNhan, a.DChi, a.DTuong } into kq
                            select new
                            {
                                ntn = kq.Key.NgayThang,
                                mabn = kq.Key.MaBNhan,
                                tenbn = kq.Key.TenBNhan,
                                dchi = kq.Key.DChi,
                                dtuong = kq.Key.DTuong,
                            }).ToList();

                 if (qbn.Count > 0)
                 {
                     foreach (var a in qbn)
                     {
                         VP them = new VP();
                         them.ntn = Convert.ToDateTime(a.ntn);
                         them.nthang = a.ntn.ToString().Substring(0, 5);
                         them.mabn = a.mabn == null ? 0 : a.mabn.Value;
                         them.tenbn = a.tenbn;
                         them.dchi = a.dchi;
                         them.dtuong = a.dtuong;
                         _VP.Add(them);

                     }
                 }
              
                 var qdv = (from tu in dataContext.TamUngs.Where(p => p.PhanLoai == 0 || p.PhanLoai == 3).Where(p => p.NgayThu >= ngaytu).Where(p => p.NgayThu <= ngayden)
                            join cd in dataContext.ChiDinhs on tu.IDTamUng equals cd.SoPhieu
                            join cls in dataContext.CLS.Where(p => p.NgayThang >= ngaytu).Where(p => p.NgayThang <= ngayden) on cd.IdCLS equals cls.IdCLS
                            join bn in dataContext.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == "Dịch vụ" || p.DTuong == "KSK") on tu.MaBNhan equals bn.MaBNhan
                            join dv in dataContext.DichVus on cd.MaDV equals dv.MaDV
                            select new { bn.MaBNhan, tu.IDTamUng, tu.NgayThu, bn.TenBNhan, bn.DChi, bn.DTuong, bn.TChung, dv.IDNhom, dv.MaDV, dv.TenRG, tu.PhanLoai, dv.DonGia, cd.TamThu }).ToList();

                 var qbndv = (from b in qdv.Where(p => p.PhanLoai == 3)
                              group new { b } by new { b.MaBNhan, b.NgayThu, b.TenBNhan, b.DChi, b.IDTamUng } into kq
                              select new
                              {
                                  ntn = kq.Key.NgayThu,
                                  sohd = kq.Key.IDTamUng,
                                  mabn = kq.Key.MaBNhan,
                                  tenbn = kq.Key.TenBNhan,
                                  dchi = kq.Key.DChi,
                                  // ck = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idck).Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idck).Sum(p => p.b.DonGia),
                                  sa = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "SA").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "SA").Sum(p => p.b.DonGia),
                                  dt = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "ĐT" || p.b.TenRG == "DT").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "ĐT" || p.b.TenRG == "DT").Sum(p => p.b.DonGia),
                                  xq = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "XQ").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "XQ").Sum(p => p.b.DonGia),
                                  nsdd = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "NSDD").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "NSDD").Sum(p => p.b.DonGia),
                                  nstmh = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "NSTMH").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Where(p => p.b.TenRG == "NSTMH").Sum(p => p.b.DonGia),
                                  //khaccd = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idcdha).Sum(p => p.b.DonGia),
                                  mau = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "Máu").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "Máu").Sum(p => p.b.DonGia),
                                  ptm = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "PTM").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "PTM").Sum(p => p.b.DonGia),
                                  nt = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "NT").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "NT").Sum(p => p.b.DonGia),
                                  shm = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "SHM").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "SHM").Sum(p => p.b.DonGia),
                                  vgb = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "VGB").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "VGB").Sum(p => p.b.DonGia),
                                  hiv = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "HIV").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "HIV").Sum(p => p.b.DonGia),
                                  // aminra = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "Aminra").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Where(p => p.b.TenRG == "Aminra").Sum(p => p.b.DonGia),
                                  //khacxn = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idxn).Sum(p => p.b.DonGia),
                                  // bobot = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Where(p => p.b.TenRG == "Bó bột").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Where(p => p.b.TenRG == "Bó bột").Sum(p => p.b.DonGia),
                                  naothai = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Where(p => p.b.TenRG == "Nạo thai" || p.b.TenRG == "Nạo Thai").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Where(p => p.b.TenRG == "Nạo thai" || p.b.TenRG == "Nạo Thai").Sum(p => p.b.DonGia),
                                  //tbe = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Where(p => p.b.TenRG == "Tắm bé" || p.b.TenRG == "Tắm Bé").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Where(p => p.b.TenRG == "Tắm bé" || p.b.TenRG == "Tắm Bé").Sum(p => p.b.DonGia),
                                  khactt = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == idttpt).Sum(p => p.b.DonGia),
                                  //khac = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom != idcdha || p.b.IDNhom != idxn || p.b.IDNhom != idttpt).Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom != idcdha || p.b.IDNhom != idxn || p.b.IDNhom != idttpt).Sum(p => p.b.DonGia),
                                  // ng = kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == ng).Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "Dịch vụ").Where(p => p.b.IDNhom == ng).Sum(p => p.b.DonGia),
                                  tonggdsk = kq.Where(p => p.b.DTuong == "KSK").Where(p => p.b.TChung == "Giám định SK" || p.b.TChung == "Sao lục BA").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "KSK").Where(p => p.b.TChung == "Giám định SK" || p.b.TChung == "Sao lục BA").Sum(p => p.b.DonGia),
                                  tongksk = kq.Where(p => p.b.DTuong == "KSK").Sum(p => p.b.DonGia) == null ? 0 : kq.Where(p => p.b.DTuong == "KSK").Sum(p => p.b.DonGia),
                                  tongdv = kq.Sum(p => p.b.DonGia) == null ? 0 : kq.Sum(p => p.b.DonGia),

                              }).ToList();
                 if (qbndv.Count > 0)
                 {
                     foreach (var b in qbndv)
                     {
                         foreach (var a in _VP)
                         {
                             if (b.ntn.ToString().Substring(0, 10) == a.ntn.ToString().Substring(0, 10) && b.mabn == a.mabn)
                             {
                                // a.sohd = Convert.ToInt32(b.sohd);
                                 a.sa = Convert.ToDouble(b.sa); a.dt = Convert.ToDouble(b.dt); a.xq = Convert.ToDouble(b.xq); a.nsdd = Convert.ToDouble(b.nsdd); a.nstmh = Convert.ToDouble(b.nstmh);
                               //  a.khaccd = Convert.ToDouble(b.khaccd - b.sa - b.dt - b.xq);
                                 a.mau = Convert.ToDouble(b.mau); a.ptm = Convert.ToDouble(b.ptm); a.nt = Convert.ToDouble(b.nt); a.shm = Convert.ToDouble(b.shm); a.vgb = Convert.ToDouble(b.vgb); a.hiv = Convert.ToDouble(b.hiv);
                                 //a.khacxn = Convert.ToDouble(b.khacxn - b.mau - b.ptm - b.nt - b.shm - b.vgb - b.hiv);
                                 a.bobot = Convert.ToDouble(b.khactt - b.naothai );
                                 a.naothai = Convert.ToDouble(b.naothai); 
                                // a.tambe = Convert.ToDouble(b.tbe);
                                 a.khac = Convert.ToDouble(b.tongdv - b.sa - b.dt - b.xq -b.nsdd-b.nstmh-b.mau-b.ptm-b.nt-b.shm-b.vgb-b.hiv-b.khactt);
                                 //  a.vp = Convert.ToDouble(b.tong-b.ck-b.khaccd-b.khacxn-b.khac-b.tbe-b.ng-b.tonggdsk-b.tongksk);
                                 a.gdsk = Convert.ToDouble(b.tonggdsk);
                                 a.ksk = Convert.ToDouble(b.tongksk);
                                 a.tongt = Convert.ToDouble(b.tongdv);
                                 a.tong = Convert.ToDouble(b.tongdv);
                             }
                         }
                     }
                 }
                 var qck = dataContext.DichVus.Where(p => p.IDNhom == idck).Select(p => new { p.DonGia }).ToList();
                 if (_VP.Count > 0)
                 {
                     foreach (var a in _VP)
                     {
                         foreach (var b in qck)
                         {
                             if (a.dtuong == "Dịch vụ")
                             {
                                 a.ck = Convert.ToDouble(b.DonGia);
                                 // a.tong = a.tong + Convert.ToDouble(b.DonGia);
                             }
                           //  a.vp = a.vp + a.ck;
                             a.tong = a.tong + a.ck;
                         }

                     }
                    
                 }
                 var qvp1 = (from vp in dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden)
                             join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             join tu in dataContext.TamUngs.Where(p=>p.PhanLoai==1||p.PhanLoai==2) on vp.MaBNhan equals tu.MaBNhan
                             join bn in dataContext.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt) on vp.MaBNhan equals bn.MaBNhan
                             select new { vp.idVPhi, vp.NgayTT, vp.MaBNhan, bn.TenBNhan, bn.DChi, bn.DTuong, vpct.TienBN, vpct.ThanhTien, vpct.MaDV }).ToList();
                 var qvp2 = (from a in qvp1//.Where(p => p.DTuong == "Dịch vụ" || p.DTuong == "KSK")
                             group new { a } by new { a.NgayTT, a.MaBNhan, a.TenBNhan, a.DChi, a.DTuong, a.idVPhi } into kq
                             select new
                             {
                                 kq.Key.idVPhi,
                                 kq.Key.NgayTT,
                                 kq.Key.MaBNhan,
                                 kq.Key.TenBNhan,
                                 kq.Key.DChi,
                                 kq.Key.DTuong,
                                 // tongvp = kq.Sum(p => p.a.ThanhTien) == null ? 0 : kq.Sum(p => p.a.ThanhTien),
                             }).ToList();
                 if (qvp2.Count > 0)
                 {

                     foreach (var b in qvp2)
                     {
                         VP them1 = new VP();
                         them1.ntn = Convert.ToDateTime(b.NgayTT);
                         them1.nthang = b.NgayTT.ToString().Substring(0, 5);
                        // them1.sohd = Convert.ToInt32(b.idVPhi);
                         them1.mabn = b.MaBNhan == null ? 0 : b.MaBNhan.Value;
                         them1.tenbn = b.TenBNhan;
                         them1.dchi = b.DChi;
                         them1.dtuong = b.DTuong;
                         //  them1.vp = Convert.ToDouble(b.tongvp);
                         _VP.Add(them1);

                     }
                 }
                  List<VP> _VP1 = new List<VP>();

                 var qvp = (from b in qvp1.Where(p => p.DTuong == "Dịch vụ" || p.DTuong == "KSK")
                            join dv in dataContext.DichVus on b.MaDV equals dv.MaDV
                            group new { b, dv } by new { b.MaBNhan, b.NgayTT } into kq
                            select new
                            {
                                ntn = kq.Key.NgayTT,
                                mabn = kq.Key.MaBNhan,
                                tongng = kq.Where(p => p.dv.IDNhom == ng).Sum(p => p.b.ThanhTien) == null ? 0 : kq.Where(p => p.dv.IDNhom == ng).Sum(p => p.b.ThanhTien),
                                tongtb = kq.Where(p => p.dv.IDNhom == idttpt && p.dv.TenRG.Contains("Tắm")).Sum(p => p.b.ThanhTien) == null ? 0 : kq.Where(p => p.dv.IDNhom == idttpt && p.dv.TenRG.Contains("Tắm")).Sum(p => p.b.ThanhTien),
                                tongvp = kq.Sum(p => p.b.ThanhTien) == null ? 0 : kq.Sum(p => p.b.ThanhTien),

                            }).ToList().Where(p => p.tongvp > 0).ToList();
                 if (qvp.Count > 0)
                 {

                     foreach (var a in qvp)
                     {
                         VP them = new VP();
                         them.ntn = Convert.ToDateTime(a.ntn);
                         them.nthang = a.ntn.ToString().Substring(0, 5);
                         them.mabn = a.mabn == null ? 0 : a.mabn.Value;
                         them.vp = Convert.ToDouble(a.tongvp - a.tongtb - a.tongng);
                         them.ng = Convert.ToDouble(a.tongng);
                         them.tambe = Convert.ToDouble(a.tongtb);
                         _VP1.Add(them);

                     }
                 }
                 var qbnbh = (from b in qvp1.Where(p => p.DTuong == "BHYT")
                              group new { b } by new { b.MaBNhan, b.NgayTT } into kq
                              select new
                              {
                                  ntn = kq.Key.NgayTT,
                                  mabn = kq.Key.MaBNhan,
                                  tongbh = kq.Sum(p => p.b.TienBN) == null ? 0 : kq.Sum(p => p.b.TienBN),

                              }).ToList();
                 if (qbnbh.Count > 0)
                 {
                     foreach (var b in qbnbh)
                     {
                         foreach (var a in _VP)
                         {
                             if (b.ntn.ToString().Substring(0, 10) == a.ntn.ToString().Substring(0, 10) && b.mabn == a.mabn)
                             {
                                 a.bhyt = Convert.ToDouble(b.tongbh);
                             }
                             a.tong = a.tong + a.bhyt;
                         }
                     }
                 }
                 var qtt = (from b in dataContext.TamUngs.Where(p => p.PhanLoai == 3)
                            join bn in dataContext.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == "Dịch vụ" || p.DTuong == "KSK") on b.MaBNhan equals bn.MaBNhan
                            group new { b } by new { b.MaBNhan } into kq
                            select new
                            {
                                mabn = kq.Key.MaBNhan,

                                tongtt = kq.Sum(p => p.b.SoTien) == null ? 0 : kq.Sum(p => p.b.SoTien),

                            }).ToList();
                 if (qtt.Count > 0)
                 {
                     foreach (var a in _VP1)
                     {
                         foreach (var b in qtt)
                         {
                             if (b.mabn == a.mabn)
                             {
                                 var ck = dataContext.DichVus.Where(p => p.IDNhom == idck).Select(p => new { p.DonGia }).ToList();
                                 if (ck.First().DonGia > 0)
                                 {
                                     a.vp = Convert.ToDouble(a.vp - b.tongtt + ck.First().DonGia);
                                 }
                                 else
                                 {
                                     a.vp = Convert.ToDouble(a.vp - b.tongtt);
                                 }
                             }
                         }
                     }
                 }
                 var qsk = (from b in dataContext.TamUngs.Where(p => p.PhanLoai == 1).Where(p => p.NgayThu >= ngaytu && p.NgayThu <= ngayden)
                            join bn in dataContext.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt).Where(p => p.DTuong == "KSK") on b.MaBNhan equals bn.MaBNhan
                            group new { b, bn } by new { bn.MaBNhan, b.NgayThu } into kq
                            select new
                            {
                                ntt = kq.Key.NgayThu,
                                mabn = kq.Key.MaBNhan,
                                tonggdsk = kq.Where(p => p.bn.DTuong == "KSK").Where(p => p.bn.TChung == "Giám định SK" || p.bn.TChung == "Sao lục BA").Sum(p => p.b.SoTien) == null ? 0 : kq.Where(p => p.bn.DTuong == "KSK").Where(p => p.bn.TChung == "Giám định SK" || p.bn.TChung == "Sao lục BA").Sum(p => p.b.SoTien),
                                tongksk = kq.Where(p => p.bn.DTuong == "KSK").Sum(p => p.b.SoTien) == null ? 0 : kq.Where(p => p.bn.DTuong == "KSK").Sum(p => p.b.SoTien),

                            }).ToList();
                 if (qsk.Count > 0)
                 {

                     foreach (var a in qsk)
                     {
                         VP them = new VP();
                         them.ntn = Convert.ToDateTime(a.ntt);
                         them.nthang = a.ntt.ToString().Substring(0, 5);
                         them.mabn = a.mabn;
                         them.ksk = Convert.ToDouble(a.tongksk) - Convert.ToDouble(a.tonggdsk);
                         them.gdsk = Convert.ToDouble(a.tonggdsk);
                         them.tong = them.tong + Convert.ToDouble(a.tongksk);
                         _VP.Add(them);

                     }
                 }
                 //int temp = 0;
                 if (_VP1.Count > 0)
                 {
                     foreach (var b in _VP1)
                     {
                         //temp = 0;
                         foreach (var a in _VP)
                         {
                             if (b.ntn.ToString().Substring(0, 10) == a.ntn.ToString().Substring(0, 10) && b.mabn == a.mabn && a.tongt == 0)
                             {
                                 a.vp = b.vp;
                                 //temp = temp + 1;
                                 a.ng = b.ng;
                                 a.tambe = b.tambe;
                                 a.tong = a.tong + a.vp + a.ng + a.tambe;
                               
                             }

                             //a.tong = a.tong + a.vp;
                         }
                     }
                 }
                 //**************
        
                 rep.DataSource = _VP.OrderBy(p => p.ntn).ToList();
                 rep.Tong.Value = _VP.Sum(p => p.tong);
                 if (!string.IsNullOrEmpty(txtTienCK.Text))
                 {
                     rep.CK.Value = Convert.ToDouble(txtTienCK.Text);
                 }
                 if (!string.IsNullOrEmpty(txtBL.Text))
                 {
                     rep.BL.Value = txtBL.Text;
                 }
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                //}
                // else MessageBox.Show("Không có dữ liệu");
                   
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}