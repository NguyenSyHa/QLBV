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
    public partial class frm_NhapKQDopplerDongMachCanh : DevExpress.XtraEditors.XtraForm
    {
        int _idCLS = -1;
        public frm_NhapKQDopplerDongMachCanh()
        {
            InitializeComponent();
        }
        public frm_NhapKQDopplerDongMachCanh(int id)
        {
            InitializeComponent();
            _idCLS = id;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public bool suacls;
        public bool kiemtra;
        public void loatthongtin(int _idCLS)
        {

            var thongtin = from cl in data.CLS.Where(p => p.IdCLS == _idCLS)
                           join cd in data.ChiDinhs on cl.IdCLS equals cd.IdCLS
                           select new { cd.MaDV, cd.IDCD, cl.MaKP, cl.MaBNhan, cl.MaCB, cl.MaCBth, cl.NgayTH, cl.MaKPth };
            int mackth = thongtin.First().MaKPth == null ? 0 : Convert.ToInt32(thongtin.First().MaKPth);         
            List<CBTH> cbth = new List<CBTH>();
            var canb = (from canbo in data.CanBoes
                        select new CBTH
                        {
                            MaCB = canbo.MaCB,
                            TenCB = canbo.TenCB,
                            MaKPsd = canbo.MaKPsd
                        }).ToList();
            foreach (var item in canb)
            {
                CBTH cb = new CBTH();
                var kpsd = item.MaKPsd.Split(';').ToList();
                for (int i = 0; i < kpsd.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(kpsd[i]) && Convert.ToInt32(kpsd[i]) == mackth)
                    {
                        cb.MaCB = item.MaCB;
                        cb.TenCB = item.TenCB;
                        cb.MaKPsd = item.MaKPsd;
                        cbth.Add(cb);
                    }
                }

            }
            lup_nguoithuchien.Properties.DataSource = cbth.Distinct().ToList();
            lup_nguoithuchien.EditValue = thongtin.First().MaCBth == null ? "" : thongtin.First().MaCBth.ToString();
            datengaythuchien.DateTime = DateTime.Now;
            if (thongtin.First().NgayTH != null)
            {
                datengaythuchien.DateTime = thongtin.First().NgayTH.Value;
            }

            var qkq = (from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) join h1 in data.CLScts on h.IDCD equals h1.IDCD join h2 in data.DichVucts on h1.MaDVct equals h2.MaDVct select new { h1.KetQua, h.KetLuan, h.LoiDan, h.MoTa, h2.STT }).FirstOrDefault();

            List<string> lkq = new List<string>();
            if (qkq != null)
            {
                if (qkq.KetQua != null)
                {
                    lkq = qkq.KetQua.Split(';').ToList();
                    if (lkq.Count > 0)
                        txt11.Text = lkq.First();
                    if (lkq.Count > 1)
                        txt12.Text = lkq.Skip(1).First();
                    if (lkq.Count > 2)
                        txt13.Text = lkq.Skip(2).First();
                    if (lkq.Count > 3)
                        txt14.Text = lkq.Skip(3).First();
                    if (lkq.Count > 4)
                        txt15.Text = lkq.Skip(4).First();
                    if (lkq.Count > 5)
                        txt16.Text = lkq.Skip(5).First();
                    if (lkq.Count > 6)
                        txt17.Text = lkq.Skip(6).First();
                    if (lkq.Count > 7)
                        txt18.Text = lkq.Skip(7).First();
                    if (lkq.Count > 8)
                        txt21.Text = lkq.Skip(8).First();
                    if (lkq.Count > 9)
                        txt22.Text = lkq.Skip(9).First();
                    if (lkq.Count > 10)
                        txt23.Text = lkq.Skip(10).First();
                    if (lkq.Count > 11)
                        txt24.Text = lkq.Skip(11).First();
                    if (lkq.Count > 12)
                        txt25.Text = lkq.Skip(12).First();
                    if (lkq.Count > 13)
                        txt26.Text = lkq.Skip(13).First();
                    if (lkq.Count > 14)
                        txt27.Text = lkq.Skip(14).First();
                    if (lkq.Count > 15)
                        txt28.Text = lkq.Skip(15).First();
                    if (lkq.Count > 16)
                        txt31.Text = lkq.Skip(16).First();
                    if (lkq.Count > 17)
                        txt32.Text = lkq.Skip(17).First();
                    if (lkq.Count > 18)
                        txt33.Text = lkq.Skip(18).First();
                    if (lkq.Count > 19)
                        txt34.Text = lkq.Skip(19).First();
                    if (lkq.Count > 20)
                        txt35.Text = lkq.Skip(20).First();
                    if (lkq.Count > 21)
                        txt36.Text = lkq.Skip(21).First();
                    if (lkq.Count > 22)
                        txt37.Text = lkq.Skip(22).First();
                    if (lkq.Count > 23)
                        txt38.Text = lkq.Skip(23).First();
                    if (lkq.Count > 24)
                        txt41.Text = lkq.Skip(24).First();
                    if (lkq.Count > 25)
                        txt42.Text = lkq.Skip(25).First();
                    if (lkq.Count > 26)
                        txt43.Text = lkq.Skip(26).First();
                    if (lkq.Count > 27)
                        txt44.Text = lkq.Skip(27).First();
                    if (lkq.Count > 28)
                        txt45.Text = lkq.Skip(28).First();
                    if (lkq.Count > 29)
                        txt46.Text = lkq.Skip(29).First();
                    if (lkq.Count > 30)
                        txt47.Text = lkq.Skip(30).First();
                    if (lkq.Count > 31)
                        txt48.Text = lkq.Skip(31).First();
                }
                List<string> lmota = new List<string>();
                if (qkq.MoTa != null)
                {
                    lmota = qkq.MoTa.Split(';').ToList();
                    if (lmota.Count > 0)
                    {
                        txtMoTa1.Text = lmota.First();
                    }
                    if (lmota.Count > 1)
                    {
                        txtMoTa2.Text = lmota.Skip(1).First();
                    }
                    if (lmota.Count > 2)
                    {
                        txtMoTa3.Text = lmota.Skip(2).First();
                    }
                    if (lmota.Count > 3)
                    {
                        txtMoTa4.Text = lmota.Skip(3).First();
                    }
                }

                txtKL.Text = qkq.KetLuan;
                txtLoiDan.Text = qkq.LoiDan;
            }


        }
        //public void hamin(int idcls)
        //{
        //    var thongtin = from cl in data.CLS.Where(p => p.IdCLS == idcls)
        //                   join cd in data.ChiDinhs on cl.IdCLS equals cd.IdCLS
        //                   select new { cd.MaDV, cd.IDCD, cl.MaKP, cl.MaBNhan,cl.NgayTH,cl.NgayThang,cd.ChiDinh1};
        //    int madichvu = 0;
        //    int makhoaphong = 0;
        //    int mabenhnhan = 0;
        //    if (thongtin.Count() > 0)
        //    {
        //        {
        //            madichvu = thongtin.First().MaDV == null ? 0 : thongtin.First().MaDV.Value;
        //            makhoaphong = thongtin.First().MaKP == null ? 0 : thongtin.First().MaKP.Value;
        //            mabenhnhan = thongtin.First().MaBNhan == null ? 0 : thongtin.First().MaBNhan.Value;

        //        }
        //    }
        //    // ngày thưc hiện và ngày chỉ định
        //    // lấy tên dịch vụ
        //    var dichvu = from dv in data.DichVus.Where(p => p.MaDV == madichvu) select new {dv.TenDV};
        //    // lấy thông tin bệnh nhân 
        //    var layttbn = from bn in data.BenhNhans join bnkb in data.BNKBs.Where(p => p.MaKP == makhoaphong && p.MaBNhan == mabenhnhan) on bn.MaBNhan equals bnkb.MaBNhan select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.Tuoi, bnkb.ChanDoan, bnkb.BenhKhac };
        //    // lấy tên khoa phòng
        //    var khoa = from h in data.KPhongs.Where(p => p.MaKP == makhoaphong) select new { h.TenKP };
        //    // tên cán bộ thực chỉ định
        //    var tencanbocd = from h in data.CLS.Where(p => p.IdCLS == _idCLS) join h1 in data.CanBoes on h.MaCB equals h1.MaCB select new { h1.TenCB };
        //    // tên cán bộ thực hiện
        //    var tencbtt = from h in data.CanBoes join h1 in data.CLS.Where(p=>p.IdCLS==_idCLS) on h.MaCB equals h1.MaCBth select new { h.TenCB };
        //    string tenkhoa = khoa.First().TenKP.ToString();
        //    var layketqua = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS)
        //                     join h in data.CLScts on cd.IDCD equals h.IDCD
        //                     join h1 in data.DichVucts on h.MaDVct equals h1.MaDVct
        //                     select new { h.MaDVct, h.KetQua, h1.STT, cd.LoiDan, cd.KetLuan }).ToList();
        //    if (layketqua.Where(p => p.STT == 1).Count() > 0)
        //    {
        //        string[] ketqua = layketqua.Where(p => p.STT == 1).First().KetQua == null ? new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" } : layketqua.Where(p => p.STT == 1).First().KetQua == "" ? new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" } : layketqua.Where(p => p.STT == 1).First().KetQua.Split(';');
        //        BaoCao.rep_phieusieuamdoppler_tim baocao = new BaoCao.rep_phieusieuamdoppler_tim();
        //        frmIn inbcao = new frmIn();
        //        for (int p = 0; p < 15; p++)
        //        {
        //            baocao.ketqua[p] = ketqua[p];
        //        }
        //        for (int k = 0; k < 5; k++)
        //        {
        //            if (layketqua.Where(p => p.STT == k + 2).Count() > 0)
        //            {

        //                baocao.ketquac[k] = layketqua.Where(p => p.STT == (k + 2)).First().KetQua;
        //            }

        //        }
        //        if (thongtin.First().NgayThang.ToString() != "")
        //        { 
        //            baocao.ngaychidinhs = daingay(thongtin.First().NgayThang.Value);
        //        }
        //        if (thongtin.First().NgayTH.ToString() != "")
        //        { 
        //            baocao.ngaythuchiens = daingay(thongtin.First().NgayTH.Value);
        //        }
        //        baocao.yeucaus = string.Format("" + dichvu.First().TenDV.ToString() + "({0})", thongtin.First().ChiDinh1 == null ? "" : thongtin.First().ChiDinh1.ToString());
        //        baocao.BSCD = tencanbocd.First().TenCB==null?"":tencanbocd.First().TenCB.ToString();
        //        baocao.CBTT = tencbtt.First().TenCB == null ? "" : tencbtt.First().TenCB.ToString();
        //        baocao.loirans = layketqua.First().LoiDan;
        //        baocao.ketluans = layketqua.First().KetLuan;
        //        baocao.hotes = layttbn.First().TenBNhan.ToString();
        //        baocao.dichis = layttbn.First().DChi.ToString();
        //        baocao.tuois = layttbn.First().Tuoi.ToString();
        //        baocao.chuandoans = layttbn.First().ChanDoan.ToString();
        //        baocao.khoas = tenkhoa;
        //        baocao.hamloatphieu();
        //        baocao.CreateDocument();
        //        inbcao.prcIN.PrintingSystem = baocao.PrintingSystem;
        //        inbcao.ShowDialog();

        //    }
        //}
        public string daingay(DateTime a)
        {
            if (a != null)
            {
                string k = string.Format("Ngày {0} tháng {1} năm {2}", a.Day, a.Month, a.Year);
                return k;
            }
            return "Ngày... tháng...năm...";
        }
        public void kiemtrakequa()
        {
            var status = from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select new { h.Status };
            if (status.Count() > 0 && status.First().Status == 0)
            {
                kiemtra = true;
            }
            else
            {

                kiemtra = false;
            }
        }


        private void frm_NhapKQDopplerDongMachCanh_Load(object sender, EventArgs e)
        {
            kiemtrakequa();
            loatthongtin(_idCLS);
            EnableControl(kiemtra);

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            inphieuDL(_idCLS);

        }

        private void inphieuDL(int idcls)
        {
            frmIn frmcd = new frmIn();
            BaoCao.rep_PhieuDopplerDongMachCanh repcd = new BaoCao.rep_PhieuDopplerDongMachCanh(idcls);
            repcd.BindingData();
            repcd.CreateDocument();
            frmcd.prcIN.PrintingSystem = repcd.PrintingSystem;
            frmcd.ShowDialog();
        }
        private void EnableControl(bool t)
        {
            txt11.Properties.ReadOnly = !t;
            txt12.Properties.ReadOnly = !t;
            txt13.Properties.ReadOnly = !t;
            txt14.Properties.ReadOnly = !t;
            txt15.Properties.ReadOnly = !t;
            txt16.Properties.ReadOnly = !t;
            txt17.Properties.ReadOnly = !t;
            txt18.Properties.ReadOnly = !t;

            txt21.Properties.ReadOnly = !t;
            txt22.Properties.ReadOnly = !t;
            txt23.Properties.ReadOnly = !t;
            txt24.Properties.ReadOnly = !t;
            txt25.Properties.ReadOnly = !t;
            txt26.Properties.ReadOnly = !t;
            txt27.Properties.ReadOnly = !t;
            txt28.Properties.ReadOnly = !t;

            txt31.Properties.ReadOnly = !t;
            txt32.Properties.ReadOnly = !t;
            txt33.Properties.ReadOnly = !t;
            txt34.Properties.ReadOnly = !t;
            txt35.Properties.ReadOnly = !t;
            txt36.Properties.ReadOnly = !t;
            txt37.Properties.ReadOnly = !t;
            txt38.Properties.ReadOnly = !t;

            txt41.Properties.ReadOnly = !t;
            txt42.Properties.ReadOnly = !t;
            txt43.Properties.ReadOnly = !t;
            txt44.Properties.ReadOnly = !t;
            txt45.Properties.ReadOnly = !t;
            txt46.Properties.ReadOnly = !t;
            txt47.Properties.ReadOnly = !t;
            txt48.Properties.ReadOnly = !t;

            txtMoTa1.Properties.ReadOnly = !t;
            txtMoTa2.Properties.ReadOnly = !t;
            txtMoTa3.Properties.ReadOnly = !t;
            txtMoTa4.Properties.ReadOnly = !t;
            txtKL.Properties.ReadOnly = !t;
            txtLoiDan.Properties.ReadOnly = !t;
            sua.Enabled = !t;
            xoa.Enabled = !t;
            inphieu.Enabled = !t;
            luu.Enabled = t;
        }
        private void sua_Click(object sender, EventArgs e)
        {
            if (kiemtra == false)
            {
                EnableControl(true);
            }
            return;

        }
        string kqua;
        private void luu_Click(object sender, EventArgs e)
        {
            var cls = data.CLS.Single(p => p.IdCLS == _idCLS);

            if (datengaythuchien.DateTime == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày thực hiện");
                return;
            }
            else if (datengaythuchien.DateTime > DateTime.Now)
            {
                MessageBox.Show("Thời gian thực hiện phải nhỏ hơn hoặc bằng thời điểm hiện tại");
                return;
            }
            else if (datengaythuchien.DateTime < cls.NgayThang)
            {
                MessageBox.Show("Thời gian thực hiện phải lớn hơn ngày chỉ định");
                return;
            }
            else if (lup_nguoithuchien.EditValue == null)
            {
                MessageBox.Show("Chưa chọn cán bộ thực hiện");
                return;
            }
            else
            {
                var loid = (from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select h).ToList();

                foreach (var a in loid)
                {
                    a.KetLuan = txtKL.Text.Trim();
                    a.LoiDan = txtLoiDan.Text.Trim();

                    string mota = "";
                    if (String.IsNullOrEmpty(txtMoTa1.Text))
                        mota = mota + ";";
                    else
                        mota = txtMoTa1.Text + ";";

                    if (String.IsNullOrEmpty(txtMoTa2.Text))
                        mota = mota + ";";
                    else
                        mota = mota + txtMoTa2.Text + ";";

                    if (String.IsNullOrEmpty(txtMoTa3.Text))
                        mota = mota + ";";
                    else
                        mota = mota + txtMoTa3.Text + ";";
                    if (String.IsNullOrEmpty(txtMoTa4.Text))
                        mota = mota + ";";
                    else
                        mota = mota + txtMoTa4.Text + ";";

                    a.MoTa = mota;
                    a.Status = 1;
                    a.NgayTH = datengaythuchien.DateTime;

                    data.SaveChanges();
                }

                cls.Status = 1;
                cls.MaCBth = lup_nguoithuchien.EditValue == null ? "" : lup_nguoithuchien.EditValue.ToString();
                if (datengaythuchien != null)
                {
                    cls.NgayTH = datengaythuchien.DateTime;
                }

                data.SaveChanges();
                var qclsct = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) join clsct in data.CLScts on cd.IDCD equals clsct.IDCD select clsct).FirstOrDefault();
                if (qclsct != null)
                {
                    string kq1 = "", kq2 = "", kq3 = "", kq4 = "", kq5 = "", kq6 = "", kq7 = "", kq8 = "", kq9 = "", kq10 = "", kq11 = "", kq12 = "", kq13 = "", kq14 = "", kq15 = "", kq16 = "", kq17 = "", kq18 = "", kq19 = "", kq20 = "", kq21 = "", kq22 = "", kq23 = "", kq24 = "", kq25 = "", kq26 = "", kq27 = "", kq28 = "", kq29 = "", kq30 = "", kq31 = "", kq32 = "";

                    if (!String.IsNullOrEmpty(txt11.Text))
                        kq1 = txt11.Text;
                    if (!String.IsNullOrEmpty(txt12.Text))
                        kq2 = txt12.Text;
                    if (!String.IsNullOrEmpty(txt13.Text))
                        kq3 = txt13.Text;
                    if (!String.IsNullOrEmpty(txt14.Text))
                        kq4 = txt14.Text;
                    if (!String.IsNullOrEmpty(txt15.Text))
                        kq5 = txt15.Text;
                    if (!String.IsNullOrEmpty(txt16.Text))
                        kq6 = txt16.Text;
                    if (!String.IsNullOrEmpty(txt17.Text))
                        kq7 = txt17.Text;
                    if (!String.IsNullOrEmpty(txt18.Text))
                        kq8 = txt18.Text;

                    if (!String.IsNullOrEmpty(txt21.Text))
                        kq9 = txt21.Text;
                    if (!String.IsNullOrEmpty(txt22.Text))
                        kq10 = txt22.Text;
                    if (!String.IsNullOrEmpty(txt23.Text))
                        kq11 = txt23.Text;
                    if (!String.IsNullOrEmpty(txt24.Text))
                        kq12 = txt24.Text;
                    if (!String.IsNullOrEmpty(txt25.Text))
                        kq13 = txt25.Text;
                    if (!String.IsNullOrEmpty(txt26.Text))
                        kq14 = txt26.Text;
                    if (!String.IsNullOrEmpty(txt27.Text))
                        kq15 = txt27.Text;
                    if (!String.IsNullOrEmpty(txt28.Text))
                        kq16 = txt28.Text;

                    if (!String.IsNullOrEmpty(txt31.Text))
                        kq17 = txt31.Text;
                    if (!String.IsNullOrEmpty(txt32.Text))
                        kq18 = txt32.Text;
                    if (!String.IsNullOrEmpty(txt33.Text))
                        kq19 = txt33.Text;
                    if (!String.IsNullOrEmpty(txt34.Text))
                        kq20 = txt34.Text;
                    if (!String.IsNullOrEmpty(txt35.Text))
                        kq21 = txt35.Text;
                    if (!String.IsNullOrEmpty(txt36.Text))
                        kq22 = txt36.Text;
                    if (!String.IsNullOrEmpty(txt37.Text))
                        kq23 = txt37.Text;
                    if (!String.IsNullOrEmpty(txt38.Text))
                        kq24 = txt38.Text;

                    if (!String.IsNullOrEmpty(txt41.Text))
                        kq25 = txt41.Text;
                    if (!String.IsNullOrEmpty(txt42.Text))
                        kq26 = txt42.Text;
                    if (!String.IsNullOrEmpty(txt43.Text))
                        kq27 = txt43.Text;
                    if (!String.IsNullOrEmpty(txt44.Text))
                        kq28 = txt44.Text;
                    if (!String.IsNullOrEmpty(txt45.Text))
                        kq29 = txt45.Text;
                    if (!String.IsNullOrEmpty(txt46.Text))
                        kq30 = txt46.Text;
                    if (!String.IsNullOrEmpty(txt47.Text))
                        kq31 = txt47.Text;
                    if (!String.IsNullOrEmpty(txt48.Text))
                        kq32 = txt48.Text;

                    string ketqua = kq1 + ";" + kq2 + ";" + kq3 + ";" + kq4 + ";" + kq5 + ";" + kq6 + ";" + kq7 + ";" + kq8 + ";" + kq9 + ";" + kq10 + ";" + kq11 + ";" + kq12 + ";" + kq13 + ";" + kq14 + ";" + kq15 + ";" + kq16 + ";" + kq17 + ";" + kq18 + ";" + kq19 + ";" + kq20 + ";" + kq21 + ";" + kq22 + ";" + kq23 + ";" + kq24 + ";" + kq25 + ";" + kq26 + ";" + kq27 + ";" + kq28 + ";" + kq29 + ";" + kq30 + ";" + kq31 + ";" + kq32;

                    qclsct.KetQua = ketqua;
                    data.SaveChanges();


                    int iddthuoc = 0;
                    //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                    int _idkb = 0;
                    int mabn = 0; int makp = 0;
                    if (cls.MaBNhan != null)
                        mabn = cls.MaBNhan.Value;
                    if (cls.MaKP != null)
                        makp = cls.MaKP.Value;
                    var bnkb = data.BNKBs.Where(p => p.MaBNhan == mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                    if (bnkb.Count > 0)
                        _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                    var ktdthuoc = data.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 2).ToList();
                    if (ktdthuoc.Count > 0)
                        iddthuoc = ktdthuoc.First().IDDon;
                    var cdinh = (from cd1 in data.ChiDinhs.Where(p => p.IdCLS == _idCLS && p.Status == 1)
                                 join dv in data.DichVus on cd1.MaDV equals dv.MaDV
                                 select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, cd1.IDCD, dv.DonVi, cd1.TrongBH, cd1.XHH, cd1.LoaiDV }).ToList();
                    if (iddthuoc > 0)
                    {
                        foreach (var cd2 in cdinh)
                        {
                            var kt = (from dt in data.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                            if (kt.Count <= 0)
                            {
                                double _dongia = DungChung.Ham._getGiaDM(data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, mabn, datengaythuchien.DateTime);
                                DThuocct moi = new DThuocct();
                                moi.MaDV = cd2.MaDV;
                                moi.IDKB = _idkb;
                                moi.IDDon = iddthuoc;
                                moi.DonVi = cd2.DonVi;
                                moi.TrongBH = cd2.TrongBH ?? 0;
                                moi.IDCD = cd2.IDCD;
                                moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.XHH = cd2.XHH;
                                moi.LoaiDV = cd2.LoaiDV;
                                if (lup_nguoithuchien.EditValue != null)
                                    moi.MaCB = lup_nguoithuchien.EditValue.ToString();
                                else
                                    moi.MaCB = "";
                                moi.MaKP = makp;
                                moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.NgayNhap = datengaythuchien.DateTime;
                                moi.SoLuong = 1;
                                moi.Status = 0;
                                if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = _idCLS;
                                data.DThuoccts.Add(moi);
                                data.SaveChanges();
                                var CheckGiaPhuThu = data.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                                var sss = data.BenhNhans.Where(p => p.MaBNhan == mabn).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(data, moi.IDDonct, s);
                                }
                            }
                            else
                            {
                                foreach (var dt in kt)
                                {
                                    dt.NgayNhap = datengaythuchien.DateTime;
                                    dt.IDCLS = _idCLS;
                                }
                                data.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        DThuoc dthuoccd = new DThuoc();
                        dthuoccd.NgayKe = datengaythuchien.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                        dthuoccd.MaBNhan = mabn;
                        dthuoccd.MaKP = cls.MaKP;
                        dthuoccd.MaCB = cls.MaCB;
                        dthuoccd.PLDV = 2;
                        dthuoccd.KieuDon = -1;
                        data.DThuocs.Add(dthuoccd);
                        if (data.SaveChanges() >= 0)
                        {
                            int maxid = dthuoccd.IDDon;
                            foreach (var cd3 in cdinh)
                            {
                                double _dongia = DungChung.Ham._getGiaDM(data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, mabn, datengaythuchien.DateTime);
                                DThuocct moi = new DThuocct();
                                moi.MaDV = cd3.MaDV;
                                moi.IDDon = maxid;
                                moi.IDKB = _idkb;
                                moi.TrongBH = cd3.TrongBH ?? 0;
                                if (lup_nguoithuchien.EditValue != null)
                                    moi.MaCB = lup_nguoithuchien.EditValue.ToString();
                                else
                                    moi.MaCB = "";
                                moi.NgayNhap = datengaythuchien.DateTime;
                                moi.MaKP = makp;
                                moi.IDCD = cd3.IDCD;
                                moi.DonVi = cd3.DonVi;
                                moi.XHH = cd3.XHH;
                                moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                                moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                                moi.SoLuong = 1;
                                moi.Status = 0;
                                moi.LoaiDV = cd3.LoaiDV;
                                if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = _idCLS;
                                data.DThuoccts.Add(moi);
                                data.SaveChanges();
                                var CheckGiaPhuThu = data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                                var sss = data.BenhNhans.Where(p => p.MaBNhan == mabn).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(data, moi.IDDonct, s);
                                }
                            }
                        }
                    }
                }
                EnableControl(false);
                MessageBox.Show("lưu thành công", "Thông báo");
                frm_NhapKQDopplerDongMachCanh_Load(sender, e);
            }
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            //DialogResult _result = MessageBox.Show("Banj muon xoa du lieu", "Hoir xoa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MessageBox.Show("Bạn muốn xóa dữ liệu", "Hỏi xóa", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                var qclsct = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS)
                              join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                              select clsct).FirstOrDefault();

                if (qclsct != null)
                {
                    qclsct.KetQua = "";
                    qclsct.Status = 0;
                    data.SaveChanges();
                }
              
                var ck = (from nhom in data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                          join dvu in data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                          select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).Select(p=>p.MaDV).ToList();
               
                var loid = (from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select h).ToList();
                foreach (var a in loid)
                {


                    int ID = a.IDCD;
                    var iddt = data.DThuoccts.Where(p => p.IDCD == ID && (ck.Count == 0 || !ck.Contains(p.MaDV ??0) )).ToList();
                    if (iddt.Count > 0)
                    {
                        foreach (var item in iddt)
                        {
                            int iddtct = item.IDDonct;
                            var ktchiphi = data.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                            if (ktchiphi.Count > 0)
                            {
                                MessageBox.Show("dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                return;
                            }
                            var xoa = data.DThuoccts.Single(p => p.IDDonct == iddtct);
                            data.DThuoccts.Remove(xoa);
                            data.SaveChanges();
                        }
                    }
                    a.NgayTH = null;
                    a.KetLuan = "";
                    a.LoiDan = "";
                    a.MoTa = "";
                    a.Status = 0;
                    data.SaveChanges();
                }
                var cls = data.CLS.Single(p => p.IdCLS == _idCLS);
                cls.Status = 0;
                cls.MaCBth = "";
                cls.NgayTH = null;
                data.SaveChanges();
                //cls :MACBth; Status
                MessageBox.Show("Xóa Thành công", "Thông báo");
                EnableControl(false);
                frm_NhapKQDopplerDongMachCanh_Load(sender, e);
                txtKL.Text = "";
                txtLoiDan.Text = "";
                txtMoTa1.Text = "";
                txtMoTa2.Text = "";
                txtMoTa3.Text = "";
                txtMoTa4.Text = "";
                txt11.Text = "";
                txt12.Text = "";
                txt13.Text = "";
                txt14.Text = "";
                txt15.Text = "";
                txt16.Text = "";
                txt17.Text = "";
                txt18.Text = "";

                txt21.Text = "";
                txt22.Text = "";
                txt23.Text = "";
                txt24.Text = "";
                txt25.Text = "";
                txt26.Text = "";
                txt27.Text = "";
                txt28.Text = "";

                txt31.Text = "";
                txt32.Text = "";
                txt33.Text = "";
                txt34.Text = "";
                txt35.Text = "";
                txt36.Text = "";
                txt37.Text = "";
                txt38.Text = "";

                txt41.Text = "";
                txt42.Text = "";
                txt43.Text = "";
                txt44.Text = "";
                txt45.Text = "";
                txt46.Text = "";
                txt47.Text = "";
                txt48.Text = "";

            }
        }

        private void frm_NhapKQDopplerDongMachCanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (luu.Enabled == true)
                {
                    if (MessageBox.Show("Bạn muốn thoát from", "Thông báo", MessageBoxButtons.YesNo).ToString() == "Yes")
                    {
                        this.Dispose();
                    }
                }
                else
                {


                    this.Dispose();

                }
            }
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            txt11.Text = "59";
            txt21.Text = "43";
            txt31.Text = "49";
            txt41.Text = "42";

            txt12.Text = "19";
            txt22.Text = "14";
            txt32.Text = "15";
            txt42.Text = "12";

            txt13.Text = "0.68";
            txt23.Text = "0.67";
            txt33.Text = "0.70";
            txt43.Text = "0.50";

            txt14.Text = "";
            txt24.Text = "";
            txt34.Text = "";
            txt44.Text = "";

            txt15.Text = "65";
            txt25.Text = "43";
            txt35.Text = "57";
            txt45.Text = "38";

            txt16.Text = "18";
            txt26.Text = "14";
            txt36.Text = "18";
            txt46.Text = "15";

            txt17.Text = "0.72";
            txt27.Text = "0.67";
            txt37.Text = "0.68";
            txt47.Text = "0.45";

            txt18.Text = "";
            txt28.Text = "";
            txt38.Text = "";
            txt48.Text = "";

            
            txtMoTa1.Text = "-  ĐK ĐM cảnh gốc trái: bình thường . Thành ĐM đều đặn. \r\n" +
                            "-  Đường kính ĐM cảnh gốc phải: bình thường  Thành ĐM đều đặn.\r\n" +
                            "-  Phổ Doppler và dòng chảy màu bình thường.";
            txtMoTa2.Text = "-  Đường kính ĐM cảnh trong trái: bình thường  Thành ĐM đều đặn. \r\n" +
                            "-  ĐK ĐM cảnh trong phải: bình thường . Thành ĐM đều đặn.\r\n" +
                            "-  Phổ Doppler và dòng chảy màu bình thường.";
            txtMoTa3.Text = "-  Đường kính ĐM cảnh ngoài trái: bình thường . Thành ĐM đều đặn.  \r\n" +
                            "-  Đường kính ĐM cảnh ngoài phải: bình thường  .Thành ĐM đều đặn.\r\n" +
                            "-  Phổ Doppler và dòng chảy màu bình thường.";
            txtMoTa4.Text = "";
        }

    }


}