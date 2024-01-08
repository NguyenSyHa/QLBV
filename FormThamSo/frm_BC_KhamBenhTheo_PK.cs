using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_KhamBenhTheo_PK : Form
    {
        public frm_BC_KhamBenhTheo_PK()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_BC_KhamBenhTheo_PK_Load(object sender, EventArgs e)
        {
            int thisYear = DateTime.Now.Year;
            for (int i = thisYear; i > 2000; i--)
            {
                cboNam.Properties.Items.Add(i);
            }
            cboNam.SelectedIndex = 0;
            lupKhoa.EditValue = DungChung.Bien.MaKP;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai == ("phòng khám")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                lupKhoa.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
        }
        public class bc591
        {
            public string Ngay { get; set; }
            public int BaoHiem { get; set; }
            public int NhanDan { get; set; }
            public int SauTuoi { get; set; }
            public int MuoiLamTuoi { get; set; }
            public int SauMuoiTuoi { get; set; }
            public int HoNgheo { get; set; }
            public int KhamSucKhoe { get; set; }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboThang.Text) || string.IsNullOrEmpty(cboNam.Text))
            {
                MessageBox.Show("Chưa đủ thông tin");
                return;
            }
            if (lupKhoa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                return;
            }
            int DaysOfThisMonth = DateTime.DaysInMonth(Convert.ToInt32(cboNam.Text), Convert.ToInt32(cboThang.Text));
            DateTime FirstDayThisMonth = Convert.ToDateTime("01" + "/" + cboThang.Text + "/" + cboNam.Text + " 00:00:00");
            DateTime LastDayThisMonth = Convert.ToDateTime(DaysOfThisMonth + "/" + cboThang.Text + "/" + cboNam.Text + " 23:59:59");
            DateTime FirstDayPreviousMonth = FirstDayThisMonth.AddMonths(-1);
            List<bc591> _list = new List<bc591>();
            for (int i = FirstDayThisMonth.Day; i <= LastDayThisMonth.Day; i++)
            {
                DateTime ngaykham = Convert.ToDateTime(i + "/" + cboThang.Text + "/" + cboNam.Text + " 00:00:00");
                bc591 bc = getbc(ngaykham);
                _list.Add(bc);
            }
            int _makp = 0;
            _makp = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
            var BNdtNgoaiTru = (from bnkb in _Data.BNKBs
                                join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                                where kp.PLoai == "Phòng khám" && bn.NoiTru == 0 && bn.DTNT == true && bn.Status == 3 && bnkb.NgayKham >= FirstDayThisMonth && bnkb.NgayKham <= LastDayThisMonth
                                where bnkb.MaKP == _makp 
                                select new
                                {
                                    bnkb.MaBNhan
                                }).Count();
            var SoNgaydtNgoaiTru = (from bnkb in _Data.BNKBs
                                    join rv in _Data.RaViens on bnkb.MaBNhan equals rv.MaBNhan
                                    join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                    join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                                    where kp.PLoai == "Phòng khám" && bn.NoiTru == 0 && bn.DTNT == true && bn.Status == 3 && bnkb.NgayKham >= FirstDayThisMonth && bnkb.NgayKham <= LastDayThisMonth
                                    where bnkb.MaKP == _makp 
                                    select new
                                    {
                                        rv.SoNgaydt,
                                        bnkb.MaBNhan
                                    }).Sum(p => p.SoNgaydt);
            var BNdtNoiTru = (from bnkb in _Data.BNKBs
                              join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                              join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                              where kp.PLoai == "Phòng khám" && bn.NoiTru == 1 && bn.Status == 3 && bnkb.NgayKham >= FirstDayThisMonth && bnkb.NgayKham <= LastDayThisMonth
                              where bnkb.MaKP == _makp 
                              select new
                              {
                                  bnkb.MaBNhan

                              }).Count();
            var o = (from bnkb in _Data.BNKBs
                     join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                     join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                     where kp.PLoai == "Phòng khám" && bn.NoiTru == 1 && bn.Status == 3 && bn.Tuoi < 6 && bnkb.NgayKham >= FirstDayThisMonth && bnkb.NgayKham <= LastDayThisMonth
                     where bnkb.MaKP == _makp 
                     select new
                     {
                         bnkb.MaBNhan
                     }).Count();
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            string s = "Tháng " + cboThang.Text + " Năm " + cboNam.Text;
            _dic.Add("Khoa", lupKhoa.Text);
            _dic.Add("Ngaythang", s.ToUpper());
            _dic.Add("BNngoaitru", BNdtNgoaiTru);
            _dic.Add("SoNgayDTNgoaiTru", SoNgaydtNgoaiTru);
            _dic.Add("BNnoitru", BNdtNoiTru);
            _dic.Add("TresautuoiNT", o);
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BC_KhamBenhTheo_PK, _list, _dic, false);
        }

        public bc591 getbc(DateTime date)
        {
            int _makp = 0;
            _makp = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
            var NgayTH = (from bnkb in _Data.BNKBs
                          join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                          join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                          where kp.PLoai == "Phòng khám" && bn.DTuong == "BHYT" && bn.Status == 3
                          where bnkb.MaKP == _makp
                          select new
                          {
                          }).ToList();
            int BH_dem = 0;
            var BH = (from bnkb in _Data.BNKBs
                      join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                      where kp.PLoai == "Phòng khám" && bn.DTuong == "BHYT" && bn.Status == 3
                      where bnkb.MaKP == _makp 
                      select new
                      {
                          bnkb.MaBNhan,
                          bnkb.NgayKham
                      }).ToList();
            foreach (var item in BH)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    BH_dem++;
            }
            int ND_dem = 0;
            var ND = (from bnkb in _Data.BNKBs
                      join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                      where kp.PLoai == "Phòng khám" && bn.DTuong == "Dịch vụ" && bn.Status == 3
                      where bnkb.MaKP == _makp 
                      select new
                      {
                          bnkb.MaBNhan,
                          bnkb.NgayKham
                      }).ToList();
            foreach (var item in ND)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    ND_dem++;
            }
            int a_dem = 0;
            var a = (from bnkb in _Data.BNKBs
                     join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                     join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                     where kp.PLoai == "Phòng khám" && bn.Tuoi < 6 && bn.Status == 3
                     where bnkb.MaKP == _makp 
                     select new
                     {
                         bnkb.MaBNhan,
                         bnkb.NgayKham
                     }).ToList();
            foreach (var item in a)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    a_dem++;
            }
            int b_dem = 0;
            var b = (from bnkb in _Data.BNKBs
                     join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                     join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                     where kp.PLoai == "Phòng khám" && bn.Tuoi < 15 && bn.Status == 3
                     where bnkb.MaKP == _makp 
                     select new
                     {
                         bnkb.MaBNhan,
                         bnkb.NgayKham,
                     }).ToList();
            foreach (var item in b)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    b_dem++;
            }
            int c_dem = 0;
            var c = (from bnkb in _Data.BNKBs
                     join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                     join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                     where kp.PLoai == "Phòng khám" && bn.Tuoi > 60 && bn.Status == 3
                     where bnkb.MaKP == _makp 
                     select new
                     {
                         bnkb.MaBNhan,
                         bnkb.NgayKham
                     }).ToList();
            foreach (var item in c)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    c_dem++;
            }
            int HN_dem = 0;
            var HN = (from bnkb in _Data.BNKBs
                      join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                      where kp.PLoai == "Phòng khám" && bn.MaDTuong == "HN" && bn.Status == 3
                      where bnkb.MaKP == _makp 
                      select new
                      {
                          bnkb.MaBNhan,
                          bnkb.NgayKham
                      }).ToList();
            foreach (var item in HN)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    HN_dem++;
            }
            int KSK_dem = 0;
            var KSK = (from bnkb in _Data.BNKBs
                       join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                       join kp in _Data.KPhongs on bnkb.MaKP equals kp.MaKP
                       where kp.PLoai == "Phòng khám" && bn.DTuong == "KSK" && bn.Status != 0
                       where bnkb.MaKP == _makp 
                       select new
                       {
                           bnkb.MaBNhan,
                           bnkb.NgayKham
                       }).ToList();
            foreach (var item in KSK)
            {
                if (item.NgayKham.Value.Date == date.Date)
                    KSK_dem++;
            }
            bc591 bc = new bc591
            {
                Ngay = date.ToShortDateString(),
                BaoHiem = BH_dem,
                NhanDan = ND_dem,
                SauTuoi = a_dem,
                SauMuoiTuoi = c_dem,
                MuoiLamTuoi = b_dem,
                HoNgheo = HN_dem,
                KhamSucKhoe = KSK_dem
            };
            return bc;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
