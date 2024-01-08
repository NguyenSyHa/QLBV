using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BangKeChungTuThu_30003 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BangKeChungTuThu_30003()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities db;
        List<CanBo> _lcb = new List<CanBo>();
        private void frm_BangKeChungTuThu_30003_Load(object sender, EventArgs e)
        {
            db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            rgplthu.SelectedIndex = 2;
            rgdtuong.SelectedIndex = 3;
            detungay.DateTime = System.DateTime.Now;
            dedenngay.DateTime = System.DateTime.Now;
            _lcb = db.CanBoes.Where(p => p.Status == 1).ToList();
            _lcb.Add(new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupCB.Properties.DataSource = _lcb.OrderBy(p => p.MaCB);
            lupCB.EditValue = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            DateTime _tungay=DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime _denngay=DungChung.Ham.NgayDen(dedenngay.DateTime);
            db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int ntru = 2;
            if (NoiNgoaiTru.SelectedIndex == 0)
                ntru = 1;
            if (NoiNgoaiTru.SelectedIndex == 1)
                ntru = 0;

            int PlThu = rgplthu.SelectedIndex;
            string _MaCB = "";
            if (lupCB.EditValue != null)
                _MaCB = lupCB.EditValue.ToString();
            string Dtuong="";
            if(rgdtuong.SelectedIndex==0)
                Dtuong="BHYT";
            else if (rgdtuong.SelectedIndex==1)
                Dtuong="Dịch vụ";
            else if(rgdtuong.SelectedIndex==2)
                Dtuong="KSK";
            var _lTamUng = (from tu in db.TamUngs.Where(p => _MaCB == "" ? true : p.MaCB == _MaCB).Where(p => PlThu == 2 ? (p.PhanLoai == 1 || p.PhanLoai == 3) : (PlThu == 0 ? p.PhanLoai == 1 : p.PhanLoai == 3)).Where(p => _tungay <= p.NgayThu && p.NgayThu <= _denngay)
                                .Where(p => TienPT.SelectedIndex == 1 ? (p.SoTien > 0 && p.SoTien <= 200000) : (TienPT.SelectedIndex == 0 ? p.SoTien > 200000 : p.SoTien > 0) )
                            join bn in db.BenhNhans.Where(p => Dtuong == "" ? true : p.DTuong == Dtuong).Where(p => p.NoiTru == ntru || ntru ==2) on tu.MaBNhan equals bn.MaBNhan
                            join kp in db.KPhongs on bn.MaKP equals kp.MaKP
                            select new
                            {
                                tu.IDTamUng,
                                tu.SoHD,
                                tu.SoTien,
                                bn.TenBNhan,
                                DonVi = "Lần",
                                bn.DChi,
                                tu.LyDo,
                                SoLuong = "1",
                                bn.NoiTru,
                                kp.TenKP
                            }).Distinct().OrderBy(p => p.IDTamUng).ToList();
            string tencb = db.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault().TenCB;
            frmIn frm = new frmIn();
            BaoCao.rep_BangKeChungTuThu_30003 rep = new BaoCao.rep_BangKeChungTuThu_30003();
            double tongtien = _lTamUng.Count > 0 ? _lTamUng.Sum(p => p.SoTien) ?? 0 : 0;
            rep.coltongtienchu.Text = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
            rep.TENCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.DIACHI.Value ="Địa chỉ: " + DungChung.Bien.DiaChi;
            rep.NGAY.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
            rep.MSTHUE.Value = "Mã số thuế: 0800726760";
            rep.xrTableCell31.Text = tencb;
            rep.DataSource = _lTamUng;
            rep.databinding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}