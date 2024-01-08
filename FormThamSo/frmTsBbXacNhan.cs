using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frmTsBbXacNhan : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBbXacNhan()
        {
            InitializeComponent();
        }
        int _id = 0;
        public frmTsBbXacNhan(int id)
        {
            InitializeComponent();
            _id = id;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
 
       private void frmTsBbXacNhan_Load(object sender, EventArgs e)
        {
            var qtencb = (from cb in data.CanBoes
                          select new { cb.MaCB, cb.TenCB }).ToList();
            if (qtencb.Count > 0)
            {
                lupTV1.Properties.DataSource = qtencb.ToList();
                lupTV2.Properties.DataSource = qtencb.ToList();
                lupTV3.Properties.DataSource = qtencb.ToList();
                lupTV4.Properties.DataSource = qtencb.ToList();
                lupTV5.Properties.DataSource = qtencb.ToList();
            }

            if (!File.Exists("C:\\TextBBXacNhan.txt"))
            {
                FileStream fs;
                fs = new FileStream("C:\\TextBBXacNhan.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("C:\\TextBBXacNhan.txt");
            if (lines[lines.Length - 1] == "1")
            {
                lupTV1.Text = lines[lines.Length - 13];
                txtChucDanh1.Text = lines[lines.Length - 12];
                lupTV2.Text = lines[lines.Length - 11];
                txtChucDanh2.Text = lines[lines.Length - 10];
                lupTV3.Text = lines[lines.Length - 9];
                txtChucDanh3.Text = lines[lines.Length - 8];
                lupTV4.Text = lines[lines.Length - 7];
                txtChucDanh4.Text = lines[lines.Length - 6];
                lupTV5.Text = lines[lines.Length - 5];
                txtChucDanh5.Text = lines[lines.Length - 4];
                txtHoi.Text = lines[lines.Length - 3];
                txtDTHT.Text = lines[lines.Length - 2];
            }

        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("C:\\TextBBXacNhan.txt", FileMode.Append);

            StreamWriter writeFile = new StreamWriter(fs, Encoding.UTF8);//dùng streamwriter để ghi file

            writeFile.WriteLine(lupTV1.Text);
            writeFile.WriteLine(txtChucDanh1.Text);
            writeFile.WriteLine(lupTV2.Text);
            writeFile.WriteLine(txtChucDanh2.Text);
            writeFile.WriteLine(lupTV3.Text);
            writeFile.WriteLine(txtChucDanh3.Text);
            writeFile.WriteLine(lupTV4.Text);
            writeFile.WriteLine(txtChucDanh4.Text);
            writeFile.WriteLine(lupTV5.Text);
            writeFile.WriteLine(txtChucDanh5.Text);
            writeFile.WriteLine(txtHoi.Text);
            writeFile.WriteLine(txtDTHT.Text);
            writeFile.WriteLine("1");//ghi từng dòng vào file TextBBKiemKeThuoc.txt
            writeFile.Flush();

            writeFile.Close();

            frmIn frm = new frmIn();
            BaoCao.repBbXacNhan rep = new BaoCao.repBbXacNhan();


            
            //int _thang = 0;
            //int _nam = 0;
            if (!string.IsNullOrEmpty(txtIDNX.Text))
                _id = int.Parse(txtIDNX.Text);
            var par = (from nhapd in data.NhapDs
                       join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                       where (nhapd.IDNhap == _id)
                       select new { kp.TenKP, nhapd.NgayNhap }).ToList();
            if (par.Count > 0)
            {
                rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
            }
            rep.So.Value = _id;
            if (par.First().TenKP!=null)
            rep.Khoa.Value = par.First().TenKP;
            rep.ThanhVienKK1.Value = lupTV1.Text;
            rep.ThanhVienKK2.Value = lupTV2.Text;
            rep.ThanhVienKK3.Value = lupTV3.Text;
            rep.ThanhVienKK4.Value = lupTV4.Text;
            rep.ThanhVienKK5.Value = lupTV5.Text;
            rep.ChucDanhKK1.Value = txtChucDanh1.Text;
            rep.ChucDanhKK2.Value = txtChucDanh2.Text;
            rep.ChucDanhKK3.Value = txtChucDanh3.Text;
            rep.ChucDanhKK4.Value = txtChucDanh4.Text;
            rep.ChucDanhKK5.Value = txtChucDanh5.Text;
            rep.Hoi.Value = txtHoi.Text;
            rep.DTHT.Value = txtDTHT.Text;

            var q = (from nhapd in data.NhapDs
                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                     where (nhapd.IDNhap == _id)
                     join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                     join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                     //join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom 
                     group new { dv, tieunhomdv, nhapd, nhapdct } by new { tieunhomdv.TenTN, dv.TenDV, dv.NuocSX, nhapd.IDNhap, nhapd.PLoai, nhapdct.SoLo, nhapdct.DonVi, nhapdct.DonGia, nhapdct.HanDung, nhapdct.SoLuongKK, nhapdct.SoLuongX } into kq
                     select new
                     {
                         TenTieuNhom = kq.Key.TenTN,
                         TenDV = kq.Key.TenDV,
                         DonVi = kq.Key.DonVi,
                         SoKiemSoat = kq.Key.SoLo,
                         HanDung=kq.Key.HanDung,
                         NuocSX = kq.Key.NuocSX,
                         DonGia = kq.Key.DonGia,
                         SoLuongX = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX),
                      }).ToList();
            if (q.Count > 0)
            {
                rep.DataSource = q;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {

                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}