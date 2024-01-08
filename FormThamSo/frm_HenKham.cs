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
    public partial class frm_HenKham : DevExpress.XtraEditors.XtraForm
    {
        public frm_HenKham()
        {
            InitializeComponent();
        }
        public int _id;
        public frm_HenKham(int id)
        {
            InitializeComponent();
            _id = id;
        }
        private class TTBN
        {
            public DateTime? ThoiGianTYT_HK { get; set; }
            public int? MaKP_HenKham { get; set; }
            public int? MaKP { get; set; }
            public string TenBNhan { get; set; }
            public string GhiChu { get; set; }
            public DateTime? NgayHen { get; set; }
            public int MaBNhan { get; set; }
            public string SThe { get; set; }
            public string NamSinh { get; set; }
            public int? GTinh { get; set; }
            public string MaICD { get; set; }
            public DateTime? NgayKham { get; set; }
            public string ChanDoan { get; set; }
            public string MaCBDaiDien { get; set; }
            public string SoHK { get; set; }


        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        bool status = false;
        private void frm_HenKham_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "30007")
            {
                txtSoHenKham.Visible = false;
                labelControl7.Visible = false;
                lupTenCQ.Visible = false;
                labelControl9.Visible = false;
                dtNgayHenTYT.Visible = false;
                labelControl10.Visible = false;
                labelControl2.Text = "Ngày hẹn";
            }
            if (DungChung.Bien.MaBV == "30007")
            {
                lupTenCQ.Text = "TTYT huyện Tứ Kỳ";
            }     
            List<KPhong> _lkp = new List<KPhong>();
            if (DungChung.Bien.MaBV == "30007")
            {
                _lkp = (from kp in _data.KPhongs.Where(p => p.TenKP == "TTYT huyện Tứ Kỳ" )
                        select kp).ToList();
            }
            else
            {
                _lkp = (from kp in _data.KPhongs.Where(p => p.PLoai == "Xã phường")
                        select kp).ToList();
            }

            //if (_lkp.Count > 0)
           // {
               // lupTenCQ.Properties.DataSource = _lkp ;
           // }
            var bnkh = (from bn in _data.BenhNhans
                        join kb in _data.BNKBs.Where(p => p.IDKB == _id) on bn.MaBNhan equals kb.MaBNhan
                        select new TTBN 
                        {
                            ThoiGianTYT_HK = kb.ThoiGianTYT_HK, 
                            MaKP_HenKham = kb.MaKP_HenKham, 
                            MaKP = kb.MaKP, 
                            TenBNhan = bn.TenBNhan, 
                            GhiChu = kb.GhiChu, 
                            NgayHen = kb.NgayHen, 
                            MaBNhan = bn.MaBNhan, 
                            SThe = bn.SThe, 
                            NamSinh = bn.NamSinh, 
                            GTinh = bn.GTinh, 
                            MaICD = kb.MaICD, 
                            NgayKham = kb.NgayKham, 
                            ChanDoan = kb.ChanDoan + kb.BenhKhac, 
                            MaCBDaiDien = kb.MaCBDaiDien, 
                            SoHK = kb.SoHK }).ToList();                                                                                  
            if (bnkh.Count > 0)
            {

                labelControl3.Text = bnkh.First().TenBNhan + " - " + bnkh.First().NamSinh + " - " + bnkh.First().SThe;
                labelControl5.Text = "Ngày khám: " + bnkh.First().NgayKham;
                labelControl4.Text = "Chẩn đoán: " + bnkh.First().ChanDoan;
                memoGhiChu.Text = bnkh.First().GhiChu;
                if ( bnkh.First().SoHK != null)
                {
                    txtSoHenKham.Text = bnkh.First().SoHK.ToString();
                }
                if (bnkh.First().NgayHen != null)
                    dtNgayHen.DateTime = bnkh.First().NgayHen.Value;
                if (bnkh.First().ThoiGianTYT_HK != null)
                    dtNgayHenTYT.DateTime = bnkh.First().ThoiGianTYT_HK.Value;
                lupDD.EditValue = bnkh.First().MaCBDaiDien;
                if (bnkh.First().MaKP_HenKham != null)
                {
                    int? makp = bnkh.First().MaKP_HenKham;
                    lupTenCQ.EditValue = makp;
                    var kphk = _data.KPhongs.Where(p => p.MaKP == makp).ToList();
                    if (DungChung.Bien.MaBV == "30007" )
                    {
                        lupTenCQ.Text = "";
                    }
                    else
                    {
                        lupTenCQ.Text = kphk.First().TenKP;
                    }
                    

                }
                status = true;

                
            }

            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in _data.CanBoes
                       select cb).ToList();
            //_lkp.Add(new KPhong { TenKP = " ", MaKP == 0 });
            _lcanbo.Add(new CanBo { TenCB = " ", MaCB = "" });
            _lcanbo.OrderBy(p => p.TenCB);
            if (_lcanbo.Count > 0)
            {
                lupDD.Properties.DataSource = _lcanbo;
            }
            if (!File.Exists("TextGiayHenKham.txt"))
            {
                FileStream fs;
                fs = new FileStream("TextGiayHenKham.txt", FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine("");
                sWriter.Flush();
                fs.Close();

            }
            string[] lines = File.ReadAllLines("TextGiayHenKham.txt");
            if (lines[lines.Length - 1] == "1")
            {
                lupDD.Text = lines[lines.Length - 2];

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_idkb"></param>
        private void _InPhieu_GiayHenKhamLai_TT40_2015(int _idkb)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _ngayhen = new DateTime();
            DateTime _ngayhenTYT = new DateTime();
            DateTime _ngaykham = new DateTime();
            if (dtNgayHen.EditValue != null && dtNgayHen.EditValue.ToString() != "")
                _ngayhen = dtNgayHen.DateTime;
            if (dtNgayHenTYT.EditValue != null && dtNgayHenTYT.EditValue.ToString() != "")
                _ngayhenTYT = dtNgayHenTYT.DateTime;
            frmIn frm = new frmIn();
            BaoCao.Rep_GiayHenKham_TKy rep = new BaoCao.Rep_GiayHenKham_TKy();    
            if (DungChung.Bien.MaBV == "30007")
            {
                rep.PaperKind = System.Drawing.Printing.PaperKind.A4;
                rep.Landscape = false;
            }
            int _mabn = 0;
            var qkb = Data.BNKBs.Where(p => p.IDKB == _idkb)
                .ToList();           
            if (DungChung.Bien.MaBV == "30007")
            {               
                int? mkp = qkb.First().MaKP_HenKham;
                var kp = Data.KPhongs.Where(p => p.MaKP == mkp ).ToList();                              
                rep.noiHen.Value = lupTenCQ.Text;              
            }

        
            if (qkb.Count > 0)
                if (qkb.First().SoHK != "" && DungChung.Bien.MaBV== "30007")
                {
                    rep.SoHK.Value = "Số: " + qkb.First().SoHK.ToString();
                }
                else
                {
                    rep.SoHK.Value = "Số:.................... ";

                }
                _mabn = qkb.First().MaBNhan == null ? 0 : qkb.First().MaBNhan.Value;
            var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            if (qbn.Count() > 0)
            {
                var macbdd = qkb.First().MaCBDaiDien;
                var cbdd = Data.CanBoes.FirstOrDefault(o => o.MaCB == macbdd);
                if (cbdd != null)
                {
                    rep.DaiDien.Value = cbdd.TenCB;
                }
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                rep.DiaChi.Value = qbn.First().DChi;
                if (qbn.First().GTinh == 1)
                { rep.Nam.Value = "x"; }
                else { rep.Nu.Value = "x"; }
                if (qbn.First().SThe != null && qbn.First().SThe.Length >= 15)
                {
                    rep.SThe0.Value = qbn.First().SThe.Substring(0, 2);
                    rep.SThe1.Value = qbn.First().SThe.Substring(2, 1);
                    rep.SThe2.Value = qbn.First().SThe.Substring(3, 2);
                    rep.SThe3.Value = qbn.First().SThe.Substring(5, 2);
                    rep.SThe4.Value = qbn.First().SThe.Substring(7, 3);
                    rep.SThe5.Value = qbn.First().SThe.Substring(10, 5);
                }
                if (qbn.First().HanBHTu != null && qbn.First().HanBHTu.ToString().Length > 10)
                    rep.HanBHTu.Value = qbn.First().HanBHTu.ToString().Substring(0, 10);
                if (qbn.First().HanBHDen != null && qbn.First().HanBHDen.ToString().Length > 10)
                    rep.HanBHDen.Value = qbn.First().HanBHDen.ToString().Substring(0, 10);
                rep.HuongDTri.Value = qkb.First().GhiChu;

                string ngaysinh = "";
                if (!string.IsNullOrEmpty(qbn.First().NgaySinh))
                { ngaysinh += qbn.First().NgaySinh + "/"; }
                if (!string.IsNullOrEmpty(qbn.First().ThangSinh))
                { ngaysinh += qbn.First().ThangSinh + "/"; }
                if (qbn.First().NamSinh != null) { ngaysinh += qbn.First().NamSinh; }
                rep.NgaySinh.Value = ngaysinh;
            }

            string _macb = "";
            if (qkb.Count() > 0)
            {                
                _ngaykham = Convert.ToDateTime(qkb.First().NgayKham); //his 236
                rep.NgayKham.Value = qkb.First().NgayKham.ToString().Substring(0, 10);
                rep.NgayKhamBangChu.Value = " Ngày " + _ngaykham.Day + " Tháng " + _ngaykham.Month + " Năm " + _ngaykham.Year + ""; //his236
                rep.ChanDoan.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.FreshString(qkb.First().ChanDoan) : qkb.First().ChanDoan;
                rep.BenhKemTheo.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.FreshString(qkb.First().BenhKhac) : qkb.First().BenhKhac;
                if (qkb.First().MaCB != null) { _macb = qkb.First().MaCB; }
            }
            var qrv = Data.RaViens.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgayVao, p.NgayRa }).ToList();           
            if (qrv.Count > 0)
            {
                rep.NgayVV.Value = qrv.First().NgayVao;
                rep.NgayRV.Value = qrv.First().NgayRa;
            }
            if (_ngayhen != null)
            {
                if (DungChung.Bien.MaBV == "30003")
                {
                    rep.ngayHen.Value = "- Vào: " + _ngayhen.Hour + " giờ " + _ngayhen.Minute + " ngày " + _ngayhen.Day + " tháng " + _ngayhen.Month + " năm " + _ngayhen.Year;
                    rep.NgayKhamLai.Value = "-Hẹn khám lại vào ngày " + _ngayhen.Day + " tháng " + _ngayhen.Month + " năm " + _ngayhen.Year + ", mong muốn người bệnh đến khám đúng ngày hẹn, để số lượt khám / bàn khám / ngày khám theo đúng quy định thanh toán của bảo hiểm y tế. \n Giấy hẹn khám lại chỉ có giá trị sử dụng 01 lần.";
                    rep.NgayHenTYT.Value = "-Vào: " + _ngayhenTYT.Hour + " giờ " + _ngayhenTYT.Minute + " ngày " + _ngayhenTYT.Day + " tháng " + _ngayhenTYT.Month + " năm " + _ngayhenTYT.Year;
                }
                else 
                    rep.ngayHen.Value = "- Vào: " + DungChung.Ham.NgaySangChu(_ngayhen,14);
                    rep.NgayHenTYT.Value = "- Vào: " + DungChung.Ham.NgaySangChu(_ngayhenTYT,14);
                if (DungChung.Bien.MaBV == "30009") 
                {
                    rep.NgayKhamLai.Value = "   Hẹn khám lại vào ngày " + _ngayhen.Day + " tháng " + _ngayhen.Month + " năm " + _ngayhen.Year + ".";   //his236                 
                }
                else
                    rep.NgayKhamLai.Value = "   Hẹn khám lại vào " + DungChung.Ham.NgaySangChu(_ngayhen, 14) + ", hoặc đến bất kỳ thời gian nào trước ngày hẹn khám nếu có dấu hiệu (triệu chứng) bất thường. \n   Giấy hẹn khám lại chỉ có giá trị sử dụng 01 lần trong thời hạn 10 ngày làm việc, kể từ ngày hẹn khám lại./.";
            }// hoặc" + (DungChung.Bien.MaBV == "30003" ? (" có thể ") : "") + " đến bất kỳ thời gian nào trước ngày được hẹn khám lại nếu có dấu hiệu (triệu chứng) bất thường" + ((DungChung.Bien.MaBV == "30003") ? (", hoặc ngày hẹn khám trùng vào các ngày nghỉ, ngày lễ.") : "."); }
            else { rep.NgayKhamLai.Value = "Hẹn khám lại vào ..... giờ ..... ngày ..... tháng ...... năm ......, hoặc đến bất kỳ thời gian nào trước ngày hẹn khám nếu có dấu hiệu (triệu chứng) bất thường. \n   Giấy hẹn khám lại chỉ có giá trị sử dụng 01 lần trong thời hạn 10 ngày làm việc, kể từ ngày hẹn khám lại./."; }// hoặc" + ((DungChung.Bien.MaBV == "30003") ? (" có thể ") : "") + " đến bất kỳ thời gian nào trước ngày được hẹn khám lại nếu có dấu hiệu (triệu chứng) bất thường" + ((DungChung.Bien.MaBV == "30003") ? (", hoặc ngày hẹn khám trùng vào các ngày nghỉ, ngày lễ.") : "."); }
            var qbs = Data.CanBoes.Where(p => p.MaCB == _macb).Select(p => new { p.TenCB }).ToList();
            if (qbs.Count > 0) { rep.BSKB.Value = qbs.First().TenCB; }
            string _dd = "";
            if (lupDD.Text != null && lupDD.Text != "")
            {
                rep.DaiDien.Value = lupDD.Text;

            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtSoHenKham.Text == "" && DungChung.Bien.MaBV == "30007")
            {
                XtraMessageBox.Show("Xin hãy nhập số hẹn khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoHenKham.Focus();
                return;
            }
            if (lupTenCQ.Text == "" && DungChung.Bien.MaBV == "30007")
            {
                XtraMessageBox.Show("Xin hãy nhập địa chỉ hẹn khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lupTenCQ.Focus();
                return;
            }
            // lưu dữ liệu
            if (dtNgayHen.EditValue != null && dtNgayHen.EditValue.ToString() != "")
            {
                var sua = _data.BNKBs.Single(p => p.IDKB == _id);
                sua.SoHK = txtSoHenKham.Text;
                sua.GhiChu = memoGhiChu.Text;
                sua.NgayHen = dtNgayHen.DateTime;
                if (DungChung.Bien.MaBV == "30007") 
                { 
                    sua.ThoiGianTYT_HK = dtNgayHenTYT.DateTime;
                    //sua.MaKP_HenKham = Convert.ToInt32(lupTenCQ.EditValue);
                }                                 
                if (lupDD.EditValue != null)
                    sua.MaCBDaiDien = lupDD.EditValue.ToString();
                else
                    sua.MaCBDaiDien = "";
                _data.SaveChanges();
                //
                if (DungChung.Bien.MaBV != "08204")
                {
                    _InPhieu_GiayHenKhamLai_TT40_2015(_id);
                }
                else
                {
                    if (status == true)
                    {
                        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        int _mabn = 0;
                        var ngaykham = _data.BNKBs.Where(p => p.IDKB == _id).Select(p => p.NgayKham).ToList();
                        if (ngaykham.Count > 0)
                        {
                            if ((dtNgayHen.DateTime - ngaykham.First().Value.Date).Days > 0)
                            {
                                BaoCao.Rep_GiayHenKham_YS01 rep = new BaoCao.Rep_GiayHenKham_YS01();
                                frmIn frm = new frmIn();
                                var q = (from bnkb in Data.BNKBs.Where(p => p.IDKB == _id)
                                         join bn in Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                         select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.SThe, bn.GTinh, bnkb.NgayKham, bnkb.ChanDoan, bnkb.MaCB, bnkb.NgayHen, bnkb.GhiChu, bnkb.MaKP }).ToList();
                                if (q.Count > 0)
                                {
                                    _mabn = q.First().MaBNhan;
                                    rep.MaBN.Value = q.First().MaBNhan;
                                    rep.TenBN.Value = q.First().TenBNhan;
                                    rep.DiaChi.Value = q.First().DChi;
                                    rep.Tuoi.Value = q.First().Tuoi;
                                    int gioiTinh = int.Parse(q.First().GTinh.ToString());
                                    if (gioiTinh == 1)
                                    {
                                        rep.Nu.Value = "/";
                                    }
                                    else if (gioiTinh == 0)
                                    {
                                        rep.Nam.Value = "/";
                                    }
                                    if (q.First().SThe != null && q.First().SThe.Length >= 10)
                                    {
                                        rep.SThe1.Value = q.First().SThe.Substring(0, 3);
                                        rep.SThe2.Value = q.First().SThe.Substring(3, 2);
                                        rep.SThe3.Value = q.First().SThe.Substring(5, 2);
                                        rep.SThe4.Value = q.First().SThe.Substring(7, 3);
                                        rep.SThe5.Value = q.First().SThe.Substring(10, 5);
                                    }
                                    rep.NgayKy.Value = DungChung.Ham.NgaySangChu(q.First().NgayKham.Value);
                                    rep.NgayKham.Value = q.First().NgayKham;
                                    rep.ChanDoan.Value = q.First().ChanDoan;
                                    rep.NgayKhamLai.Value = q.First().NgayHen;
                                    
                                    rep.HuongDT.Value = q.First().GhiChu;
                                    rep.BSKB.Value = q.First().MaCB;
                                    rep.KPhong.Value = q.First().MaKP;
                                    var dantoc = (from tt in _data.TTboXungs.Where(p => p.MaBNhan == _mabn) join dt in _data.DanTocs on tt.MaDT equals dt.MaDT select dt).ToList();
                                    if (dantoc.Count > 0)
                                    {
                                        rep.DanToc.Value = dantoc.First().TenDT;
                                    }
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else MessageBox.Show("Không có dữ liệu");
                            }
                            else
                            {
                                MessageBox.Show("Ngày hẹn phải lớn hơn ngày khám");
                                dtNgayHen.Focus();
                            }
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập ngày hẹn tái khám");
                dtNgayHen.Focus();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            var sua = _data.BNKBs.Single(p => p.IDKB == _id);
            sua.GhiChu = "";
            sua.NgayHen = null;
            _data.SaveChanges();
            MessageBox.Show("Xóa thành công");
            this.Dispose();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}