using DevExpress.XtraEditors;
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
    public partial class Frm_DS_BenhNhan : Form
    {
        public Frm_DS_BenhNhan()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Frm_DS_BenhNhan_Load(object sender, EventArgs e)
        {
            dtpDenNgay.DateTime = dtpTuNgay.DateTime = DateTime.Now;
            lupLayTheoNgay.SelectedIndex = 0;
            lupKhoa.Enabled = false;
            cboDoiTuong.SelectedIndex = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                lupKhoa.EditValue = DungChung.Bien.MaKP;
                panel1.Visible = true;
                var KP = (from kp1 in data.KPhongs.Where(p => p.PLoai == ("Lâm sàng")) select new { kp1.TenKP, kp1.MaKP }).ToList();
                if (KP.Count > 0)
                {
                    lupKhoa.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
                }
            }
        }

        private class DsBn
        {
            public int MaBN { get; set; }
            public string TenBNhan { get; set; }
            public string NamSinh { get; set; }
            public string SThe { get; set; }
            public string DChi { get; set; }
            public string DThoai { get; set; }
            public string MaICD { get; set; }
            public string TenICD { get; set; }
            public int? MaKP { get; set; }
            public string TenKP { get; set; }
            public string NgayVao { get; set; }
            public string NgayRa { get; set; }

        }
        List<DsBn> listDsBn = new List<DsBn>();

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            listDsBn.Clear();
            int MaKP = 0;
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            if(chkkhoa.Checked == true)
            {
                MaKP = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
            }
            int IndexValue = lupLayTheoNgay.SelectedIndex;
            var BenhNhanChung = (from bn in data.BenhNhans.Where(p => (cboDoiTuong.SelectedIndex == 0 ? true : p.DTuong.Contains(cboDoiTuong.Text)))
                                 join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                 join icd in data.ICD10 on bnkb.MaICD equals icd.MaICD
                                 join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                 join kp in data.KPhongs.Where(p => (MaKP == 0 ? true : p.MaKP == MaKP)) on bn.MaKP equals kp.MaKP
                                 select new { bn, bnkb, icd, ttbx, kp }).ToList();

            if (IndexValue == 0)
            {
                var BenhNhanDDT = (from bn in BenhNhanChung 
                                   //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                   join vv in data.VaoViens on bn.bn.MaBNhan equals vv.MaBNhan
                                   select new { bn, vv }).ToList();

                var listDsBnDDT = (from bn in BenhNhanDDT.Where(p => (p.bn.bn.Status == 1 || p.bn.bn.Status == 4 || p.bn.bn.Status == 5) && p.bn.bn.NNhap >= TuNgay && p.bn.bn.NNhap <= DenNgay && p.bn.bn.MaKP == p.bn.bnkb.MaKP && p.bn.bn.NoiTru == 1)
                                   select new DsBn
                                   {
                                       MaBN = bn.bn.bn.MaBNhan,
                                       TenBNhan = bn.bn.bn.TenBNhan,
                                       NamSinh = bn.bn.bn.NamSinh,
                                       SThe = bn.bn.bn.SThe,
                                       DChi = bn.bn.bn.DChi,
                                       DThoai = bn.bn.ttbx.DThoai,
                                       MaICD = bn.bn.bnkb.MaICD,
                                       TenICD = bn.bn.icd.TenICD,
                                       MaKP = bn.bn.bn.MaKP,
                                       TenKP = bn.bn.kp.TenKP,
                                       //(bn.rvnew.NgayVao == null ? DateTime.MinValue : NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rvnew.NgayVao), 11),
                                       NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.vv.NgayVao), 11),
                                       NgayRa = " "
                                   }).ToList();


                var BenhNhanDRV = (from bn in BenhNhanChung
                                   join rv in data.RaViens on bn.bn.MaBNhan equals rv.MaBNhan
                                   join vv in data.VaoViens on bn.bn.MaBNhan equals vv.MaBNhan
                                   select new { bn, rv,vv }).ToList();

                var listDsBnDRV = (from bn in BenhNhanDRV.Where(p => (p.bn.bn.Status == 2 || p.bn.bn.Status == 3) && p.bn.bn.NNhap >= TuNgay && p.bn.bn.NNhap <= DenNgay && p.bn.bn.MaKP == p.bn.bnkb.MaKP && p.bn.bn.NoiTru == 1)
                                   select new DsBn
                                   {
                                       MaBN = bn.bn.bn.MaBNhan,
                                       TenBNhan = bn.bn.bn.TenBNhan,
                                       NamSinh = bn.bn.bn.NamSinh,
                                       SThe = bn.bn.bn.SThe,
                                       DChi = bn.bn.bn.DChi,
                                       DThoai = bn.bn.ttbx.DThoai,
                                       MaICD = bn.bn.bnkb.MaICD,
                                       TenICD = bn.bn.icd.TenICD,
                                       MaKP = bn.bn.bn.MaKP,
                                       TenKP = bn.bn.kp.TenKP,
                                       NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.vv.NgayVao), 11),
                                       NgayRa = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 11)
                                   }).ToList();

                var listDsBnTemp = listDsBnDDT.Union(listDsBnDRV);
                listDsBn = listDsBnTemp.ToList();
            }
            else if (IndexValue == 1)
            {
                var BenhNhan = (from bn in BenhNhanChung
                                    //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                join vv in data.VaoViens on bn.bn.MaBNhan equals vv.MaBNhan
                                select new { bn, vv }).ToList();

                listDsBn = (from bn in BenhNhan.Where(p => (p.bn.bn.Status == 1 || p.bn.bn.Status == 4 || p.bn.bn.Status == 5) && p.vv.NgayVao >= TuNgay && p.vv.NgayVao <= DenNgay && p.bn.bn.MaKP == p.bn.bnkb.MaKP && p.bn.bn.NoiTru == 1)
                            select new DsBn
                            {
                                MaBN = bn.bn.bn.MaBNhan,
                                TenBNhan = bn.bn.bn.TenBNhan,
                                NamSinh = bn.bn.bn.NamSinh,
                                SThe = bn.bn.bn.SThe,
                                DChi = bn.bn.bn.DChi,
                                DThoai = bn.bn.ttbx.DThoai,
                                MaICD = bn.bn.bnkb.MaICD,
                                TenICD = bn.bn.icd.TenICD,
                                MaKP = bn.bn.bn.MaKP,
                                TenKP = bn.bn.kp.TenKP,
                                NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.vv.NgayVao), 11),
                                //NgayRa = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 11)
                                NgayRa = " "
                            }).ToList();
            }
            else
            {
                var BenhNhan = (from bn in BenhNhanChung
                                join rv in data.RaViens on bn.bn.MaBNhan equals rv.MaBNhan
                                join vv in data.VaoViens on bn.bn.MaBNhan equals vv.MaBNhan
                                select new { bn, rv, vv }).ToList();

                listDsBn = (from bn in BenhNhan.Where(p => p.bn.bn.Status == 2 || p.bn.bn.Status == 3 && p.rv.NgayRa >= TuNgay && p.rv.NgayRa <= DenNgay && p.bn.bn.MaKP == p.bn.bnkb.MaKP && p.bn.bn.NoiTru == 1)
                            select new DsBn
                            {
                                MaBN = bn.bn.bn.MaBNhan,
                                TenBNhan = bn.bn.bn.TenBNhan,
                                NamSinh = bn.bn.bn.NamSinh,
                                SThe = bn.bn.bn.SThe,
                                DChi = bn.bn.bn.DChi,
                                DThoai = bn.bn.ttbx.DThoai,
                                MaICD = bn.bn.bnkb.MaICD,
                                TenICD = bn.bn.icd.TenICD,
                                MaKP = bn.bn.bn.MaKP,
                                TenKP = bn.bn.kp.TenKP,
                                NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayVao), 11),
                                NgayRa = DungChung.Ham.NgaySangChu(Convert.ToDateTime(bn.rv.NgayRa), 11)
                            }).ToList();
            }

            if (listDsBn.Count > 0)
            {
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_DSBenhNhan_ID580, listDsBn.ToList(), new Dictionary<string, object>(), false);
                if (chkXuatEX.Checked == true)
                {
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    string[] _tieude = { "STT", "MA_BENHNHAN", "TEN_BENHNHAN", "NAM_SINH", "MA_SOBHXH", "MA_THE", "DIA_CHI", "DIEN_THOAI", "MA_ICD", "TEN_ICD", "MA_KHOAPHONG", "TEN_KHOAPHONG", "NGAY_VAO", "NGAY_RA" };
                    DungChung.Bien.MangHaiChieu = new Object[listDsBn.ToList().Count + 1, 26];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    int num = 1;
                    foreach (var r in listDsBn.ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaBN;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.NamSinh;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SThe == "" ? "" : r.SThe.Substring(5, 10);
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SThe;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.DChi;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DThoai;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.MaICD;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TenICD;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.MaKP;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.TenKP;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.NgayVao;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.NgayRa;
                        num++;
                    }
                    SaveFileDialog op = new SaveFileDialog();
                    string filepatd;
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        filepatd = op.FileName + ".xls";
                    }
                    else
                    {
                        filepatd = "C:\\DS_BENHNHAN" + DateTime.Now + ".xls";
                    }

                    DungChung.Ham.xuatExcel(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", filepatd, true);
                    XtraMessageBox.Show("Xuất File thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkkhoa_CheckedChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                if (chkkhoa.Checked == true)
                {
                    lupKhoa.Enabled = true;
                    lupKhoa.EditValue = DungChung.Bien.MaKP;
                    var KP = (from kp1 in data.KPhongs.Where(p => p.PLoai == ("Lâm sàng")) select new { kp1.TenKP, kp1.MaKP }).ToList();
                    if (KP.Count > 0)
                    {
                        lupKhoa.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
                    }
                }
                else
                {
                    lupKhoa.Enabled = false;
                    lupKhoa.Properties.DataSource = null;
                }
            }
        }
    }
}
