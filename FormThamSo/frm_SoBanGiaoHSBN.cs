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
    public partial class frm_SoBanGiaoHSBN : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoBanGiaoHSBN()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<frm_ThongKeBNHuySoHSBA.KhoaPhong> lstKP = new List<frm_ThongKeBNHuySoHSBA.KhoaPhong>();
        List<CanBo> _lCanBo = new List<CanBo>();
        List<CanBo> _lCanBo1 = new List<CanBo>();
        List<CanBo> _lCanBo2 = new List<CanBo>();
        private void frm_SoBanGiaoHSBN_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
           
            lstKP = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Select(p => new frm_ThongKeBNHuySoHSBA.KhoaPhong { MaKP = p.MaKP, TenKP = p.TenKP }).ToList();
            //frm_ThongKeBNHuySoHSBA.KhoaPhong all = new frm_ThongKeBNHuySoHSBA.KhoaPhong();
            //all.MaKP = 0;
            //all.TenKP = "Tất cả";
            //lstKP.Insert(0, all);
            lupKhoa.Properties.DataSource = lstKP;
            
            var qcb = (from tcb in _data.CanBoes
                       select new { MaCB = tcb.MaCB, TenCB = tcb.TenCB, MaKP = tcb.MaKP }).ToList();
            foreach (var a in qcb)
            {
                CanBo cb0 = new CanBo();
                cb0.MaCB = a.MaCB;
                cb0.TenCB = a.TenCB;
                cb0.MaKP = a.MaKP;
                _lCanBo.Add(cb0);
            }
            _lCanBo = _lCanBo.OrderBy(p => p.TenCB).ToList();
            //CanBo cb = new CanBo();
            //cb.TenCB = "Tất cả";
            //cb.MaCB = "0";
            //_lCanBo.Insert(0, cb);
            lupnguoigiao.Properties.DataSource = _lCanBo;
            lupnguoinhan.Properties.DataSource = _lCanBo;
        }

        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {
            if(lupKhoa.EditValue != "")
            {
                int makp = Convert.ToInt32(lupKhoa.EditValue);
                _lCanBo1 = _lCanBo.Where(p => p.MaKP == makp).ToList();
                lupnguoigiao.Properties.DataSource = _lCanBo1;
                _lCanBo2 = _lCanBo.Where(p => p.MaKP != makp).ToList();
                lupnguoinhan.Properties.DataSource = _lCanBo2;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string tenkp = lupKhoa.Text;
            BaoCao.rep_bia_SoBanGiaoHSBN1 rep = new BaoCao.rep_bia_SoBanGiaoHSBN1();
            frmIn frm = new frmIn();
            rep.celkhoa.Text = tenkp;
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime daungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime cuoingay = DungChung.Ham.NgayDen(lupNgaytu.DateTime);
            string tenkp = lupKhoa.Text;
            BaoCao.rep_ruot_SoBanGiaoHSBN1 rep = new BaoCao.rep_ruot_SoBanGiaoHSBN1();
            frmIn frm = new frmIn();
            rep.khoa.Text = tenkp;
            rep.nguoigiao.Text = lupnguoigiao.Text;
            rep.nguoigiao2.Text = lupnguoigiao.Text;
            rep.nguoinhan.Text = lupnguoinhan.Text;
            rep.nguoinhan2.Text = lupnguoinhan.Text;
            rep.ngay.Text = " ngày " + lupNgaytu.DateTime.Day + " / " + lupNgaytu.DateTime.Month + " / " + lupNgaytu.DateTime.Year;
            if(lupKhoa.EditValue != "")
            {
                int makp = Convert.ToInt32(lupKhoa.EditValue);
                var dshsbn = (from a in _data.RaViens.Where(p => p.MaKP == makp).Where(p => p.NgayRa >= daungay && p.NgayRa <= cuoingay)
                              join b in _data.VaoViens on a.MaBNhan equals b.MaBNhan
                              join c in _data.BenhNhans on a.MaBNhan equals c.MaBNhan
                              select new { a,b.SoBA, c.TenBNhan}).ToList();
                if(dshsbn.Count>0)
                {
                    rep.tongso.Text = Convert.ToString(dshsbn.Count);
                    if(dshsbn.Where(p => p.a.KetQua == "Tử vong").Count() > 0)
                    {
                        rep.tongsotuvong.Text = Convert.ToString(dshsbn.Where(p => p.a.KetQua == "Tử vong").Count());
                        rep.DataSource = dshsbn.Where(p => p.a.KetQua == "Tử vong").ToList();
                        rep.BindingData();
                    }
                }
                
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}