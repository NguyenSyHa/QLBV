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
    public partial class Frm_BbKiemKie_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BbKiemKie_CL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KT()
        {
            if (lupTuNgay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn ngày tháng");
                lupTuNgay.Focus();
                return false;

            }
            if (lupKhoa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa");
                lupKhoa.Focus();
                return false;
            }
            return true;
      
        }
         private class TT
        {
            private int MaKP;
            private string TenKP;
             public int makp
            {
                set { MaKP = value; }
                get { return MaKP; }
            }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
        }
         //List<TT> _ltt = new List<TT>();
         //private class KK
         //{
         //    private string TenRG;

         //    public string TenRG1
         //    {
         //        get { return TenRG; }
         //        set { TenRG = value; }
         //    }
         //    private string TenNhomCT;

         //    public string TenNhomCT1
         //    {
         //        get { return TenNhomCT; }
         //        set { TenNhomCT = value; }
         //    }
         //    private int madv;

         //    public int madv1
         //    {
         //        get { return MaDV; }
         //        set { MaDV = value; }
         //    }
         //    private string TenDV;

         //    public string TenDV1
         //    {
         //        get { return TenDV; }
         //        set { TenDV = value; }
         //    }
         //    private string DVT;

         //    public string DVT1
         //    {
         //        get { return DVT; }
         //        set { DVT = value; }
         //    }
         //    private int TT;

         //    public int TT1
         //    {
         //        get { return TT; }
         //        set { TT = value; }
         //    }
         //    private int BA;

         //    public int BA1
         //    {
         //        get { return BA; }
         //        set { BA = value; }
         //    }
         //    private int Tong;

         //    public int Tong1
         //    {
         //        get { return Tong; }
         //        set { Tong = value; }
         //    }
         //}
         //List<KK> _KK = new List<KK>();
        private void Frm_BbKiemKie_CL_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
             DateTime _ng = System.DateTime.Now;
             var kd = (from khoa in data.KPhongs.Where(p => p.PLoai == "Lâm sàng") select new { khoa.TenKP, khoa.MaKP }).ToList();
            lupKhoa.Properties.DataSource = kd.ToList();
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in data.CanBoes
                       select cb).ToList();
            _lcanbo.Add(new CanBo { TenCB = " ", MaCB = "" });
            _lcanbo.OrderBy(p => p.TenCB);
            if (_lcanbo.Count > 0)
            {
                lupTV3.Properties.DataSource = _lcanbo;
                lupTV4.Properties.DataSource = _lcanbo;
                lupTV5.Properties.DataSource = _lcanbo;
                lupTV6.Properties.DataSource = _lcanbo;
                lupTV7.Properties.DataSource = _lcanbo;
            }
            if (!File.Exists("TextBBKiemKe_CL.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextBBKiemKe_CL.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("TextBBKiemKe_CL.txt");
            if (lines[lines.Length - 1] == "1")
            {
               
                txtTV3goi.Text = lines[lines.Length - 16];
                lupTV3.Text = lines[lines.Length - 15];
                txtChucDanh3.Text = lines[lines.Length - 14];
                txtTV4goi.Text = lines[lines.Length - 13];
                lupTV4.Text = lines[lines.Length - 12];
                txtChucDanh4.Text = lines[lines.Length - 11];
                txtTV5goi.Text = lines[lines.Length - 10];
                lupTV5.Text = lines[lines.Length - 9];
                txtChucDanh5.Text = lines[lines.Length - 8];
                txtTV6goi.Text = lines[lines.Length - 7];
                lupTV6.Text = lines[lines.Length - 6];
                txtChucDanh6.Text = lines[lines.Length - 5];
                txtTV7goi.Text = lines[lines.Length - 4];
                lupTV7.Text = lines[lines.Length - 3];
                txtChucDanh7.Text = lines[lines.Length - 2];
                
             }
        }
          

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            //_KK.Clear();
            FileStream fs = new FileStream("TextBBKiemKe_CL.txt", FileMode.Append);

            StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file

          
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
          
            writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
            writeFile.Flush();

            writeFile.Close();
            string dscb = ""; int i = 1;
            if ((txtTV3goi.Text + lupTV3.Text + txtChucDanh3).Length > 1)
            {

                dscb += i + "/ " + txtTV3goi.Text + " " + lupTV3.Text + " " + txtChucDanh3.Text + "\n";
                i++;
            }
            if ((txtTV4goi.Text + lupTV4.Text + txtChucDanh4.Text).Length > 1)
            {
                dscb += i + "/ " + txtTV4goi.Text + " " + lupTV4.Text + " " + txtChucDanh4.Text + "\n";
                i++;

            }
            if ((txtTV5goi.Text + lupTV5.Text + txtChucDanh5.Text).Length > 1)
            {
                dscb += i + "/ " + txtTV5goi.Text + " " + lupTV5.Text + " " + txtChucDanh5.Text + "\n";
                i++;
            }
            if ((txtTV6goi.Text + lupTV6.Text + txtChucDanh6.Text).Length > 1)
            {
                dscb += i + "/ " + txtTV6goi.Text + " " + lupTV6.Text + " " + txtChucDanh6.Text + "\n";
                i++;
            }
            if ((txtTV7goi.Text + lupTV7.Text + txtChucDanh7.Text).Length > 1)
            {
                dscb += i + "/ " + txtTV7goi.Text + " " + lupTV7.Text + " " + txtChucDanh7.Text + "\n";
                i++;
            }
            frmIn frm = new frmIn();


            //if (txtID.EditValue != null)
            //    _id = Convert.ToInt32(txtID.EditValue);
            DateTime tungay = DungChung.Ham.NgayDen(lupTuNgay.DateTime);
            BaoCao.Rep_BbKiemKe_CL rep = new BaoCao.Rep_BbKiemKe_CL();
            rep.NTN.Value = "Tháng " + tungay.Month.ToString() + " năm " + tungay.Year.ToString();
            rep.HomNay.Value = "Hôm nay ngày " + tungay.ToString().Substring(0, 2) + " tháng " + tungay.ToString().Substring(3, 2) + " năm " + tungay.ToString().Substring(6, 4) + " Chúng tôi gồm:";
            rep.TV1.Value = dscb.ToString();
            if (ckNhom.Checked == true) { rep.InNhom.Value = 1; }
            if (ckTieuNhom.Checked == true) { rep.InTieuNhom.Value = 1; }

            int _kho = 0; 
            if (lupKhoa.EditValue.ToString() != null)
            {
                _kho = Convert.ToInt32( lupKhoa.EditValue);
            }
            var k = (from kp in data.KPhongs.Where(p => p.MaKP == _kho) select new { kp.TenKP }).ToList();
            if (k.Count > 0) { rep.Khoa.Value = k.First().TenKP; }
                             var dvu=(from  dv in data.DichVus   join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                  join nhom in data.NhomDVs.Where(p=>p.Status==1) on dv.IDNhom equals nhom.IDNhom
                                  select new{dv.MaDV,tn.TenRG,nhom.TenNhom}) .ToList();
                             List<KPhong> _lKhoaP = data.KPhongs.Where(p => p.PLoai == "Tủ trực").Where(p => p.NhomKP == _kho).ToList();
            int makp=-1;
            string tenkp="";
            if(_lKhoaP.Count>0){
            makp= _lKhoaP.First().MaKP;
            tenkp=_lKhoaP.First().TenKP;
            }
             //tính số lượng nhập tủ trực
            //var qntt = (from xd in data.NhapDs.Where(p => p.NgayNhap <= tungay).Where(p => p.PLoai == 2)  .Where(p=>p.MaKPnx==makp)
            //            join xdct in data.NhapDcts on xd.IDNhap equals xdct.IDNhap
            //            group new { xd, xdct } by new { xdct.MaDV,xdct.DonGia } into kq
            //            select new        {
            //                                                     kq.Key.MaDV,
            //                                          SLTTn=kq.Sum(p=>p.xdct.SoLuongX),
            //                                          TTTTn=kq.Sum(p=>p.xdct.ThanhTienX),
            //            }
            //                // SoLuong = kq.Key.SoLuongX
            //            }).ToList();
            //var qxtt = (from dt in data.DThuocs.Where(p => p.NgayKe <= tungay).Where(p => p.MaKP == _kho).Where(p=>p.Status==1)
            //            join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
            //            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
            //            join kp in data.KPhongs.Where(p => p.PLoai == "Tủ trực").Where(p => p.NhomKP == _kho) on dt.MaKXuat equals kp.MaKP
            //            // where (from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
            //            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //            join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
            //            group new { dt, dtct } by new { dv.MaDV, dv.TenDV, dv.DonVi, tn.TenRG, nhom.TenNhomCT } into kq
            //            select new
            //            {
            //                TenRG = kq.Key.TenRG,
            //                TenNhomCt = kq.Key.TenNhomCT,
            //                MaDV = kq.Key.MaDV,
            //                TenDV = kq.Key.TenDV,
            //                DonVi = kq.Key.DonVi,
            //                SoLuongN = kq.Where(p => p.dt.IDDon == -1).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dt.IDDon == -1).Sum(p => p.dtct.SoLuong),
            //                SoLuongX = kq.Where(p => p.dt.IDDon != -1).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dt.IDDon != -1).Sum(p => p.dtct.SoLuong),
            //                SoLuongBA = kq.Where(p => p.dt.IDDon == -2).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dt.IDDon != -2).Sum(p => p.dtct.SoLuong),
            //            }).ToList();
            //var qba = (from dt in data.DThuocs.Where(p => p.NgayKe <= tungay).Where(p => p.MaKP == _kho)
            //           join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
            //              join dv in data.DichVus on dtct.MaDV equals dv.MaDV
            //             join kp in data.KPhongs.Where(p => p.PLoai != "Tủ trực").Where(p => p.NhomKP != _kho) on dt.MaKXuat equals kp.MaKP
            //             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //           join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
            //           where !(from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
            //           group new { dt, dtct } by new { dv.MaDV, dv.TenDV, dv.DonVi, tn.TenRG, nhom.TenNhomCT } into kq
            //           select new
            //           {
            //               TenRG = kq.Key.TenRG,
            //               TenNhomCt = kq.Key.TenNhomCT,
            //               MaDV = kq.Key.MaDV,
            //               TenDV = kq.Key.TenDV,
            //               DonVi = kq.Key.DonVi,
            //               SoLuongN = kq.Where(p => p.dt.IDDon == -2).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dt.IDDon == -2).Sum(p => p.dtct.SoLuong),
            //               SoLuongX = kq.Where(p => p.dt.IDDon == -1).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dt.IDDon == -1).Sum(p => p.dtct.SoLuong),
            //               SoLuongBA = kq.Where(p => p.dt.IDDon != -1).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dt.IDDon != -1).Sum(p => p.dtct.SoLuong),
                       
            //           }).ToList();
            //var qtt = qntt.Union(qxtt).Union(qba).Select(x => new { x.TenRG, x.TenNhomCt, x.MaDV, x.TenDV, x.DonVi, x.SoLuongN, x.SoLuongX, x.SoLuongBA }).ToList();

            //if (qtt.Count > 0)
            //{
            //    var q = (from qt in qtt
            //             group new { qt } by new { qt.TenNhomCt, qt.TenRG, qt.MaDV, qt.TenDV, qt.DonVi } into kq
            //             select new
            //             {
            //                 TenNhomCT1 = kq.Key.TenNhomCt,
            //                 TenRG1 = kq.Key.TenRG,
            //                 MaDV1 = kq.Key.MaDV,
            //                 TenDV1 = kq.Key.TenDV,
            //                 DonVi1 = kq.Key.DonVi,
            //                 TT1 = (kq.Sum(p => p.qt.SoLuongN) - kq.Sum(p => p.qt.SoLuongX)) == null ? 0 : (kq.Sum(p => p.qt.SoLuongN) - kq.Sum(p => p.qt.SoLuongX)),
            //                 BA1 = kq.Sum(p => p.qt.SoLuongBA) == null ? 0 : kq.Sum(p => p.qt.SoLuongBA),
            //                 // SoLuongBA = kq.Key.SoLuongBA == null ? 0 : kq.Key.SoLuongBA,
            //                 Tong1 = (kq.Sum(p => p.qt.SoLuongN) - kq.Sum(p => p.qt.SoLuongX) + kq.Sum(p => p.qt.SoLuongBA)) == null ? 0 : (kq.Sum(p => p.qt.SoLuongN) - kq.Sum(p => p.qt.SoLuongX) + kq.Sum(p => p.qt.SoLuongBA)),

            //             }
            //                 ).ToList();
      
            //    rep.DataSource = q.Where(p => p.Tong1 > 0).OrderBy(p => p.TenDV1).ToList();
            //    rep.DataBinding();
            //    rep.CreateDocument();
            //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //    frm.ShowDialog();
            //}
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}