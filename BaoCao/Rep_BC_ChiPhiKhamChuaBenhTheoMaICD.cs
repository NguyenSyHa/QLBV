using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ChiPhiKhamChuaBenhTheoMaICD : Form
    {
        public Rep_BC_ChiPhiKhamChuaBenhTheoMaICD()
        {
            InitializeComponent();
            dtpDenNgay.DateTime = dtpTuNgay.DateTime = DateTime.Now;
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);

            var listMaICD = (from bn in data.BenhNhans.Where(p => p.DTuong == "BHYT" && p.Status == 3)
                             join bnkb in data.BNKBs.Where(p => p.PhuongAn == 0 || p.PhuongAn == 2) on bn.MaBNhan equals bnkb.MaBNhan
                             join vp in data.VienPhis.Where(p => p.NgayTT >= TuNgay && p.NgayTT <= DenNgay) on bnkb.MaBNhan equals vp.MaBNhan
                             join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             select new { bnkb.MaICD, bnkb.MaBNhan, vpct.ThanhTien }).ToList();

            var listGroupMaICD = (from test in ((from lm in listMaICD
                                                    group lm by new { lm.MaICD, lm.MaBNhan } into kq
                                                    orderby kq.Key.MaICD ascending
                                                    select new
                                                    {
                                                        MaBenhNhan = kq.Key.MaICD,
                                                        TongTienTT = kq.Sum(x => x.ThanhTien)
                                                    }).ToList())
                                                    group test by new {test.MaBenhNhan } into kq1
                                                   select new {
                                                       MaBenhNhan = kq1.Key.MaBenhNhan,
                                                       TongBenhNhan = kq1.Count(),
                                                       TongTienTT = kq1.Sum(x => x.TongTienTT)
                                                   }).ToList();



            if (listGroupMaICD.Count > 0)
            {
                //DungChung.Ham.Print(DungChung.PrintConfig.Rep_DSBenhNhan_ID580, listGroupMaICD.ToList(), new Dictionary<string, object>(), false);
                if (chkXuatEX.Checked == true)
                {
                    string[] _arr = new string[] { "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    string[] _tieude = { "STT", "MÃ BỆNH NHÂN", "SỐ LƯỢT", "SỐ TIỀN" };
                    DungChung.Bien.MangHaiChieu = new Object[listGroupMaICD.ToList().Count + 1, 26];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    int num = 1;
                    foreach (var r in listGroupMaICD.ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaBenhNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TongBenhNhan;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TongTienTT;
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
                        filepatd = "C:\\DS_CHIPHIKHAMBENH_MAICD" + DateTime.Now + ".xls";
                    }

                    DungChung.Ham.xuatExcel(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", filepatd, true);
                    XtraMessageBox.Show("Xuất File thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Dictionary<string, object> _dic = new Dictionary<string, object>();
            string date = "Từ ngày " + TuNgay.ToString("dd-MM-yyyy") + " đến ngày " + DenNgay.ToString("dd-MM-yyyy");
            _dic.Add("NameHeader", date);
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BC_ChiPhiKhamChuaBenhTheoMaICD, listGroupMaICD, _dic, false);
        }
    }
}
