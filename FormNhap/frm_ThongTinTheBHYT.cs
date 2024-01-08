using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_ThongTinTheBHYT : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThongTinTheBHYT(List<frmHSBNNhapMoi.LichSuKCB2018> _ls)
        {
            InitializeComponent();
            ls = _ls.ToList();
        }
        List<QLBV.FormNhap.frmHSBNNhapMoi.LichSuKCB2018> ls = new List<frmHSBNNhapMoi.LichSuKCB2018>();
        public class LichSuKCB2018New
        {
            public string maHoSo { set; get; }
            public string maCSKCB { set; get; }
            public DateTime ngayVao { set; get; }
            public DateTime ngayRa { set; get; }
            public string tenBenh { set; get; }
            public string tinhTrang { set; get; }
            public string kqDieuTri { set; get; }
        }
        QLBV_Database.QLBVEntities _data;
        List<LichSuKCB2018New> lsnew = new List<LichSuKCB2018New>();
        private void frm_ThongTinTheBHYT_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BenhVien> _lbv = new List<BenhVien>();
            _lbv = _data.BenhViens.ToList();
            foreach (var item in ls)
            {
                LichSuKCB2018New moi = new LichSuKCB2018New();
                moi.maHoSo = item.maHoSo;
                var tencs = _lbv.Where(p => p.MaBV == item.maCSKCB).ToList();
                if (tencs.Count() > 0)
                    moi.maCSKCB = tencs.First().TenBV;
                else
                    moi.maCSKCB = item.maCSKCB;
                DateTime ngayvao = new DateTime();
                DateTime.TryParseExact(item.ngayVao, "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out ngayvao);
                DateTime ngayra= new DateTime();
                DateTime.TryParseExact(item.ngayRa, "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out ngayra);
                moi.ngayRa = ngayra;
                //string FormattedDate=ngayvao.ToString
                moi.ngayVao = ngayvao;
                moi.tenBenh = item.tenBenh;
                moi.tinhTrang = item.tinhTrang;
                string Kqdt = "";
                switch(item.tinhTrang)
                {
                    case "1":
                        Kqdt = "Khỏi";
                        break;
                    case "2":
                        Kqdt = "Đỡ";
                        break;
                    case "3":
                        Kqdt = "Không thay đổi";
                        break;
                    case "4":
                        Kqdt = "Nặng hơn";
                        break;
                    case "5":
                        Kqdt = "Xin ra viện";
                        break;
                    default:
                        Kqdt = "Không xác định";
                        break;
                }
                moi.kqDieuTri = Kqdt;
                lsnew.Add(moi);
            }
            deDenNgay.DateTime = DateTime.Now;
            deTuNgay.DateTime = DateTime.Now.AddMonths(-3);
            TimKiem();
            //gridView1.DataSource=
        }
        void TimKiem()
        {
            DateTime ngayvao = DungChung.Ham.NgayTu(deTuNgay.DateTime);
            DateTime ngayra = DungChung.Ham.NgayDen(deDenNgay.DateTime);
            var skq = lsnew.Where(p => p.ngayVao >= ngayvao && p.ngayRa <= ngayra).OrderByDescending(p => p.ngayVao).ToList();
            txtSoLandt.Text = skq.Count().ToString();
            bindingSource1.DataSource = skq;// lsnew.Where(p => p.ngayVao >= ngayvao && p.ngayRa <= ngayra).OrderByDescending(p => p.ngayVao).ToList();
            gridControl1.DataSource = bindingSource1;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void deDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtSoLandt_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoLandt.Text) && txtSoLandt.Text == "0")
            {
                MessageBox.Show("Kiểm tra thời gian đợt điề trị dài hơn");
            }
        }
    }
}