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
    public partial class frmTsBbKKThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBbKKThuoc()
        {
            InitializeComponent();
        }
        //int _id = 0;
        //public frmTsBbKKThuoc(int id)
        //{
        //    InitializeComponent();
        //    _id = id;
        //}
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        //private bool KT()
        //{
        //    if (grvNhapD.CheckLoade  ==false)
        //    {
        //        MessageBox.Show("Bạn chưa chọn chứng từ để in Biên bản");
        //        txtID.Focus();
        //        return false;
        //    }

        //    else return true;
        //}
        private class ID
        {
            private int IDNhap;

            public int IDNhap1
            {
                get { return IDNhap; }
                set { IDNhap = value; }
            }
            private string SoCT;

            public string SoCT1
            {
                get { return SoCT; }
                set { SoCT = value; }
            }
            private string NgayNhap;

            public string NgayNhap1
            {
                get { return NgayNhap; }
                set { NgayNhap = value; }
            }
            private string LiDo;

            public string LiDo1
            {
                get { return LiDo; }
                set { LiDo = value; }
            }
            private bool Chon;

            public bool chon
            { set { Chon = value; } get { return Chon; } }


        }
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
        List<KHO> _lkho = new List<KHO>();
        List<ID> _ID = new List<ID>();
        private void frmTsBbKKThuoc_Load(object sender, EventArgs e)
        {
            
            _ID.Clear();
            lupTuNgay.Focus();
            if(DungChung.Bien.MaBV == "27023")
            {
                cmbKK.Properties.Items.Clear();
                this.cmbKK.Properties.Items.AddRange(new object[] 
                {
                  "Thuốc",
                  "Hóa chất",
                  "VTYT tiêu hao",
                  "Hàng Hóa"
                });
            }    
            if (DungChung.Bien.MaBV != "30003")
            {
                lblTV6.Visible = false;
                lblTV7.Visible = false;
                lblTV8.Visible = false;

                lblChucDanh9.Visible = false;
                lblChucDanh10.Visible = false;
                lblChucDanh11.Visible = false;

                txtTV9goi.Visible = false;
                txtTV10goi.Visible = false;
                txtTV11goi.Visible = false;

                lupTV9.Visible = false;
                lupTV10.Visible = false;
                lupTV11.Visible = false;

                txtChucDanh9.Visible = false;
                txtChucDanh10.Visible = false;
                txtChucDanh11.Visible = false;
            }
            if(DungChung.Bien.MaBV == "27001")
            {
                rgNamThang.Visible = true;

                lblTV6.Visible = true;
                lblTV7.Visible = true;
                lblTV8.Visible = true;
                lblTV9.Visible = true;
                lblTV10.Visible = true;
                lblTV11.Visible = true;
                lblTV12.Visible = true;

                lblChucDanh9.Visible = true;
                lblChucDanh10.Visible = true;
                lblChucDanh11.Visible = true;
                lblChucDanh12.Visible = true;
                lblChucDanh13.Visible = true;
                lblChucDanh14.Visible = true;
                lblChucDanh15.Visible = true;

                txtTV9goi.Visible = true;
                txtTV10goi.Visible = true;
                txtTV11goi.Visible = true;
                txtTV12goi.Visible = true;
                txtTV13goi.Visible = true;
                txtTV14goi.Visible = true;
                txtTV15goi.Visible = true;

                lupTV9.Visible = true;
                lupTV10.Visible = true;
                lupTV11.Visible = true;
                lupTV12.Visible = true;
                lupTV13.Visible = true;
                lupTV14.Visible = true;
                lupTV15.Visible = true;

                txtChucDanh9.Visible = true;
                txtChucDanh10.Visible = true;
                txtChucDanh11.Visible = true;
                txtChucDanh12.Visible = true;
                txtChucDanh13.Visible = true;
                txtChucDanh14.Visible = true;
                txtChucDanh15.Visible = true;
            }
            else
            {


                //labelControl1.Location = new Point(this.labelControl1.Location.X, this.labelControl1.Location.Y + 245);
                //txtYKDX.Location = new Point(this.txtYKDX.Location.X, this.txtYKDX.Location.Y + 245);
                //labelControl2.Location = new Point(this.labelControl2.Location.X, this.labelControl2.Location.Y + 245);
                //txtKKTai.Location = new Point(this.txtKKTai.Location.X, this.txtKKTai.Location.Y + 245);
                //ckInNuocSX.Location = new Point(this.ckInNuocSX.Location.X, this.ckInNuocSX.Location.Y + 245);
                //rgChonMau.Location = new Point(this.rgChonMau.Location.X, this.rgChonMau.Location.Y + 245);
                //rgNamThang.Location = new Point(this.rgNamThang.Location.X, this.rgNamThang.Location.Y + 245);
                //btnThoat.Location = new Point(this.btnThoat.Location.X, this.btnThoat.Location.Y + 245);
                //btnInPhieu.Location = new Point(this.btnInPhieu.Location.X, this.btnInPhieu.Location.Y + 245);

                //panelControl1.Size = new Size(818, 512);
                //this.Size = new Size(841, 631);
            }
            if (DungChung.Bien.MaBV == "20001")
                chkMadv.Visible = true;
            if (DungChung.Bien.MaBV == "27021" && cmbKK.SelectedIndex == 0)
                chkInTien.Checked = true;
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in data.CanBoes
                       select cb).ToList();
            _lcanbo.OrderBy(p => p.TenCB);
            List<string> dscb = _lcanbo.Select(p => p.TenCB).Distinct().ToList();
            if (_lcanbo.Count > 0)
            {
                foreach (var item in dscb)
                {
                    lupTV1.Properties.Items.Add(item);
                    lupTV2.Properties.Items.Add(item);
                    lupTV3.Properties.Items.Add(item);
                    lupTV4.Properties.Items.Add(item);
                    lupTV5.Properties.Items.Add(item);
                    lupTV6.Properties.Items.Add(item);
                    lupTV7.Properties.Items.Add(item);
                    lupTV8.Properties.Items.Add(item);
                    lupTV9.Properties.Items.Add(item);
                    lupTV10.Properties.Items.Add(item);
                    lupTV11.Properties.Items.Add(item);
                    lupTV12.Properties.Items.Add(item);
                    lupTV13.Properties.Items.Add(item);
                    lupTV14.Properties.Items.Add(item);
                    lupTV15.Properties.Items.Add(item);
                }

                //lupTV2.Properties.DataSource = _lcanbo;
                //lupTV3.Properties.DataSource = _lcanbo;
                //lupTV4.Properties.DataSource = _lcanbo;
                //lupTV5.Properties.DataSource = _lcanbo;
                //lupTV6.Properties.DataSource = _lcanbo;
                //lupTV7.Properties.DataSource = _lcanbo;
                //lupTV8.Properties.DataSource = _lcanbo;
            }
            lupTuNgay.DateTime = System.DateTime.Now;
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
            var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _ng && p.NgayNhap <= _ng).Where(p => p.PLoai == 4)
                      select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
            if (qn.Count > 0)
            {
                ID themmoi1 = new ID();
                themmoi1.SoCT1 = "";
                themmoi1.IDNhap1 = 0;
                themmoi1.NgayNhap1 = "";
                themmoi1.LiDo1 = "Chọn tất cả";
                themmoi1.chon = true;
                _ID.Add(themmoi1);
                foreach (var a in qn)
                {
                    ID themmoi = new ID();
                    themmoi.IDNhap1 = a.IDNhap;
                    themmoi.SoCT1 = a.SoCT;
                    themmoi.NgayNhap1 = a.NgayNhap.ToString();
                    themmoi.LiDo1 = a.GhiChu;
                    themmoi.chon = true;
                    _ID.Add(themmoi);
                }


                grcNhapD.DataSource = _ID.ToList();

            }

            //var q = (from nd in data.NhapDs.Where(p => p.PLoai == 4) select new { nd.SoCT, nd.IDNhap, nd.NgayNhap }).OrderByDescending(p=>p.IDNhap).ToList();
            //txtID.Properties.DataSource = q.ToList();

            if (!File.Exists("TextBBKiemKeThuoc.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextBBKiemKeThuoc.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("TextBBKiemKeThuoc.txt");
            if (lines[lines.Length - 1] == "1")
            {
                txtTV1goi.Text = lines[lines.Length - 48];
                lupTV1.Text = lines[lines.Length - 47];
                txtChucDanh1.Text = lines[lines.Length - 46];
                txtTV2goi.Text = lines[lines.Length - 45];
                lupTV2.Text = lines[lines.Length - 44];
                txtChucDanh2.Text = lines[lines.Length - 43];
                txtTV3goi.Text = lines[lines.Length - 42];
                lupTV3.Text = lines[lines.Length - 41];
                txtChucDanh3.Text = lines[lines.Length - 40];
                txtTV4goi.Text = lines[lines.Length - 39];
                lupTV4.Text = lines[lines.Length - 38];
                txtChucDanh4.Text = lines[lines.Length - 37];
                txtTV5goi.Text = lines[lines.Length - 36];
                lupTV5.Text = lines[lines.Length - 35];
                txtChucDanh5.Text = lines[lines.Length - 34];
                txtTV6goi.Text = lines[lines.Length - 33];
                lupTV6.Text = lines[lines.Length - 32];
                txtChucDanh6.Text = lines[lines.Length - 31];
                txtTV7goi.Text = lines[lines.Length - 30];
                lupTV7.Text = lines[lines.Length - 29];
                txtChucDanh7.Text = lines[lines.Length - 28];

                txtTV8goi.Text = lines[lines.Length - 27];
                lupTV8.Text = lines[lines.Length - 26];
                txtChucDanh8.Text = lines[lines.Length - 25];

                txtTV9goi.Text = lines[lines.Length - 24];
                lupTV9.Text = lines[lines.Length - 23];
                txtChucDanh9.Text = lines[lines.Length - 22];

                txtTV10goi.Text = lines[lines.Length - 21];
                lupTV10.Text = lines[lines.Length - 20];
                txtChucDanh10.Text = lines[lines.Length - 19];

                txtTV11goi.Text = lines[lines.Length - 18];
                lupTV11.Text = lines[lines.Length - 17];
                txtChucDanh11.Text = lines[lines.Length - 16];

                txtTV12goi.Text = lines[lines.Length - 15];
                lupTV12.Text = lines[lines.Length - 14];
                txtChucDanh12.Text = lines[lines.Length - 13];

                txtTV13goi.Text = lines[lines.Length - 12];
                lupTV13.Text = lines[lines.Length - 11];
                txtChucDanh13.Text = lines[lines.Length - 10];

                txtTV14goi.Text = lines[lines.Length - 9];
                lupTV14.Text = lines[lines.Length - 8];
                txtChucDanh14.Text = lines[lines.Length - 7];

                txtTV15goi.Text = lines[lines.Length - 6];
                lupTV15.Text = lines[lines.Length - 5];
                txtChucDanh15.Text = lines[lines.Length - 4];

                txtKKTai.Text = lines[lines.Length - 3];
                txtYKDX.Text = lines[lines.Length - 2];
            }

            //  txtID.EditValue = _id;
        }

        private string kytutrang(string lengttencb, int dodai)
        {
            string a = "";
            int i = lengttencb.Trim().Length;
            for (; i < dodai; i++)
            {
                a += " ";
            }
            return a;
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            int _MauIn = rgChonMau.SelectedIndex;
            FileStream fs = new FileStream("TextBBKiemKeThuoc.txt", FileMode.Append);
            bool Thang = rgNamThang.SelectedIndex == 0 ? true : false;
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

            writeFile.WriteLine(txtTV8goi.Text);
            writeFile.WriteLine(lupTV8.Text);
            writeFile.WriteLine(txtChucDanh8.Text);

            writeFile.WriteLine(txtTV9goi.Text);
            writeFile.WriteLine(lupTV9.Text);
            writeFile.WriteLine(txtChucDanh9.Text);

            writeFile.WriteLine(txtTV10goi.Text);
            writeFile.WriteLine(lupTV10.Text);
            writeFile.WriteLine(txtChucDanh10.Text);

            writeFile.WriteLine(txtTV11goi.Text);
            writeFile.WriteLine(lupTV11.Text);
            writeFile.WriteLine(txtChucDanh11.Text);

            writeFile.WriteLine(txtTV12goi.Text);
            writeFile.WriteLine(lupTV12.Text);
            writeFile.WriteLine(txtChucDanh12.Text);

            writeFile.WriteLine(txtTV13goi.Text);
            writeFile.WriteLine(lupTV13.Text);
            writeFile.WriteLine(txtChucDanh13.Text);

            writeFile.WriteLine(txtTV14goi.Text);
            writeFile.WriteLine(lupTV14.Text);
            writeFile.WriteLine(txtChucDanh14.Text);

            writeFile.WriteLine(txtTV15goi.Text);
            writeFile.WriteLine(lupTV15.Text);
            writeFile.WriteLine(txtChucDanh15.Text);

            writeFile.WriteLine(txtKKTai.Text);
            writeFile.WriteLine(txtYKDX.Text);
            writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
            writeFile.Flush();

            writeFile.Close();

            frmIn frm = new frmIn();


            //if (txtID.EditValue != null)
            //    _id = Convert.ToInt32(txtID.EditValue);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            string _id = "";
            int _id1 = 0; int _id2 = 0; int _id3 = 0; int _id4 = 0; int _id5 = 0; int _id6 = 0; int _id7 = 0; int _id8 = 0; int _id9 = 0; int _id10 = 0; int _id11 = 0; int _id12 = 0; int _id13 = 0; int _id14 = 0;
            for (int i = 0; i < _ID.Count; i++)
            {
                if (_ID.Skip(i).First().chon == true)
                {
                    switch (i)
                    {
                        case 0:
                            _id1 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            break;
                        case 1:
                            _id2 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString();
                            break;
                        case 2:
                            _id3 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString();
                            break;
                        case 3:
                            _id4 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString();
                            break;
                        case 4:
                            _id5 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id6.ToString();
                            break;
                        case 5:
                            _id6 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString();
                            break;
                        case 6:
                            _id7 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString();
                            break;
                        case 7:
                            _id8 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString();
                            break;
                        case 8:
                            _id9 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString() + ", " + _id9.ToString();
                            break;
                        case 9:
                            _id10 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString() + ", " + _id9.ToString() + ", " + _id10.ToString();
                            break;
                        case 10:
                            _id11 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString() + ", " + _id9.ToString() + ", " + _id10.ToString() + ", " + _id10.ToString();
                            break;
                        case 11:
                            _id12 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString() + ", " + _id9.ToString() + ", " + _id11.ToString() + ", " + _id12.ToString();
                            break;
                        case 12:
                            _id13 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString() + ", " + _id9.ToString() + ", " + _id11.ToString() + ", " + _id12.ToString() + ", " + _id13.ToString();
                            break;
                        case 13:
                            _id14 = Convert.ToInt32(_ID.Skip(i).First().IDNhap1);
                            _id = _id2.ToString() + ", " + _id3.ToString() + ", " + _id4.ToString() + ", " + _id4.ToString() + ", " + _id5.ToString() + ", " + _id6.ToString() + ", " + _id7.ToString() + ", " + _id8.ToString() + ", " + _id9.ToString() + ", " + _id11.ToString() + ", " + _id12.ToString() + ", " + _id13.ToString() + ", " + _id14.ToString();
                            break;

                    }
                }
            }
            var par = (from nhapd in data.NhapDs.Where(p => p.PLoai == 3 || p.PLoai == 4).Where(p => p.IDNhap == _id1 || p.IDNhap == _id2 || p.IDNhap == _id3 || p.IDNhap == _id4 || p.IDNhap == _id5 || p.IDNhap == _id6 || p.IDNhap == _id7 || p.IDNhap == _id8 || p.IDNhap == _id9 || p.IDNhap == _id10 || p.IDNhap == _id11 || p.IDNhap == _id12 || p.IDNhap == _id13 || p.IDNhap == _id14)
                       join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                       select new { kp.TenKP }).Distinct().ToList();
            var q1 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 3 || p.PLoai == 4).Where(p => p.IDNhap == _id1 || p.IDNhap == _id2 || p.IDNhap == _id3 || p.IDNhap == _id4 || p.IDNhap == _id5 || p.IDNhap == _id6 || p.IDNhap == _id7 || p.IDNhap == _id8 || p.IDNhap == _id9 || p.IDNhap == _id10 || p.IDNhap == _id11 || p.IDNhap == _id12 || p.IDNhap == _id13 || p.IDNhap == _id14)
                      join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                      join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                      join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                      select new { nhapd, nhapdct, dv, tieunhomdv }).ToList();
            var q = (from nhapd in q1
                     group new { nhapd } by new
                     {
                         nhapd.tieunhomdv.TenTN,
                         nhapd.dv.SoDK,
                         nhapd.dv.MaTam,
                         nhapd.dv.TenDV,
                         nhapd.dv.NuocSX,
                         NgayNhap = nhapd.nhapd.NgayNhap.Value.Date,
                         nhapd.nhapd.SoCT,
                         nhapd.nhapd.PLoai,
                         nhapd.nhapdct.SoLo,
                         nhapd.nhapdct.DonVi,
                         nhapd.nhapdct.DonGia,
                         nhapd.dv.MaDV,
                         HanDung = nhapd.nhapdct.HanDung == null ? "" : nhapd.nhapdct.HanDung.Value.Date.ToShortDateString(),
                         //  nhapd.nhapdct.SoLuongKK, nhapd.nhapdct.ThanhTienKK, nhapd.nhapdct.SoLuongX , nhapd.nhapd.IDNhap
                     } into kq
                     select new
                     {
                         MaTam = kq.Key.MaTam,
                         NgayThang = kq.Key.NgayNhap,
                         SoCT = kq.Key.SoCT,
                         TenTieuNhom = kq.Key.TenTN,
                         TenDV = kq.Key.TenDV,
                         kq.Key.MaDV,
                         DonVi = kq.Key.DonVi,
                         SoLo = kq.Key.SoLo ?? "",
                         NuocSX = kq.Key.NuocSX,
                         HanDung = kq.Key.HanDung,
                         DonGia = kq.Key.DonGia,
                         SoDK = kq.Key.SoDK,
                         NuocSXGr = kq.Key.NuocSX == null ? 1 : ((kq.Key.NuocSX.ToLower().Trim().Contains("việt nam") || kq.Key.NuocSX.ToLower().Trim() == "vnam" || kq.Key.NuocSX.ToLower().Trim() == "vịêt nam" || kq.Key.NuocSX.ToLower().Trim() == "vn" || kq.Key.NuocSX.ToLower().Trim() == "v.nam") ? 1 : 2),
                         SoLuongSS = kq.Where(p => p.nhapd.nhapd.PLoai == 4).Sum(p => p.nhapd.nhapdct.SoLuongKK),
                         ThanhTienSS = kq.Where(p => p.nhapd.nhapd.PLoai == 4).Sum(p => p.nhapd.nhapdct.ThanhTienKK),
                         SoLuongTT = kq.Where(p => p.nhapd.nhapd.PLoai == 4).Sum(p => p.nhapd.nhapdct.SoLuongKK),
                         ThanhTienTT = kq.Where(p => p.nhapd.nhapd.PLoai == 4).Sum(p => p.nhapd.nhapdct.ThanhTienKK),
                         HongVo = kq.Where(p => p.nhapd.nhapd.PLoai == 3).Sum(p => p.nhapd.nhapdct.SoLuongX)
                     }).ToList().OrderBy(p => p.NuocSXGr).ThenBy(p => p.TenDV).Select(p => new
                     {
                         NgayThang = p.NgayThang.ToString().Substring(0, 10),
                         p.SoCT,
                         p.TenTieuNhom,
                         p.MaTam,
                         p.TenDV,
                         p.DonVi,
                         p.SoLo,
                         p.SoDK,
                         p.NuocSX,
                         p.NuocSXGr,
                         p.DonGia,
                         p.SoLuongSS,
                         p.ThanhTienSS,
                         p.SoLuongTT,
                         p.ThanhTienTT,
                         p.HongVo,
                         p.MaDV,
                         HanDung = p.HanDung, // p.HanDung.ToString().PadLeft(10),
                     }).Where(p => p.SoLuongTT != 0).OrderBy(p => p.TenTieuNhom).ThenBy(p => p.TenDV).ToList();

            
            #region Thuốc
            if (cmbKK.EditValue == "Thuốc")
            {
                #region 27021
                if (_MauIn == 1)
                {
                    #region in bc
                    BaoCao.repBbKKThuoc_ThanhTien_27021 rep = new BaoCao.repBbKKThuoc_ThanhTien_27021();
                    if (chkTT.Checked == true)
                    { rep.TT.Value = "A"; }
                    else { rep.TT.Value = "X"; }
                    rep.So.Value = _id;
                    if (par.Count > 0)
                    {
                        if (par.Count == 1)
                        {
                            rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                            rep.Khoa.Value = par.First().TenKP;
                        }
                        if (par.Count > 1)
                        {
                            rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                            if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                            if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                            if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                            if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                        }
                    }
                    if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                    //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                    int i = 1; int j = 0;
                    string dscb = "", dscd = "";
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
                    if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                        i++;
                    }
                    if ((txtTV9goi.Text + lupTV9.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                        i++;
                    }
                    if ((txtTV10goi.Text + lupTV10.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                        i++;
                    }
                    if ((txtTV11goi.Text + lupTV11.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
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
                    if ((txtChucDanh8.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh9.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh10.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh11.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                    }


                    rep.TV1goi.Value = dscb.ToString();
                    rep.TV2goi.Value = dscd.ToString();
                    rep.TV1.Value = lupTV1.Text;
                    rep.TV2.Value = lupTV2.Text;
                    rep.TV3.Value = lupTV3.Text;
                    rep.TV4.Value = lupTV4.Text;
                    rep.TV5.Value = lupTV5.Text;
                    rep.TV6.Value = lupTV6.Text;
                    rep.TV7.Value = lupTV7.Text;
                    rep.TV8.Value = lupTV8.Text;
                    //rep.TV9.Value = lupTV9.Text;
                    //rep.TV10.Value = lupTV10.Text;
                    //rep.TV11.Value = lupTV11.Text;

                    rep.DaKKTai.Value = txtKKTai.Text;
                    rep.YKDX.Value = txtYKDX.Text;
                    double TTong = 0;
                    if (q.Count > 0)
                    {
                        TTong = Math.Round(Math.Round(q.Sum(p => p.ThanhTienTT), 2), 0);
                    }
                    if (TTong > 0)
                    {
                        rep.SoTienBC.Value = "Số tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(TTong, " đồng./.");
                    }
                    else { rep.SoTienBC.Value = "Số tiền bằng chữ: "; }
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
                else if (_MauIn == 2)
                {
                    BaoCao.repBbKKThuoc_C32 rep = new BaoCao.repBbKKThuoc_C32();
                    if (chkTT.Checked == true)
                    { rep.TT.Value = "A"; }
                    else { rep.TT.Value = "X"; }
                    rep.So.Value = _id;
                    if (par.Count > 0)
                    {
                        if (par.Count == 1)
                        {
                            rep.ThangNam.Value = "- Thời điểm kiểm kê " + "....giờ...." + " ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                            rep.Khoa.Value = par.First().TenKP;
                        }
                        if (par.Count > 1)
                        {
                            rep.ThangNam.Value = "- Thời điểm kiểm kê " + "....giờ...." + " ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                            if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                            if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                            if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                            if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                        }
                    }
                    if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                    //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                    int i = 1; int j = 0;
                    string dscb = "", dscd = "";
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
                    if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                        i++;
                    }
                    if ((txtTV9goi.Text + lupTV9.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                        i++;
                    }
                    if ((txtTV10goi.Text + lupTV10.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                        i++;
                    }
                    if ((txtTV11goi.Text + lupTV11.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
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
                    } if ((txtChucDanh8.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh9.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh10.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh11.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                    }


                    rep.TV1goi.Value = dscb.ToString();
                    rep.TV2goi.Value = dscd.ToString();
                    rep.TV1.Value = lupTV1.Text;
                    rep.TV2.Value = lupTV2.Text;
                    rep.TV3.Value = lupTV3.Text;
                    rep.TV4.Value = lupTV4.Text;
                    rep.TV5.Value = lupTV5.Text;
                    rep.TV6.Value = lupTV6.Text;
                    rep.TV7.Value = lupTV7.Text;
                    rep.TV8.Value = lupTV8.Text;
                    //rep.TV9.Value = lupTV9.Text;
                    //rep.TV10.Value = lupTV10.Text;
                    //rep.TV11.Value = lupTV11.Text;

                    rep.DaKKTai.Value = txtKKTai.Text;
                    rep.YKDX.Value = txtYKDX.Text;


                    if (q.Count > 0)
                    {
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else { MessageBox.Show("Không có dữ liệu"); }
                }


                else if (_MauIn == 3) // bbkk ydct Sơn La 14017
                {
                    BaoCao.repBbKKThuoc14017 rep = new BaoCao.repBbKKThuoc14017();
                    var khokk = "";
                    if (chkTT.Checked == true)
                    { rep.TT.Value = "A"; }
                    else { rep.TT.Value = "X"; }
                    rep.So.Value = _id;
                    if (par.Count > 0)
                    {
                        if (par.Count == 1)
                        {
                            rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                            rep.Khoa.Value = par.First().TenKP;
                            khokk = par.First().TenKP;
                        }
                        if (par.Count > 1)
                        {
                            rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                            if (par.Count == 2)
                            {
                                rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP;
                                khokk = par.First().TenKP + ", " + par.Skip(1).First().TenKP;

                            }
                            if (par.Count == 3)
                            {
                                rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP;
                                khokk = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP;
                            }
                            if (par.Count == 4)
                            {
                                rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP;
                                khokk = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP;

                            }
                            if (par.Count == 5)
                            {
                                rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP;
                                khokk = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP;

                            }
                            if (par.Count > 5)
                            {
                                rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP;
                                khokk = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP;

                            }
                        }
                    }
                    if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                    int i = 1; int j = 0;
                    string dscb = "", dscd = "";
                    if ((txtTV1goi.Text + lupTV1.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                        i++;
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
                    if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                        i++;
                    }
                    if ((txtTV9goi.Text + lupTV9.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                        i++;
                    }
                    if ((txtTV10goi.Text + lupTV10.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                        i++;
                    }
                    if ((txtTV11goi.Text + lupTV11.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                        i++;
                    }

                    if (DungChung.Bien.MaBV == "27001")
                    {
                        if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
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
                    if ((txtChucDanh8.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh9.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh10.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                    }
                    if ((txtChucDanh11.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                    }

                    if (DungChung.Bien.MaBV == "27001")
                    {
                        if ((txtChucDanh12.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if ((txtChucDanh13.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if ((txtChucDanh14.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if ((txtChucDanh15.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                    }


                    txtKKTai.Text = txtKKTai.Text == "" ? khokk + ". Từ ngày " + lupTuNgay.Text + " 00:00 đến ngày " + lupDenNgay.Text + " 23:59 " : txtKKTai.Text;
                    rep.TV1goi.Value = dscb.ToString();
                    rep.TV2goi.Value = dscd.ToString();
                    rep.TV1.Value = lupTV1.Text;
                    rep.TV2.Value = lupTV2.Text != "" ? lupTV2.Text + ".........." : "";
                    rep.TV3.Value = lupTV3.Text;
                    rep.TV4.Value = lupTV4.Text != "" ? lupTV4.Text + ".........." : "";
                    rep.TV5.Value = lupTV5.Text != "" ? lupTV5.Text + ".........." : "";
                    rep.TV6.Value = lupTV6.Text != "" ? lupTV6.Text + ".........." : "";
                    rep.TV7.Value = lupTV7.Text != "" ? lupTV7.Text + ".........." : "";
                    rep.TV8.Value = lupTV8.Text != "" ? lupTV8.Text + ".........." : "";

                    if (DungChung.Bien.MaBV == "27001")
                    {
                        rep.Parameters["TV12"].Value = lupTV12.Text != "" ? lupTV12.Text + ".........." : "";
                        rep.Parameters["TV13"].Value = lupTV13.Text != "" ? lupTV13.Text + ".........." : "";
                        rep.Parameters["TV14"].Value = lupTV14.Text != "" ? lupTV14.Text + ".........." : "";
                        rep.Parameters["TV15"].Value = lupTV15.Text != "" ? lupTV15.Text + ".........." : "";
                    }

                    rep.DaKKTai.Value = txtKKTai.Text;
                    rep.YKDX.Value = txtYKDX.Text;
                    //double TTong = 0;
                    //if (q.Count > 0)
                    //{
                    //    TTong = Math.Round(Math.Round(q.Sum(p => p.ThanhTienTT), 2), 0);
                    //}
                    //if (TTong > 0)
                    //{
                    //rep.SoTienBC.Value = "Số tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(TTong, " đồng./.");
                    //}
                    //else { //rep.SoTienBC.Value = "Số tiền bằng chữ: "; 
                    //}
                    rep.DataSource = q;


                    #region xuất Excel
                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 2;
                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 2, _arr.Length];
                    string[] _tieude = { "STT", "Tên thuốc - Hàm lượng", "Đơn vị", "SĐK hoặc GPNK", "Số kiểm soát", "Nước sản xuất", "Hạn dùng", "Giá", "Số lượng sổ sách", "Thành tiền sổ sách", "Số lượng thực tế", "Thành tiền thực tế", "Hỏng, vỡ", "Ghi chú" };
                    for (int k = 0; k < _tieude.Length; k++)
                    {
                        DungChung.Bien.MangHaiChieu[0, k] = _tieude[k].ToUpper();
                        DungChung.Bien.MangHaiChieu[1, k] = (k + 1).ToString();
                    }

                    //for (int i = 0; i <= 17; i++) {
                    //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                    //}
                    foreach (var r in q)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num - 1;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.SoDK;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoLo;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.NuocSX;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.HanDung;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;//string.Format(DungChung.Bien.FormatString[1], r.DonGia);
                        DungChung.Bien.MangHaiChieu[num, 8] = r.SoLuongSS;//string.Format(DungChung.Bien.FormatString[0], r.SoLuongSS);
                        DungChung.Bien.MangHaiChieu[num, 9] = r.ThanhTienSS;//string.Format(DungChung.Bien.FormatString[1], r.ThanhTienSS);
                        if (chkTT.Checked == true)
                        {
                            DungChung.Bien.MangHaiChieu[num, 10] = r.SoLuongSS; //string.Format(DungChung.Bien.FormatString[0], r.SoLuongTT);
                            DungChung.Bien.MangHaiChieu[num, 11] = r.ThanhTienSS;//string.Format(DungChung.Bien.FormatString[1], r.ThanhTienTT);
                        }
                        DungChung.Bien.MangHaiChieu[num, 12] = r.HongVo;// string.Format(DungChung.Bien.FormatString[1], r.HongVo);
                        DungChung.Bien.MangHaiChieu[num, 13] = "";
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                    #endregion


                    rep.BindingData();
                    rep.CreateDocument();
                    frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Biên bản kiểm kê thuốc", "C:\\BBKiemKeThuoc.xls", true, this.Name);
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                else
                {
                    if (chkInTien.Checked)
                    {
                        BaoCao.repBbKKThuoc_ThanhTien rep = new BaoCao.repBbKKThuoc_ThanhTien(Thang);
                        if (chkTT.Checked == true)
                        { rep.TT.Value = "A"; }
                        else { rep.TT.Value = "X"; }
                        rep.So.Value = _id;
                        if (par.Count > 0)
                        {
                            if (par.Count == 1)
                            {
                                if (DungChung.Bien.MaBV == "14018")
                                {
                                    rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                }
                                else
                                    rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                rep.Khoa.Value = par.First().TenKP;
                            }
                            if (par.Count > 1)
                            {
                                if (DungChung.Bien.MaBV == "14018")
                                {
                                    rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                }
                                else
                                    rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            }
                        }
                        if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                        //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                        int i = 1; int j = 0;
                        string dscb = "", dscd = "";
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
                        if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                            i++;
                        }
                        if ((txtTV9goi.Text + lupTV9.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                            i++;
                        }
                        if ((txtTV10goi.Text + lupTV10.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                            i++;
                        }
                        if ((txtTV11goi.Text + lupTV11.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                            i++;
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
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
                        if ((txtChucDanh8.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh9.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh10.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                        }
                        if ((txtChucDanh11.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            if ((txtChucDanh12.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh13.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh14.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh15.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                        }


                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.TV8.Value = lupTV8.Text;
                        

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            rep.Parameters["TV9"].Value = lupTV12.Text;
                            rep.Parameters["TV10"].Value = lupTV13.Text;
                            rep.Parameters["TV11"].Value = lupTV14.Text;
                            rep.Parameters["TV12"].Value = lupTV12.Text;
                            rep.Parameters["TV13"].Value = lupTV13.Text;
                            rep.Parameters["TV14"].Value = lupTV14.Text;
                            rep.Parameters["TV15"].Value = lupTV15.Text;
                        }

                        rep.DaKKTai.Value = txtKKTai.Text;
                        rep.YKDX.Value = txtYKDX.Text;
                        double TTong = 0;
                        if (q.Count > 0)
                        {
                            // TTong = q.Sum(p => p.ThanhTienTT);
                            TTong = Math.Round(Math.Round(q.Sum(p => p.ThanhTienTT), 2), 0);
                        }
                        if (TTong > 0)
                        {
                            rep.SoTienBC.Value = "Số tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(TTong, " đồng./.");
                        }
                        else { rep.SoTienBC.Value = "Số tiền bằng chữ: "; }
                        rep.DataSource = q;


                        #region xuất Excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 2;
                        DungChung.Bien.MangHaiChieu = new Object[q.Count + 2, _arr.Length];
                        string[] _tieude = { "STT", "Tên thuốc - Hàm lượng", "Đơn vị", "SĐK hoặc GPNK", "Số kiểm soát", "Nước sản xuất", "Hạn dùng", "Giá", "Số lượng sổ sách", "Thành tiền sổ sách", "Số lượng thực tế", "Thành tiền thực tế", "Hỏng, vỡ", "Ghi chú" };
                        for (int k = 0; k < _tieude.Length; k++)
                        {
                            DungChung.Bien.MangHaiChieu[0, k] = _tieude[k].ToUpper();
                            DungChung.Bien.MangHaiChieu[1, k] = (k + 1).ToString();
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in q)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num - 1;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.SoDK;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.SoLo;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.NuocSX;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.HanDung;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;//string.Format(DungChung.Bien.FormatString[1], r.DonGia);
                            DungChung.Bien.MangHaiChieu[num, 8] = r.SoLuongSS;//string.Format(DungChung.Bien.FormatString[0], r.SoLuongSS);
                            DungChung.Bien.MangHaiChieu[num, 9] = r.ThanhTienSS;//string.Format(DungChung.Bien.FormatString[1], r.ThanhTienSS);
                            if (chkTT.Checked == true)
                            {
                                DungChung.Bien.MangHaiChieu[num, 10] = r.SoLuongSS; //string.Format(DungChung.Bien.FormatString[0], r.SoLuongTT);
                                DungChung.Bien.MangHaiChieu[num, 11] = r.ThanhTienSS;//string.Format(DungChung.Bien.FormatString[1], r.ThanhTienTT);
                            }
                            DungChung.Bien.MangHaiChieu[num, 12] = r.HongVo;// string.Format(DungChung.Bien.FormatString[1], r.HongVo);
                            DungChung.Bien.MangHaiChieu[num, 13] = "";
                            num++;
                        }
                        //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo cham cong PTTT", "C:\\BC_ChamCongPTTT.xls", true);
                        #endregion


                        rep.BindingData();
                        rep.CreateDocument();
                        frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Biên bản kiểm kê thuốc", "C:\\BBKiemKeThuoc.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV != "20001")
                        {
                            BaoCao.repBbKKThuoc rep = new BaoCao.repBbKKThuoc(ckInNuocSX.Checked, Thang);
                            rep.TieuDe.Value = "BIÊN BẢN KIỂM KÊ THUỐC";
                            if (chkTT.Checked == true)
                            { rep.TT.Value = "A"; }
                            else { rep.TT.Value = "X"; }
                            rep.So.Value = _id;
                            if (par.Count > 0)
                            {
                                if (par.Count == 1)
                                {
                                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                    {
                                        rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                    }
                                    else
                                        rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                    rep.Khoa.Value = par.First().TenKP;
                                }
                                if (par.Count > 1)
                                {
                                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                    {
                                        rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                    }
                                    else
                                        rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                    if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                    if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                    if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                    if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                    if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                }
                            }
                            if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                            //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                            int i = 1; int j = 0;
                            string dscb = "", dscd = "";
                            //  int test = 0;
                            if ((txtTV1goi.Text + lupTV1.Text).Length >= 1)
                            {

                                dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                                i++;
                                //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV2goi.Text + lupTV2.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                                i++;

                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV3goi.Text + lupTV3.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV4goi.Text + lupTV4.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV5goi.Text + lupTV5.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV6goi.Text + lupTV6.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV7goi.Text + lupTV7.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV8goi.Text + lupTV8.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if (DungChung.Bien.MaBV == "27001") 
                            {
                                if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                            }

                            if ((txtChucDanh1.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh2.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh3.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh4.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh5.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh6.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh7.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh8.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh9.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh10.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh11.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if (DungChung.Bien.MaBV == "27001")
                            {
                                if ((txtChucDanh12.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtChucDanh13.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtChucDanh14.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtChucDanh15.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                            }


                            rep.TV1goi.Value = dscb.ToString();
                            rep.TV2goi.Value = dscd.ToString();
                            rep.TV1.Value = lupTV1.Text;
                            rep.TV2.Value = lupTV2.Text;
                            rep.TV3.Value = lupTV3.Text;
                            rep.TV4.Value = lupTV4.Text;
                            rep.TV5.Value = lupTV5.Text;
                            rep.TV6.Value = lupTV6.Text;
                            rep.TV7.Value = lupTV7.Text;
                            rep.TV8.Value = lupTV8.Text;
                            rep.TV9.Value = lupTV9.Text;
                            rep.TV10.Value = lupTV10.Text;
                            rep.TV11.Value = lupTV11.Text;
                            if (DungChung.Bien.MaBV == "27001")
                            {
                                rep.Parameters["TV12"].Value = lupTV12.Text;
                                rep.Parameters["TV13"].Value = lupTV13.Text;
                                rep.Parameters["TV14"].Value = lupTV14.Text;
                                rep.Parameters["TV15"].Value = lupTV15.Text;
                            }

                            rep.DaKKTai.Value = txtKKTai.Text;
                            rep.YKDX.Value = txtYKDX.Text;


                            if (q.Count > 0)
                            {
                                rep.DataSource = q;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else { MessageBox.Show("Không có dữ liệu"); }
                        }
                        else
                        {
                            if (chkMadv.Checked != true)
                            {
                                BaoCao.repBbKKThuoc rep = new BaoCao.repBbKKThuoc(ckInNuocSX.Checked, Thang);
                                if (chkTT.Checked == true)
                                { rep.TT.Value = "A"; }
                                else { rep.TT.Value = "X"; }
                                rep.So.Value = _id;
                                if (par.Count > 0)
                                {
                                    if (par.Count == 1)
                                    {
                                        if (DungChung.Bien.MaBV == "14018")
                                        {
                                            rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                        }
                                        else
                                            rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                        rep.Khoa.Value = par.First().TenKP;
                                    }
                                    if (par.Count > 1)
                                    {
                                        if (DungChung.Bien.MaBV == "14018")
                                        {
                                            rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                        }
                                        else
                                            rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                        if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                        if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                        if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                        if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                        if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                    }
                                }
                                if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                                //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                                int i = 1; int j = 0;
                                string dscb = "", dscd = "";
                                //  int test = 0;
                                if ((txtTV1goi.Text + lupTV1.Text).Length >= 1)
                                {

                                    dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                                    i++;
                                    //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV2goi.Text + lupTV2.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                                    i++;

                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV3goi.Text + lupTV3.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV4goi.Text + lupTV4.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV5goi.Text + lupTV5.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV6goi.Text + lupTV6.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV7goi.Text + lupTV7.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV8goi.Text + lupTV8.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if (DungChung.Bien.MaBV == "27001")
                                {
                                    if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                                    {
                                        dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                                        i++;
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }

                                    if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                                    {
                                        dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                                        i++;
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }

                                    if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                                    {
                                        dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                                        i++;
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }

                                    if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                                    {
                                        dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                                        i++;
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }
                                }

                                if ((txtChucDanh1.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh2.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh3.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh4.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh5.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh6.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh7.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh8.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh9.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh10.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                                if ((txtChucDanh11.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if (DungChung.Bien.MaBV == "27001")
                                {
                                    if ((txtChucDanh12.Text).Length >= 1)
                                    {
                                        dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }

                                    if ((txtChucDanh13.Text).Length >= 1)
                                    {
                                        dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }

                                    if ((txtChucDanh14.Text).Length >= 1)
                                    {
                                        dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }

                                    if ((txtChucDanh15.Text).Length >= 1)
                                    {
                                        dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                                    }
                                    else
                                    {
                                        dscb += " ";
                                        i++;
                                    }
                                }

                                rep.TV1goi.Value = dscb.ToString();
                                rep.TV2goi.Value = dscd.ToString();
                                rep.TV1.Value = lupTV1.Text;
                                rep.TV2.Value = lupTV2.Text;
                                rep.TV3.Value = lupTV3.Text;
                                rep.TV4.Value = lupTV4.Text;
                                rep.TV5.Value = lupTV5.Text;
                                rep.TV6.Value = lupTV6.Text;
                                rep.TV7.Value = lupTV7.Text;
                                rep.TV8.Value = lupTV8.Text;
                                rep.TV9.Value = lupTV9.Text;
                                rep.TV10.Value = lupTV10.Text;
                                rep.TV11.Value = lupTV11.Text;

                                if (DungChung.Bien.MaBV == "27001")
                                {
                                    rep.Parameters["TV9"].Value = lupTV12.Text;
                                    rep.Parameters["TV10"].Value = lupTV13.Text;
                                    rep.Parameters["TV11"].Value = lupTV14.Text;
                                    rep.Parameters["TV12"].Value = lupTV12.Text;
                                    rep.Parameters["TV13"].Value = lupTV13.Text;
                                    rep.Parameters["TV14"].Value = lupTV14.Text;
                                    rep.Parameters["TV15"].Value = lupTV15.Text;
                                }

                                rep.DaKKTai.Value = txtKKTai.Text;
                                rep.YKDX.Value = txtYKDX.Text;


                                if (q.Count > 0)
                                {
                                    rep.DataSource = q;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else { MessageBox.Show("Không có dữ liệu"); }
                            }
                            else
                            {
                                BaoCao.repBbKKThuoc_20001 rep = new BaoCao.repBbKKThuoc_20001(ckInNuocSX.Checked, Thang);
                                if (chkTT.Checked == true)
                                { rep.TT.Value = "A"; }
                                else { rep.TT.Value = "X"; }
                                rep.So.Value = _id;
                                if (par.Count > 0)
                                {
                                    if (par.Count == 1)
                                    {
                                        if (DungChung.Bien.MaBV == "14018")
                                        {
                                            rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                        }
                                        else
                                            rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                        rep.Khoa.Value = par.First().TenKP;
                                    }
                                    if (par.Count > 1)
                                    {
                                        if (DungChung.Bien.MaBV == "14018")
                                        {
                                            rep.ThangNam.Value = "Vào lúc ........... giờ " + DungChung.Ham.NgaySangChu(DateTime.Now).ToLower();
                                        }
                                        else
                                            rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                        if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                        if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                        if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                        if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                        if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                    }
                                }
                                if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                                //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                                int i = 1; int j = 0;
                                string dscb = "", dscd = "";
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
                                if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                                {
                                    dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
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
                                } if ((txtChucDanh8.Text).Length > 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                                }
                                rep.TV1goi.Value = dscb.ToString();
                                rep.TV2goi.Value = dscd.ToString();
                                rep.TV1.Value = lupTV1.Text;
                                rep.TV2.Value = lupTV2.Text;
                                rep.TV3.Value = lupTV3.Text;
                                rep.TV4.Value = lupTV4.Text;
                                rep.TV5.Value = lupTV5.Text;
                                rep.TV6.Value = lupTV6.Text;
                                rep.TV7.Value = lupTV7.Text;
                                rep.TV8.Value = lupTV8.Text;

                                rep.DaKKTai.Value = txtKKTai.Text;
                                rep.YKDX.Value = txtYKDX.Text;


                                if (q.Count > 0)
                                {
                                    rep.DataSource = q;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else { MessageBox.Show("Không có dữ liệu"); }
                            }
                        }
                    }
                }
            }
            #endregion

            #region Hóa chất
            if (cmbKK.EditValue == "Hóa chất")
            {
                if (_MauIn == 2)
                {
                    BaoCao.repBbKKThuoc_C32 rep = new BaoCao.repBbKKThuoc_C32();
                    if (chkTT.Checked == true)
                    { rep.TT.Value = "A"; }
                    else { rep.TT.Value = "X"; }
                    rep.So.Value = _id;
                    if (par.Count > 0)
                    {
                        if (par.Count == 1)
                        {
                            rep.ThangNam.Value = "- Thời điểm kiểm kê " + "....giờ...." + " ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                            rep.Khoa.Value = par.First().TenKP;
                        }
                        if (par.Count > 1)
                        {
                            rep.ThangNam.Value = "- Thời điểm kiểm kê " + "....giờ...." + " ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                            if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                            if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                            if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                            if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                        }
                    }
                    if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                    //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                    int i = 1; int j = 0;
                    string dscb = "", dscd = "";
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
                    if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                        i++;
                    }

                    //if (DungChung.Bien.MaBV == "27001")
                    //{
                    //    if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }
                    //    if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                    //    {
                    //        dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }
                    //}

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
                    } if ((txtChucDanh8.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                    }

                    //if (DungChung.Bien.MaBV == "27001")
                    //{

                    //    if ((txtChucDanh9.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtChucDanh10.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtChucDanh11.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }
                    //    if ((txtChucDanh12.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtChucDanh13.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtChucDanh14.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }

                    //    if ((txtChucDanh15.Text).Length >= 1)
                    //    {
                    //        dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                    //    }
                    //    else
                    //    {
                    //        dscb += " ";
                    //        i++;
                    //    }
                    //}

                    rep.TV1goi.Value = dscb.ToString();
                    rep.TV2goi.Value = dscd.ToString();
                    rep.TV1.Value = lupTV1.Text;
                    rep.TV2.Value = lupTV2.Text;
                    rep.TV3.Value = lupTV3.Text;
                    rep.TV4.Value = lupTV4.Text;
                    rep.TV5.Value = lupTV5.Text;
                    rep.TV6.Value = lupTV6.Text;
                    rep.TV7.Value = lupTV7.Text;
                    rep.TV8.Value = lupTV8.Text;

                    //if (DungChung.Bien.MaBV == "27001")
                    //{
                    //    rep.Parameters["TV9"].Value = lupTV12.Text;
                    //    rep.Parameters["TV10"].Value = lupTV13.Text;
                    //    rep.Parameters["TV11"].Value = lupTV14.Text;
                    //    rep.Parameters["TV12"].Value = lupTV12.Text;
                    //    rep.Parameters["TV13"].Value = lupTV13.Text;
                    //    rep.Parameters["TV14"].Value = lupTV14.Text;
                    //    rep.Parameters["TV15"].Value = lupTV15.Text;
                    //}

                    rep.DaKKTai.Value = txtKKTai.Text;
                    rep.YKDX.Value = txtYKDX.Text;


                    if (q.Count > 0)
                    {
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else { MessageBox.Show("Không có dữ liệu"); }
                }
                else
                {
                    if (chkInTien.Checked)
                    {
                        BaoCao.repBbKKHoaChat_ThanhTien rep = new BaoCao.repBbKKHoaChat_ThanhTien(Thang);
                        if (chkTT.Checked == true)
                        { rep.TT.Value = "A"; }
                        else { rep.TT.Value = "X"; }
                        rep.So.Value = _id;
                        if (par.Count > 0)
                        {
                            if (par.Count == 1)
                            {
                                rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                rep.Khoa.Value = par.First().TenKP;
                            }
                            if (par.Count > 1)
                            {
                                rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            }
                        }
                        if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                        //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                        int i = 1; int j = 0;
                        string dscb = "", dscd = "";
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
                        if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                            i++;
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
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
                        if ((txtChucDanh8.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {

                            if ((txtChucDanh9.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh10.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh11.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh12.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh13.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh14.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh15.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                        }

                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.TV8.Value = lupTV8.Text;

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            rep.Parameters["TV9"].Value = lupTV12.Text;
                            rep.Parameters["TV10"].Value = lupTV13.Text;
                            rep.Parameters["TV11"].Value = lupTV14.Text;
                            rep.Parameters["TV12"].Value = lupTV12.Text;
                            rep.Parameters["TV13"].Value = lupTV13.Text;
                            rep.Parameters["TV14"].Value = lupTV14.Text;
                            rep.Parameters["TV15"].Value = lupTV15.Text;
                        }

                        rep.DaKKTai.Value = txtKKTai.Text;
                        rep.YKDX.Value = txtYKDX.Text;
                        double TTong = 0;
                        if (q.Count > 0)
                        {
                            //TTong = q.Sum(p => p.ThanhTienTT);
                            TTong = Math.Round(Math.Round(q.Sum(p => p.ThanhTienTT), 2), 0);
                        }
                        if (TTong > 0)
                        {
                            rep.SoTienBC.Value = "Số tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(TTong, " đồng./.");
                        }
                        else { rep.SoTienBC.Value = "Số tiền bằng chữ: "; }
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (DungChung.Bien.MaBV == "30009")
                    {
                        BaoCao.repBbKKThuoc rep = new BaoCao.repBbKKThuoc(ckInNuocSX.Checked, Thang);
                        rep.TieuDe.Value = "BIÊN BẢN KIỂM KÊ HÓA CHẤT";
                            if (chkTT.Checked == true)
                            { rep.TT.Value = "A"; }
                            else { rep.TT.Value = "X"; }
                            rep.So.Value = _id;
                            if (par.Count > 0)
                            {
                                if (par.Count == 1)
                                {
                                    rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                    rep.Khoa.Value = par.First().TenKP;
                                }
                                if (par.Count > 1)
                                {
                                    
                                    rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                    if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                    if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                    if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                    if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                    if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                }
                            }
                            if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                            //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                            int i = 1; int j = 0;
                            string dscb = "", dscd = "";
                            //  int test = 0;
                            if ((txtTV1goi.Text + lupTV1.Text).Length >= 1)
                            {

                                dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                                i++;
                                //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV2goi.Text + lupTV2.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                                i++;

                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV3goi.Text + lupTV3.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV4goi.Text + lupTV4.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV5goi.Text + lupTV5.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV6goi.Text + lupTV6.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV7goi.Text + lupTV7.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV8goi.Text + lupTV8.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh1.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh2.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh3.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh4.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh5.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh6.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh7.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh8.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh9.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh10.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh11.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            rep.TV1goi.Value = dscb.ToString();
                            rep.TV2goi.Value = dscd.ToString();
                            rep.TV1.Value = lupTV1.Text;
                            rep.TV2.Value = lupTV2.Text;
                            rep.TV3.Value = lupTV3.Text;
                            rep.TV4.Value = lupTV4.Text;
                            rep.TV5.Value = lupTV5.Text;
                            rep.TV6.Value = lupTV6.Text;
                            rep.TV7.Value = lupTV7.Text;
                            rep.TV8.Value = lupTV8.Text;
                            rep.TV9.Value = lupTV9.Text;
                            rep.TV10.Value = lupTV10.Text;
                            rep.TV11.Value = lupTV11.Text;
                            rep.DaKKTai.Value = txtKKTai.Text;
                            rep.YKDX.Value = txtYKDX.Text;


                            if (q.Count > 0)
                            {
                                rep.DataSource = q;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else { MessageBox.Show("Không có dữ liệu"); }
                    }
                    else
                    {
                        BaoCao.repBbKKHoaChat rep = new BaoCao.repBbKKHoaChat(Thang);
                        if (chkTT.Checked == true)
                        { rep.TT.Value = "A"; }
                        else { rep.TT.Value = "X"; }
                        if (par.Count > 0)
                        {
                            if (par.Count == 1)
                            {
                                rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                rep.Khoa.Value = par.First().TenKP;
                            }
                            if (par.Count > 0)
                            {
                                if (par.Count == 1)
                                {
                                    rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                    rep.Khoa.Value = par.First().TenKP;
                                }
                                if (par.Count > 1)
                                {
                                    rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                    if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                    if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                    if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                    if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                    if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                }
                            }
                            rep.So.Value = _id;
                            // rep.Khoa.Value = par.First().TenKP;
                            if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                            //  if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }

                            int i = 1; int j = 0;
                            string dscb = "", dscd = "";
                            //  int test = 0;
                            if ((txtTV1goi.Text + lupTV1.Text).Length >= 1)
                            {

                                dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                                i++;
                                //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV2goi.Text + lupTV2.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                                i++;

                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV3goi.Text + lupTV3.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV4goi.Text + lupTV4.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV5goi.Text + lupTV5.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV6goi.Text + lupTV6.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV7goi.Text + lupTV7.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV8goi.Text + lupTV8.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if (DungChung.Bien.MaBV == "27001")
                            {
                                if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                                {
                                    dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                                    i++;
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                            }

                            if ((txtChucDanh1.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh2.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh3.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh4.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh5.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh6.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh7.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh8.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh9.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh10.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh11.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if (DungChung.Bien.MaBV == "27001")
                            {
                                if ((txtChucDanh12.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtChucDanh13.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtChucDanh14.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }

                                if ((txtChucDanh15.Text).Length >= 1)
                                {
                                    dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                                }
                                else
                                {
                                    dscb += " ";
                                    i++;
                                }
                            }

                            rep.TV1goi.Value = dscb.ToString();
                            rep.TV2goi.Value = dscd.ToString();
                            rep.TV1.Value = lupTV1.Text;
                            rep.TV2.Value = lupTV2.Text;
                            rep.TV3.Value = lupTV3.Text;
                            rep.TV4.Value = lupTV4.Text;
                            rep.TV5.Value = lupTV5.Text;
                            rep.TV6.Value = lupTV6.Text;
                            rep.TV7.Value = lupTV7.Text;
                            rep.TV8.Value = lupTV8.Text;
                            rep.TV9.Value = lupTV9.Text;
                            rep.TV10.Value = lupTV10.Text;
                            rep.TV11.Value = lupTV11.Text;

                            if (DungChung.Bien.MaBV == "27001")
                            {
                                rep.Parameters["TV12"].Value = lupTV12.Text;
                                rep.Parameters["TV13"].Value = lupTV13.Text;
                                rep.Parameters["TV14"].Value = lupTV14.Text;
                                rep.Parameters["TV15"].Value = lupTV15.Text;
                            }

                            rep.DaKKTai.Value = txtKKTai.Text;
                            rep.YKDX.Value = txtYKDX.Text;
                            if (q.Count > 0)
                            {
                                rep.DataSource = q;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else
                            MessageBox.Show("Không có dữ liệu!");
                    }
                }
            }
            #endregion

            #region VTYT tiêu hao
            if (cmbKK.EditValue == "VTYT tiêu hao" || cmbKK.EditValue == "Hàng Hóa")
            {
                if (_MauIn == 2)
                {
                    BaoCao.repBbKKThuoc_C32 rep = new BaoCao.repBbKKThuoc_C32();
                    if (chkTT.Checked == true)
                    { rep.TT.Value = "A"; }
                    else { rep.TT.Value = "X"; }
                    rep.So.Value = _id;
                    if (par.Count > 0)
                    {
                        if (par.Count == 1)
                        {
                            rep.ThangNam.Value = "- Thời điểm kiểm kê " + "....giờ...." + " ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                            rep.Khoa.Value = par.First().TenKP;
                        }
                        if (par.Count > 1)
                        {
                            rep.ThangNam.Value = "- Thời điểm kiểm kê " + "....giờ...." + " ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                            if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                            if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                            if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                            if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                        }
                    }
                    if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                    //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                    int i = 1; int j = 0;
                    string dscb = "", dscd = "";
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
                    if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                    {
                        dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
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
                    } if ((txtChucDanh8.Text).Length > 1)
                    {
                        dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                    }
                    rep.TV1goi.Value = dscb.ToString();
                    rep.TV2goi.Value = dscd.ToString();
                    rep.TV1.Value = lupTV1.Text;
                    rep.TV2.Value = lupTV2.Text;
                    rep.TV3.Value = lupTV3.Text;
                    rep.TV4.Value = lupTV4.Text;
                    rep.TV5.Value = lupTV5.Text;
                    rep.TV6.Value = lupTV6.Text;
                    rep.TV7.Value = lupTV7.Text;
                    rep.TV8.Value = lupTV8.Text;
                    rep.DaKKTai.Value = txtKKTai.Text;
                    rep.YKDX.Value = txtYKDX.Text;


                    if (q.Count > 0)
                    {
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else { MessageBox.Show("Không có dữ liệu"); }
                }
                else
                {
                    if (chkInTien.Checked)
                    {
                        BaoCao.repBbKKVTYTTieuHao_ThanhTien rep = new BaoCao.repBbKKVTYTTieuHao_ThanhTien(Thang);
                        if (cmbKK.EditValue == "Hàng Hóa")
                        {
                            rep.xrLabel5.Text = "BIÊN BẢN KIỂM KÊ VẬT TƯ Y TẾ HÀNG HÓA";
                        }
                        if (chkTT.Checked == true)
                        { rep.TT.Value = "A"; }
                        else { rep.TT.Value = "X"; }
                        rep.So.Value = _id;
                        if (par.Count > 0)
                        {
                            if (par.Count == 1)
                            {
                                rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                rep.Khoa.Value = par.First().TenKP;
                            }
                            if (par.Count > 1)
                            {
                                rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            }
                        }
                        if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                        //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                        int i = 1; int j = 0;
                        string dscb = "", dscd = "";
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
                        if ((txtTV8goi.Text + lupTV8.Text).Length > 1)
                        {
                            dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                            i++;
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {

                            if ((txtChucDanh9.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh10.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh11.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh12.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh13.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh14.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh15.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
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
                        if ((txtChucDanh8.Text).Length > 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {

                            if ((txtChucDanh9.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh10.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh11.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                            if ((txtChucDanh12.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh13.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh14.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh15.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                        }

                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.TV8.Value = lupTV8.Text;

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            rep.Parameters["TV9"].Value = lupTV12.Text;
                            rep.Parameters["TV10"].Value = lupTV13.Text;
                            rep.Parameters["TV11"].Value = lupTV14.Text;
                            rep.Parameters["TV12"].Value = lupTV12.Text;
                            rep.Parameters["TV13"].Value = lupTV13.Text;
                            rep.Parameters["TV14"].Value = lupTV14.Text;
                            rep.Parameters["TV15"].Value = lupTV15.Text;
                        }

                        rep.DaKKTai.Value = txtKKTai.Text;
                        rep.YKDX.Value = txtYKDX.Text;
                        double TTong = 0;
                        if (q.Count > 0)
                        {
                            //TTong = q.Sum(p => p.ThanhTienTT);
                            TTong = Math.Round(Math.Round(q.Sum(p => p.ThanhTienTT), 2), 0);
                        }
                        if (TTong > 0)
                        {
                            rep.SoTienBC.Value = "Số tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(TTong, " đồng./.");
                        }
                        else { rep.SoTienBC.Value = "Số tiền bằng chữ: "; }
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else if (DungChung.Bien.MaBV == "30009")
                    {
                        BaoCao.repBbKKThuoc rep = new BaoCao.repBbKKThuoc(ckInNuocSX.Checked, Thang);
                        rep.TieuDe.Value = "BIÊN BẢN KIỂM KÊ VẬT TƯ Y TẾ TIÊU HAO";
                        if (chkTT.Checked == true)
                        { rep.TT.Value = "A"; }
                        else { rep.TT.Value = "X"; }
                        rep.So.Value = _id;
                        if (par.Count > 0)
                        {
                            if (par.Count == 1)
                            {
                                rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                rep.Khoa.Value = par.First().TenKP;
                            }
                            if (par.Count > 1)
                            {
                                 rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                            }
                        }
                        if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                        //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }
                        int i = 1; int j = 0;
                        string dscb = "", dscd = "";
                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length >= 1)
                        {

                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV8goi.Text + lupTV8.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh1.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh2.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh3.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh4.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh5.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh6.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh7.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh8.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh9.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh10.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh11.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.TV8.Value = lupTV8.Text;
                        rep.TV9.Value = lupTV9.Text;
                        rep.TV10.Value = lupTV10.Text;
                        rep.TV11.Value = lupTV11.Text;
                        rep.DaKKTai.Value = txtKKTai.Text;
                        rep.YKDX.Value = txtYKDX.Text;


                        if (q.Count > 0)
                        {
                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else { MessageBox.Show("Không có dữ liệu"); }
                    }
                    else
                    {
                        BaoCao.repBbKKVTYTTieuHao rep = new BaoCao.repBbKKVTYTTieuHao(Thang);
                        if(cmbKK.EditValue == "Hàng Hóa")
                        {
                            rep.xrLabel5.Text = "BIÊN BẢN KIỂM KÊ VẬT TƯ Y TẾ HÀNG HÓA";
                        }    
                        if (chkTT.Checked == true)
                        { rep.TT.Value = "A"; }
                        else { rep.TT.Value = "X"; }
                        if (par.Count > 0)
                        {
                            if (par.Count == 1)
                            {
                                rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                rep.Khoa.Value = par.First().TenKP;
                            }
                            if (par.Count > 0)
                            {
                                if (par.Count == 1)
                                {
                                    rep.ThangNam.Value = "Tháng " + denngay.ToString().Substring(3, 2) + " năm " + denngay.ToString().Substring(6, 4);
                                    rep.Khoa.Value = par.First().TenKP;
                                }
                                if (par.Count > 1)
                                {
                                    rep.ThangNam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " Đến ngày: " + denngay.ToString().Substring(0, 10);
                                    if (par.Count == 2) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP; }
                                    if (par.Count == 3) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP; }
                                    if (par.Count == 4) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP; }
                                    if (par.Count == 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }
                                    if (par.Count > 5) { rep.Khoa.Value = par.First().TenKP + ", " + par.Skip(1).First().TenKP + ", " + par.Skip(2).First().TenKP + ", " + par.Skip(3).First().TenKP + ", " + par.Skip(4).First().TenKP; }

                                }


                            }

                        }
                        rep.So.Value = _id;
                        //    rep.Khoa.Value = par.First().TenKP;
                        if (ckNT.Checked == true) { rep.NT.Value = 1; } else { rep.NT.Value = 0; }
                        //    if (ckCT.Checked == true) { rep.CT.Value = 1; } else { rep.CT.Value = 0; }

                        int i = 1; int j = 0;
                        string dscb = "", dscd = "";
                        //  int test = 0;
                        if ((txtTV1goi.Text + lupTV1.Text).Length >= 1)
                        {

                            dscb += i + ". " + txtTV1goi.Text + " " + lupTV1.Text + "\n";
                            i++;
                            //      MessageBox.Show( (i + ". " + txtTV1goi.Text + " " + lupTV1.Text + kytutrang(lupTV1.Text, 30)).Length.ToString());
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV2goi.Text + lupTV2.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV2goi.Text + " " + lupTV2.Text + "\n";
                            i++;

                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV3goi.Text + lupTV3.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV3goi.Text + " " + lupTV3.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV4goi.Text + lupTV4.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV4goi.Text + " " + lupTV4.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV5goi.Text + lupTV5.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV5goi.Text + " " + lupTV5.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV6goi.Text + lupTV6.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV6goi.Text + " " + lupTV6.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV7goi.Text + lupTV7.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV7goi.Text + " " + lupTV7.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV8goi.Text + lupTV8.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV8goi.Text + " " + lupTV8.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV9goi.Text + lupTV9.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV9goi.Text + " " + lupTV9.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV10goi.Text + lupTV10.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV10goi.Text + " " + lupTV10.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtTV11goi.Text + lupTV11.Text).Length >= 1)
                        {
                            dscb += i + ". " + txtTV11goi.Text + " " + lupTV11.Text + "\n";
                            i++;
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            if ((txtTV12goi.Text + lupTV12.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV12goi.Text + " " + lupTV12.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV13goi.Text + lupTV13.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV13goi.Text + " " + lupTV13.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV14goi.Text + lupTV14.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV14goi.Text + " " + lupTV14.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtTV15goi.Text + lupTV15.Text).Length >= 1)
                            {
                                dscb += i + ". " + txtTV15goi.Text + " " + lupTV15.Text + "\n";
                                i++;
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                        }

                        if ((txtChucDanh1.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh1.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh2.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh2.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh3.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh3.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh4.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh4.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh5.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh5.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh6.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh6.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh7.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh7.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh8.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh8.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh9.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh9.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh10.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh10.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }
                        if ((txtChucDanh11.Text).Length >= 1)
                        {
                            dscd += "Chức danh: " + txtChucDanh11.Text.Trim() + "\n";
                        }
                        else
                        {
                            dscb += " ";
                            i++;
                        }

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            if ((txtChucDanh12.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh12.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh13.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh13.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh14.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh14.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }

                            if ((txtChucDanh15.Text).Length >= 1)
                            {
                                dscd += "Chức danh: " + txtChucDanh15.Text.Trim() + "\n";
                            }
                            else
                            {
                                dscb += " ";
                                i++;
                            }
                        }

                        rep.TV1goi.Value = dscb.ToString();
                        rep.TV2goi.Value = dscd.ToString();
                        rep.TV1.Value = lupTV1.Text;
                        rep.TV2.Value = lupTV2.Text;
                        rep.TV3.Value = lupTV3.Text;
                        rep.TV4.Value = lupTV4.Text;
                        rep.TV5.Value = lupTV5.Text;
                        rep.TV6.Value = lupTV6.Text;
                        rep.TV7.Value = lupTV7.Text;
                        rep.TV8.Value = lupTV8.Text;
                        rep.TV9.Value = lupTV9.Text;
                        rep.TV10.Value = lupTV10.Text;
                        rep.TV11.Value = lupTV11.Text;

                        if (DungChung.Bien.MaBV == "27001")
                        {
                            rep.Parameters["TV12"].Value = lupTV12.Text;
                            rep.Parameters["TV13"].Value = lupTV13.Text;
                            rep.Parameters["TV14"].Value = lupTV14.Text;
                            rep.Parameters["TV15"].Value = lupTV15.Text;
                        }

                        rep.DaKKTai.Value = txtKKTai.Text;
                        rep.YKDX.Value = txtYKDX.Text;
                        if (q.Count > 0)
                        {
                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                }
            }
            #endregion

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupTV1_EditValueChanged(object sender, EventArgs e)
        {

        }



        private void cmbKK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lupTV1_EditValueChanged_1(object sender, EventArgs e)
        {
            if (lupTV1.EditValue == "")
            {
                txtChucDanh1.EditValue = "";
                txtTV1goi.EditValue = "";
            }
        }
        private void lupTV2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupTV2_EditValueChanged_1(object sender, EventArgs e)
        {
            if (lupTV2.EditValue == "")
            {
                txtChucDanh2.EditValue = "";
                txtTV2goi.EditValue = "";
            }
        }

        private void lupTV3_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV3.EditValue == "")
            {
                txtChucDanh3.EditValue = "";
                txtTV3goi.EditValue = "";
            }
        }

        private void lupTV4_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV4.EditValue == "")
            {
                txtChucDanh4.EditValue = "";
                txtTV4goi.EditValue = "";
            }
        }

        private void lupTV5_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV5.EditValue == "")
            {
                txtChucDanh5.EditValue = "";
                txtTV5goi.EditValue = "";
            }
        }

        private void lupTV6_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV6.EditValue == "")
            {
                txtChucDanh6.EditValue = "";
                txtTV6goi.EditValue = "";
            }
        }

        private void lupTV7_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV7.EditValue == "")
            {
                txtChucDanh7.EditValue = "";
                txtTV7goi.EditValue = "";
            }
        }
        private void lupTV8_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTV8.EditValue == "")
            {
                txtChucDanh8.EditValue = "";
                txtTV8goi.EditValue = "";
            }
        }
        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            _ID.Clear();
            if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
            {
                if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 4 || p.PLoai == 3)
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            ID themmoi1 = new ID();
                            themmoi1.SoCT1 = "";
                            themmoi1.IDNhap1 = 0;
                            themmoi1.NgayNhap1 = "";
                            themmoi1.LiDo1 = "Chọn tất cả";
                            themmoi1.chon = true;
                            _ID.Add(themmoi1);
                            foreach (var a in qn)
                            {
                                ID themmoi = new ID();
                                themmoi.IDNhap1 = a.IDNhap;
                                themmoi.SoCT1 = a.SoCT;
                                themmoi.NgayNhap1 = a.NgayNhap.ToString();
                                themmoi.LiDo1 = a.GhiChu;
                                themmoi.chon = true;
                                _ID.Add(themmoi);
                            }
                        }
                }
            }
            grcNhapD.DataSource = _ID.ToList();

        }

        private void lupDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            _ID.Clear();

            if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
            {
                if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 4 || p.PLoai == 3)
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            ID themmoi1 = new ID();
                            themmoi1.SoCT1 = "";
                            themmoi1.IDNhap1 = 0;
                            themmoi1.NgayNhap1 = "";
                            themmoi1.LiDo1 = "Chọn tất cả";
                            themmoi1.chon = true;
                            _ID.Add(themmoi1);
                            foreach (var a in qn)
                            {
                                ID themmoi = new ID();
                                themmoi.IDNhap1 = a.IDNhap;
                                themmoi.SoCT1 = a.SoCT;
                                themmoi.NgayNhap1 = a.NgayNhap.ToString();
                                themmoi.LiDo1 = a.GhiChu;
                                themmoi.chon = true;
                                _ID.Add(themmoi);
                            }
                        }
                }
            }
            grcNhapD.DataSource = _ID.ToList();

        }

        private void grvNhapD_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvNhapD.GetFocusedRowCellValue("LiDo1") != null)
                {
                    string Ten = grvNhapD.GetFocusedRowCellValue("LiDo1").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_ID.First().chon == true)
                        {
                            foreach (var a in _ID)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _ID)
                            {
                                a.chon = true;
                            }
                        }
                        grcNhapD.DataSource = "";
                        grcNhapD.DataSource = _ID.ToList();
                    }
                }
            }
        }



        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            _ID.Clear();
            int _makp = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
          
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 4 || p.PLoai == 3)
                              join KP in data.KPhongs.Where(p => (_makp == 0 || _makp == null) ? true : p.MaKP == _makp) on nd.MaKP equals KP.MaKP
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            ID themmoi1 = new ID();
                            themmoi1.SoCT1 = "";
                            themmoi1.IDNhap1 = 0;
                            themmoi1.NgayNhap1 = "";
                            themmoi1.LiDo1 = "Chọn tất cả";
                            themmoi1.chon = true;
                            _ID.Add(themmoi1);
                            foreach (var a in qn)
                            {
                                ID themmoi = new ID();
                                themmoi.IDNhap1 = a.IDNhap;
                                themmoi.SoCT1 = a.SoCT;
                                themmoi.NgayNhap1 = a.NgayNhap.ToString();
                                themmoi.LiDo1 = a.GhiChu;
                                themmoi.chon = true;
                                _ID.Add(themmoi);
                            }
                        } 
            grcNhapD.DataSource = _ID.ToList();

        }



        private void chkMadv_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void rgChonMau_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}