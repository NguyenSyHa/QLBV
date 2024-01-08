using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormThamSo
{
    public partial class frm_TKTamThu : DevExpress.XtraEditors.XtraForm
    {
        public frm_TKTamThu()
        {
            InitializeComponent();
        }
        //DateTime tungay = System.DateTime.Now.Date;
        //DateTime denngay = System.DateTime.Now.Date;
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_TKTamThu_Load(object sender, EventArgs e)
        {
            var qcb = (from cb in _data.CanBoes join kp in _data.KPhongs.Where(p => p.PLoai == "Kế toán") on cb.MaKP equals kp.MaKP select new { cb.TenCB, cb.MaCB }).ToList();
            lupcanbo.Properties.DataSource = qcb.OrderBy(p => p.TenCB).ToList();
            dateTuNgay.DateTime = System.DateTime.Today;
            DateTime tungay = dateTuNgay.DateTime.AddDays(1).AddHours(23).AddMinutes(59);
            dateDenNgay.DateTime = tungay;
            string[] _coso = new string[21] { "24000", "24208", "24209", "24210", "24211", "24212", "24213", "24214", "24215", "24216", "24217", "24218", "24219", "24220", "24221", "24222", "24223", "24224", "24225", "24226", "24009" };

            radioGroup1.Enabled = false;
            #region Set chọn dữ liệu MaKCB cho riêng các nhánh con của bệnh viện 24009
            //for (int i = 0; i < _coso.Length;i++ )
            //{
            //    if (DungChung.Bien.MaBV == _coso[i].ToString())
            //    {
            //        var _makcb1 = (from bn in _data.BenhNhans join bv in _data.BenhViens on bn.MaKCB equals bv.MaBV select new { bn.MaKCB, bv.TenBV }).Distinct().ToList();
            //        lookUpEdit1.Properties.DataSource = _makcb1;
            //    }
            //    else
            //    {
            //        label5.Visible = false;
            //        lookUpEdit1.Visible = false;
            //        radioGroup1.Location = new Point(86, 122);
            //    }
            //}
            #endregion

            var _makcb = (from bn in _data.BenhNhans join bv in _data.BenhViens on bn.MaKCB equals bv.MaBV select new { bn.MaKCB, bv.TenBV }).Distinct().ToList();
            lookUpEdit1.Properties.DataSource = _makcb;


        }

        private bool CheckPL(int _mabn)
        {
            DateTime tungay = dateTuNgay.DateTime;
            DateTime denngay = dateDenNgay.DateTime;
            var q10 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                       join tthu in _data.TamUngs on bn.MaBNhan equals tthu.MaBNhan
                       where (tthu.NgayThu >= tungay && tthu.NgayThu <= denngay) && (tthu.PhanLoai == 4)
                       select new { bn.MaBNhan, bn.SThe, tthu.NgayThu, tthu.IDTamUng, tthu.PhanLoai, tthu.NgoaiGio }).ToList();
            if (q10.Count > 0)
                return true;
            else
                return false;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = dateTuNgay.DateTime;
            DateTime denngay = dateDenNgay.DateTime;
            //tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
            //denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
            int noitru = cboNoiTru.SelectedIndex;

            string tieude = "";
            int tamthu;

            string _macb = "";
            if (lupcanbo.EditValue != null)
                _macb = lupcanbo.EditValue.ToString();
            if (radTamthu.SelectedIndex == 0)
            {
                tamthu = 0;
                tieude = "DANH SÁCH BỆNH NHÂN NỘP TẠM THU";
            }
            if (radTamthu.SelectedIndex == 1)
            {
                tamthu = 4;
                tieude = "DANH SÁCH BỆNH NHÂN THANH TOÁN TẠM THU";
            }
            else
            {
                tamthu = 0;
                tieude = "DANH SÁCH BỆNH NHÂN CHƯA THANH TOÁN TẠM THU";
            }
            #region Bệnh viên khác 24009
            //if (DungChung.Bien.MaBV != "24009")
            //{
            //    var q = (from bn in _data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru)
            //             join tthu in _data.TamUngs.Where(p => p.PhanLoai == tamthu) on bn.MaBNhan equals tthu.MaBNhan
            //             join cb in _data.CanBoes.Where(p => _macb == "" ? true : p.MaCB == _macb) on tthu.MaCB equals cb.MaCB into kq
            //             from a in kq.DefaultIfEmpty()
            //             where (tthu.NgayThu >= tungay && tthu.NgayThu <= denngay)
            //             select new { bn.TenBNhan, bn.MaBNhan, bn.SThe, tthu.NgayThu, tthu.IDTamUng,tthu.PhanLoai, tthu.LyDo, tthu.SoTien, TenCB = a != null ? a.TenCB : "", tthu.NgoaiGio }).ToList().Select(p => new { p.MaBNhan, p.TenBNhan, p.SThe, p.IDTamUng, p.LyDo, p.SoTien, p.NgayThu, p.TenCB, p.NgoaiGio,p.PhanLoai }).OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();



            //    var q2 = (from bn in _data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru)
            //             join tthu in _data.TamUngs.Where(p => p.PhanLoai == 4) on bn.MaBNhan equals tthu.MaBNhan
            //             join cb in _data.CanBoes.Where(p => _macb == "" ? true : p.MaCB == _macb) on tthu.MaCB equals cb.MaCB into kq
            //             from a in kq.DefaultIfEmpty()
            //             where (tthu.NgayThu >= tungay && tthu.NgayThu <= denngay)
            //             select new { bn.TenBNhan, bn.MaBNhan, bn.SThe, tthu.NgayThu, tthu.IDTamUng, tthu.PhanLoai, tthu.LyDo, tthu.SoTien, TenCB = a != null ? a.TenCB : "", tthu.NgoaiGio }).ToList().Select(p => new { p.MaBNhan, p.TenBNhan, p.SThe, p.IDTamUng, p.LyDo, p.SoTien, p.NgayThu, p.TenCB, p.NgoaiGio, p.PhanLoai }).OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();



            //    if(radTamthu.SelectedIndex==2)
            //    {
            //        int i, j;
            //        int count = 0;
            //        for (i = 0; i < q.Count; i++)
            //        {
            //            for (j = 0; j < q2.Count; j++)
            //            {
            //                if (q[i].MaBNhan == q2[j].MaBNhan)
            //                {
            //                    count++;
            //                    q.RemoveAt(i);
            //                }
            //            }
            //        }
            //    }


            //        if (rdMauIn.SelectedIndex == 1)
            //        {

            //            frmIn frm = new frmIn();
            //            BaoCao.rep_TKTamThu_02 rep = new BaoCao.rep_TKTamThu_02();
            //            rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
            //            rep.TT.Value = radTamthu.SelectedIndex;
            //            rep.Title.Value = tieude;
            //            rep.DataSource = q;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //        }
            //        else if (rdMauIn.SelectedIndex == 0)
            //        {
            //            frmIn frm = new frmIn();
            //            BaoCao.rep_TKTamThu rep = new BaoCao.rep_TKTamThu();
            //            rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
            //            rep.TT.Value = radTamthu.SelectedIndex;
            //            rep.Title.Value = tieude;
            //            rep.DataSource = q;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //        }
            //        else
            //        {
            //            if (radioGroup1.SelectedIndex == 0)
            //                q = q.Where(p => p.NgoaiGio == 0).ToList();
            //            else if (radioGroup1.SelectedIndex == 1)
            //                q = q.Where(p => p.NgoaiGio == 1).ToList();
            //            frmIn frm = new frmIn();
            //            BaoCao.rep_TKTamThu_03 rep = new BaoCao.rep_TKTamThu_03();
            //            rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
            //            rep.TT.Value = radTamthu.SelectedIndex;
            //            rep.Title.Value = tieude;
            //            rep.DataSource = q;
            //            rep.BindingData();
            //            rep.CreateDocument();
            //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //            frm.ShowDialog();
            //        }
            //}
            //#endregion
            //#region Bệnh viên 24009
            //else
            //{
            string _makcb = lookUpEdit1.EditValue.ToString();

            var q = (from bn in _data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru)
                     join tthu in _data.TamUngs.Where(p => p.PhanLoai == tamthu) on bn.MaBNhan equals tthu.MaBNhan
                     join kp in _data.KPhongs on tthu.MaKP equals kp.MaKP
                     join cb in _data.CanBoes on tthu.MaCB equals cb.MaCB into kq
                     from a in kq.DefaultIfEmpty()
                     where (tthu.NgayThu >= tungay && tthu.NgayThu <= denngay)
                     select new { bn.MaKCB, bn.TenBNhan, bn.MaBNhan, bn.SThe, tthu.NgayThu, tthu.IDTamUng, tthu.PhanLoai, tthu.LyDo, tthu.SoTien, a.MaCB, TenCB = a != null ? a.TenCB : "", tthu.NgoaiGio,tthu.MaKP,kp.TenKP }).Where(x => _makcb == "" ? true : x.MaKCB == _makcb).Where(p => _macb == "" ? true : p.MaCB == _macb).ToList().Select(p => new { p.MaKP,p.TenKP,p.MaBNhan, p.TenBNhan, p.SThe, p.IDTamUng, p.LyDo, p.SoTien, p.NgayThu, p.TenCB, p.NgoaiGio, p.PhanLoai }).OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();

            if (q.Count > 0)
            {
                var q2 = (from bn in _data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru)
                          join tthu in _data.TamUngs.Where(p => p.PhanLoai == 4) on bn.MaBNhan equals tthu.MaBNhan
                          join kp in _data.KPhongs on tthu.MaKP equals kp.MaKP
                          join cb in _data.CanBoes on tthu.MaCB equals cb.MaCB into kq
                          from a in kq.DefaultIfEmpty()
                          where (tthu.NgayThu >= tungay && tthu.NgayThu <= denngay)
                          select new { bn.MaKCB, bn.TenBNhan, bn.MaBNhan, bn.SThe, tthu.NgayThu, tthu.IDTamUng, tthu.PhanLoai, tthu.LyDo, tthu.SoTien, a.MaCB, TenCB = a != null ? a.TenCB : "", tthu.NgoaiGio,tthu.MaKP,kp.TenKP }).Where(x => _makcb == "" ? true : x.MaKCB == _makcb).Where(p => _macb == "" ? true : p.MaCB == _macb).ToList().Select(p => new {p.MaKP,p.TenKP, p.MaBNhan, p.TenBNhan, p.SThe, p.IDTamUng, p.LyDo, p.SoTien, p.NgayThu, p.TenCB, p.NgoaiGio, p.PhanLoai }).OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();

                if (radTamthu.SelectedIndex == 2)
                {
                    var qq = q.Where(o => !q2.Select(p => p.MaBNhan).Contains(o.MaBNhan)).ToList();
                    if (rdMauIn.SelectedIndex == 1)
                    {

                        frmIn frm = new frmIn();
                        BaoCao.rep_TKTamThu_02 rep = new BaoCao.rep_TKTamThu_02();
                        rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                        rep.TT.Value = radTamthu.SelectedIndex;
                        rep.Title.Value = tieude;
                        rep.DataSource = qq;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else if (rdMauIn.SelectedIndex == 0)
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_TKTamThu rep = new BaoCao.rep_TKTamThu();
                        rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                        rep.TT.Value = radTamthu.SelectedIndex;
                        rep.Title.Value = tieude;
                        rep.DataSource = qq;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (radioGroup1.SelectedIndex == 0)
                            qq = qq.Where(p => p.NgoaiGio == 0).ToList();
                        else if (radioGroup1.SelectedIndex == 1)
                            qq = qq.Where(p => p.NgoaiGio == 1).ToList();
                        frmIn frm = new frmIn();
                        BaoCao.rep_TKTamThu_03 rep = new BaoCao.rep_TKTamThu_03();
                        rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                        rep.TT.Value = radTamthu.SelectedIndex;
                        rep.Title.Value = tieude;
                        rep.DataSource = qq;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    if (rdMauIn.SelectedIndex == 1)
                    {

                        frmIn frm = new frmIn();
                        BaoCao.rep_TKTamThu_02 rep = new BaoCao.rep_TKTamThu_02();
                        rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                        rep.TT.Value = radTamthu.SelectedIndex;
                        rep.Title.Value = tieude;
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else if (rdMauIn.SelectedIndex == 0)
                    {
                        if(DungChung.Bien.MaBV =="30010")
                        {
                            string _ngay = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                            Dictionary<string, object> _dic = new Dictionary<string, object>();
                            _dic.Add("Title",tieude);
                            _dic.Add("Ngay",_ngay);
                            DungChung.Ham.Print(DungChung.PrintConfig.rp_ID22_30010, q, _dic, false);
                        }
                        else 
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep_TKTamThu rep = new BaoCao.rep_TKTamThu();
                            rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                            rep.TT.Value = radTamthu.SelectedIndex;
                            rep.Title.Value = tieude;
                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        
                    }
                    else
                    {
                        if (radioGroup1.SelectedIndex == 0)
                            q = q.Where(p => p.NgoaiGio == 0).ToList();
                        else if (radioGroup1.SelectedIndex == 1)
                            q = q.Where(p => p.NgoaiGio == 1).ToList();
                        frmIn frm = new frmIn();
                        BaoCao.rep_TKTamThu_03 rep = new BaoCao.rep_TKTamThu_03();
                        rep.Ngay.Value = "Từ " + tungay.ToString().Substring(0, 10) + " đến " + denngay.ToString().Substring(0, 10);
                        rep.TT.Value = radTamthu.SelectedIndex;
                        rep.Title.Value = tieude;
                        rep.DataSource = q;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }


                
            }
            else MessageBox.Show("Không có dữ liệu !");
            //}
            #endregion
        }



        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rdMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdMauIn.SelectedIndex == 2)
            {
                radioGroup1.Enabled = true;
            }
            else
                radioGroup1.Enabled = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}