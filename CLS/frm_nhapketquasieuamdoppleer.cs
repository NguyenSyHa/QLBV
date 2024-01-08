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
    public partial class frm_nhapketquasieuamdoppleer : DevExpress.XtraEditors.XtraForm
    {
        int _idCLS = -1;
        Action reload;
        BindingSource bind = new BindingSource();
        public frm_nhapketquasieuamdoppleer()
        {
            InitializeComponent();
        }
        public frm_nhapketquasieuamdoppleer(int id, Action _reload)
        {
            InitializeComponent();
            _idCLS = id;
            this.reload = _reload;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public bool suacls;
        public bool kiemtra;
        public void loatthongtin(int _idCLS)
        {
            int madichvu = 0;
            int makhoaphong = 0;
            int mabenhnhan = 0;
            var thongtin = from cl in data.CLS.Where(p => p.IdCLS == _idCLS)
                           join cd in data.ChiDinhs on cl.IdCLS equals cd.IdCLS
                           select new { cd.MaDV, cd.IDCD, cl.MaKP, cl.MaBNhan, cl.MaCB, cl.MaCBth, cl.NgayTH, cl.MaKPth };
            int mackth = thongtin.First().MaKPth == null ? 0 : Convert.ToInt32(thongtin.First().MaKPth);
            List<CBTH> cbth = new List<CBTH>();
            var canb = (from canbo in data.CanBoes
                        select new CBTH
                        { MaCB = canbo.MaCB,
                        TenCB = canbo.TenCB,
                        MaKPsd = canbo.MaKPsd
                        }).ToList();
            foreach(var item in canb)
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

            
            loup_nguoithuchien.Properties.DataSource = cbth.Distinct().ToList();
            if (thongtin.Count() > 0)
            {
                {
                    madichvu = thongtin.First().MaDV == null ? 0 : Convert.ToInt32(thongtin.First().MaDV);
                    makhoaphong = thongtin.First().MaKP == null ? 0 : Convert.ToInt32(thongtin.First().MaKP);
                    mabenhnhan = thongtin.First().MaBNhan == null ? 0 : Convert.ToInt32(thongtin.First().MaBNhan);
                }
            }
            var khoa = from h in data.KPhongs.Where(p => p.MaKP == makhoaphong) select new { h.TenKP };
            string tenkhoa = khoa.First().TenKP.ToString();
            if (kiemtra == false)
            {
                var lkq = (from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) join h1 in data.CLScts on h.IDCD equals h1.IDCD join h2 in data.DichVucts on h1.MaDVct equals h2.MaDVct select new { h1.KetQua, h.KetLuan, h.LoiDan, h2.STT }).ToList(); ;
                string[] lkqcls = new string[7];
                for (int so = 0; so < lkq.Count(); so++)
                {
                    int a1 = so + 2;
                    if (lkq.Where(p => p.STT == a1).Count() > 0)
                    {
                        lkqcls[so] = lkq.Where(p => p.STT == a1).First() == null ? "" : lkq.Where(p => p.STT == a1).First().KetQua;
                    }
                    else
                    {
                        lkqcls[so] = "";
                    }
                }
                Txt1.Text = lkqcls[0];
                txt2.Text = lkqcls[1];
                txt3.Text = lkqcls[2];
                txt4.Text = lkqcls[3];
                txt5.Text = lkqcls[4];
                txt6.Text = lkq.First().KetLuan;
                txt7.Text = lkq.First().LoiDan;
                loup_nguoithuchien.EditValue = thongtin.First().MaCBth == null ? "" : thongtin.First().MaCBth.ToString();
                if (thongtin.First().NgayTH != null)
                {
                    datengaythuchien.DateTime = thongtin.First().NgayTH.Value;
                }
                status.Text = string.Format("Trang thái: {0}", "Đã làm");

            }
            else
            {

                var dichvuct = (from h in data.DichVucts.Where(p => p.MaDV == madichvu) select new { h.MaDVct, h.TSBT, h.TenDVct, h.STT }).ToList();
                string[] a = new string[6];

                for (int so = 0; so < dichvuct.Count(); so++)
                {
                    int a1 = so + 2;
                    if (dichvuct.Where(p => p.STT == a1).Count() > 0)
                    {
                        a[so] = dichvuct.Where(p => p.STT == a1).First().TSBT == null ? "" : dichvuct.Where(p => p.STT == a1).First().TSBT;
                    }
                    else
                    {
                        a[so] = "";
                    }
                }
                Txt1.Text = a[0];
                txt2.Text = a[1];
                txt3.Text = a[2];
                txt4.Text = a[3];
                txt5.Text = a[4];
                datengaythuchien.DateTime = DateTime.Now;
                status.Text = string.Format("Trang thái: {0}", "Chưa làm");

            }
            var layttbn = from bn in data.BenhNhans join bnkb in data.BNKBs.Where(p => p.MaKP == makhoaphong && p.MaBNhan == mabenhnhan) on bn.MaBNhan equals bnkb.MaBNhan select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.Tuoi, bnkb.ChanDoan, bnkb.BenhKhac };
            if (layttbn.Count() > 0)
            {
                txthoten.Text = string.Format("Họ Tên: {0}", layttbn.First().TenBNhan);
                txtma.Text = string.Format("Mã: {0}", layttbn.First().MaBNhan);
                txtdiachi.Text = string.Format("Địa chỉ: {0}", layttbn.First().DChi);
                txttuoi.Text = string.Format("Tuổi: {0}", layttbn.First().Tuoi.ToString());

            }
            var layketqua = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS)
                             join h in data.CLScts on cd.IDCD equals h.IDCD
                             join h1 in data.DichVucts on h.MaDVct equals h1.MaDVct
                             select new { h.MaDVct, h.KetQua, h1.STT }).ToList();
            List<chiso> them = new List<chiso>();
            if (layketqua.Where(p => p.STT == 1).Count() > 0)
            {
                string[] ketqua = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                if (!string.IsNullOrEmpty(layketqua.Where(p => p.STT == 1).First().KetQua))
                {
                    string[] trunggian = layketqua.Where(p => p.STT == 1).First().KetQua.Split(';');
                    for (int i = 0; i < trunggian.Length; i++)
                    {
                        ketqua[i] = trunggian[i];
                    }
                }

                if (ketqua.Count() > 0)
                {
                    var layketquacs = (from h in ketqua
                                       select new
                                       {
                                           so1s = ketqua[0],
                                           so2s = ketqua[1],
                                           so3s = ketqua[2],
                                           so4s = ketqua[3],
                                           so5s = ketqua[4],
                                           so6s = ketqua[5],
                                           so7s = ketqua[6],
                                           so8s = ketqua[7],
                                           so9s = ketqua[8],
                                           so10s = ketqua[9],
                                           so11s = ketqua[10],
                                           so12s = ketqua[11],
                                           so13s = ketqua[12],
                                           so14s = ketqua[13],
                                           so15s = ketqua[14],
                                           so16s = ketqua[15],
                                           so17s = ketqua[16],
                                           so18s = ketqua[17],
                                           so19s = ketqua[18],
                                           so20s = ketqua[19],
                                       }).ToList();
                    Txt1.Text = layketquacs.First().so16s;
                    txt2.Text = layketquacs.First().so17s;
                    txt3.Text = layketquacs.First().so18s;
                    txt4.Text = layketquacs.First().so19s;
                    txt5.Text = layketquacs.First().so20s;
                    chiso cs = new chiso();
                    foreach (var th in layketquacs)
                    {
                        cs.so1 = th.so1s;
                        cs.so2 = th.so2s;
                        cs.so3 = th.so3s;
                        cs.so4 = th.so4s;
                        cs.so5 = th.so5s;
                        cs.so6 = th.so6s;
                        cs.so7 = th.so7s;
                        cs.so8 = th.so8s;
                        cs.so9 = th.so9s;
                        cs.so10 = th.so10s;
                        cs.so11 = th.so11s;
                        cs.so12 = th.so12s;
                        cs.so13 = th.so13s;
                        cs.so14 = th.so14s;
                        cs.so15 = th.so15s;
                    }
                    them.Add(cs);
                }
            }
            else
                them.Add(new chiso());
            bind.DataSource = them;
            griviketqua.DataSource = bind;
        }
        public void hamin(int idcls, int mau)
        {
            var thongtin = from cl in data.CLS.Where(p => p.IdCLS == idcls)
                           join cd in data.ChiDinhs on cl.IdCLS equals cd.IdCLS
                           select new { cd.MaDV, cd.IDCD, cl.MaKP, cl.MaKPth, cl.MaBNhan, cl.NgayTH, cl.NgayThang, cd.ChiDinh1 };
            int madichvu = 0;
            int makhoaphong = 0;
            int makhoaphongTH = 0;
            int mabenhnhan = 0;
            if (thongtin.Count() > 0)
            {
                {
                    madichvu = thongtin.First().MaDV == null ? 0 : thongtin.First().MaDV.Value;
                    makhoaphong = thongtin.First().MaKP == null ? 0 : thongtin.First().MaKP.Value;
                    makhoaphongTH = thongtin.First().MaKP == null ? 0 : thongtin.First().MaKPth.Value;
                    mabenhnhan = thongtin.First().MaBNhan == null ? 0 : thongtin.First().MaBNhan.Value;

                }
            }
            // ngày thưc hiện và ngày chỉ định
            // lấy tên dịch vụ
            var dichvu = from dv in data.DichVus.Where(p => p.MaDV == madichvu)
                         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         select new { dv.TenDV, tn.TenRG };
            // lấy thông tin bệnh nhân 
            var layttbn = from bn in data.BenhNhans join bnkb in data.BNKBs.Where(p => p.MaKP == makhoaphong && p.MaBNhan == mabenhnhan) on bn.MaBNhan equals bnkb.MaBNhan select new {bn.SThe, bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.Tuoi, bnkb.ChanDoan, bnkb.BenhKhac, GTinh = bn.GTinh == 0 ? "Nữ" : "Nam" };
            // lấy tên khoa phòng
            var khoa = from h in data.KPhongs.Where(p => p.MaKP == makhoaphong) select new { h.TenKP };
            // lấy tên khoa phòng thực hiện
            var NhomkhoaTH = data.KPhongs.Where(p => p.MaKP == makhoaphongTH).Select(p => p.NhomKP).First();
            var khoaTH = (from h in data.KPhongs.Where(p => p.MaKP == NhomkhoaTH) select new { h.TenKP }).ToList();
            // tên cán bộ thực chỉ định
            var tencanbocd = (from h in data.CLS.Where(p => p.IdCLS == _idCLS) join h1 in data.CanBoes on h.MaCB equals h1.MaCB select new { h1.TenCB, h.NgayThang}).ToList(); ;
            // tên cán bộ thực hiện
            var tencbtt = (from h in data.CanBoes join h1 in data.CLS.Where(p => p.IdCLS == _idCLS) on h.MaCB equals h1.MaCBth select new { h.TenCB, h.ChucVu }).ToList();
            string tenkhoa = khoa.First().TenKP.ToString();
            string tenkhoaTH = khoaTH.Count > 0? khoaTH.First().TenKP.ToString() : "";
            var layketqua = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS)
                             join h in data.CLScts on cd.IDCD equals h.IDCD
                             join h1 in data.DichVucts on h.MaDVct equals h1.MaDVct
                             select new { cd.MaDV, h.MaDVct, h.KetQua, h1.STT, cd.LoiDan, cd.KetLuan }).ToList();
            if (layketqua.Where(p => p.STT == 1).Count() > 0)
            {
                string[] ketqua = layketqua.Where(p => p.STT == 1).First().KetQua == null ? new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" } : layketqua.Where(p => p.STT == 1).First().KetQua == "" ? new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" } : layketqua.Where(p => p.STT == 1).First().KetQua.Split(';');
                if (mau == 1)
                {
                    BaoCao.rep_phieusieuamdoppler_tim_27001 baocao = new BaoCao.rep_phieusieuamdoppler_tim_27001();
                    frmIn inbcao = new frmIn();
                    for (int p = 0; p < 15; p++)
                    {

                        baocao.ketqua[p] = ketqua[p];
                    }
                    for (int k = 0; k < 5; k++)
                    {
                        string[] arr = ketqua[k + 15].Split('$');
                        baocao.ketquac[k] = arr[0];
                        if (arr.Length > 1)
                            baocao.ketquac[k + 10] = arr[1];

                    }
                    if (thongtin.First().NgayThang.ToString() != "")
                    {
                        baocao.ngaychidinhs = DungChung.Ham.NgaySangChu(thongtin.First().NgayThang.Value);
                    }
                    baocao.ngaythuchiens = thongtin.First().NgayTH.ToString() != "" ? DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(thongtin.First().NgayTH.Value) : DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now);
                    if (dichvu.First().TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)
                        baocao.txtTieuDe.Text = "PHIẾU SIÊU ÂM TIM";
                    baocao.yeucaus = string.Format("" + dichvu.First().TenDV.ToString() + "({0})", thongtin.First().ChiDinh1 == null ? "" : thongtin.First().ChiDinh1.ToString());
                    baocao.TENcbtt.Text = tencbtt.Count() > 0 ? "" : tencbtt.First().TenCB.ToString();
                    baocao.CBTT = tencbtt.First().TenCB == null ? "" : tencbtt.First().TenCB.ToString();
                    baocao.loirans = layketqua.First().LoiDan;
                    baocao.ketluans = layketqua.First().KetLuan;
                    baocao.hotes = layttbn.First().TenBNhan.ToString();
                    baocao.dichis = layttbn.First().DChi.ToString();
                    baocao.tuois = layttbn.First().Tuoi.ToString();
                    baocao.gioitinhs = layttbn.First().GTinh.ToString();
                    baocao.thebhyts = layttbn.First().SThe.ToString();
                    baocao.BSCD = tencanbocd.First().TenCB.ToString();
                    baocao.chuandoans = layttbn.First().ChanDoan.ToString();
                    baocao.khoas = tenkhoa;
                    baocao.khoaThucHiens = tenkhoaTH.ToUpper();
                    baocao.hamloatphieu();
                    baocao.CreateDocument();
                    inbcao.prcIN.PrintingSystem = baocao.PrintingSystem;
                    inbcao.ShowDialog();
                }
                else
                {
                    if (DungChung.Bien.MaBV == "01830")
                    {
                        BaoCao.rep_phieusieuamdoppler_tim_01830 baocao = new BaoCao.rep_phieusieuamdoppler_tim_01830();
                        frmIn inbcao = new frmIn();
                        for (int p = 0; p < 15; p++)
                        {
                            baocao.ketqua[p] = ketqua[p];
                        }
                        for (int k = 0; k < 5; k++)
                        {
                            string[] arr = ketqua[k + 15].Split('$');
                            baocao.ketquac[k] = arr[0];
                            if (arr.Length > 1)
                                baocao.ketquac[k + 10] = arr[1];
                        }
                        if (thongtin.First().NgayThang.ToString() != "")
                        {
                            baocao.ngaychidinhs = DungChung.Ham.NgaySangChu(thongtin.First().NgayThang.Value);
                        }
                        baocao.ngaythuchiens = thongtin.First().NgayTH.ToString() != "" ? DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(thongtin.First().NgayTH.Value) : DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now);
                        string cd1 = (from a in layketqua
                                      join b in data.DichVus on a.MaDV equals b.MaDV
                                      select new { b.TenDV }).ToList().FirstOrDefault().TenDV;
                        baocao.yeucaus = string.Format("" + dichvu.First().TenDV.ToString() + "({0})", thongtin.First().ChiDinh1 == null ? "" : thongtin.First().ChiDinh1.ToString());
                        baocao.BSCD = tencanbocd.First().TenCB == null ? "" : tencanbocd.First().TenCB.ToString();
                        baocao.CBTT = tencbtt.First().TenCB == null ? "" : tencbtt.First().ChucVu.ToString() + tencbtt.First().TenCB.ToString();
                        baocao.loirans = layketqua.First().LoiDan;
                        baocao.ketluans = layketqua.First().KetLuan;
                        baocao.hotes = layttbn.First().TenBNhan.ToString();
                        baocao.dichis = layttbn.First().DChi.ToString();
                        baocao.tuois = layttbn.First().Tuoi.ToString();
                        baocao.chuandoans = layttbn.First().ChanDoan.ToString();
                        baocao.khoas = tenkhoa;
                        baocao.chidinh = cd1;
                        baocao.gtinh = layttbn.First().GTinh;
                        baocao.hamloatphieu();
                        baocao.CreateDocument();
                        inbcao.prcIN.PrintingSystem = baocao.PrintingSystem;
                        inbcao.ShowDialog();
                    }
                    else
                    {
                        BaoCao.rep_phieusieuamdoppler_tim baocao = new BaoCao.rep_phieusieuamdoppler_tim();
                        frmIn inbcao = new frmIn();
                        for (int p = 0; p < 15; p++)
                        {
                            baocao.ketqua[p] = ketqua[p];
                        }
                        for (int k = 0; k < 5; k++)
                        {
                            baocao.ketquac[k] = ketqua[k + 15];
                        }
                        if (thongtin.First().NgayThang.ToString() != "")
                        {
                            baocao.ngaychidinhs = DungChung.Ham.NgaySangChu(thongtin.First().NgayThang.Value);
                        }
                        baocao.ngaythuchiens = thongtin.First().NgayTH.ToString() != "" ? DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(thongtin.First().NgayTH.Value) : DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now);
                        baocao.yeucaus = string.Format("" + dichvu.First().TenDV.ToString() + "({0})", thongtin.First().ChiDinh1 == null ? "" : thongtin.First().ChiDinh1.ToString());
                        baocao.BSCD = tencanbocd.First().TenCB == null ? "" : tencanbocd.First().TenCB.ToString();
                        if(tencanbocd.First().NgayThang != null)
                        {
                            DateTime ngay = Convert.ToDateTime(tencanbocd.First().NgayThang);
                            baocao.ngaythang.Text = DungChung.Ham.NgaySangChu(ngay, DungChung.Bien.FormatDate);
                        }
                        baocao.CBTT = tencbtt.First().TenCB == null ? "" : tencbtt.First().TenCB.ToString();
                        baocao.loirans = layketqua.First().LoiDan;
                        baocao.ketluans = layketqua.First().KetLuan;
                        baocao.hotes = layttbn.First().TenBNhan.ToString();
                        baocao.dichis = layttbn.First().DChi.ToString();
                        baocao.tuois = layttbn.First().Tuoi.ToString();
                        baocao.chuandoans = layttbn.First().ChanDoan.ToString();
                        baocao.khoas = tenkhoa;
                        baocao.SoPhieu.Text = Convert.ToString(idcls);
                        baocao.hamloatphieu();
                        baocao.CreateDocument();
                        inbcao.prcIN.PrintingSystem = baocao.PrintingSystem;
                        inbcao.ShowDialog();
                    }
                }
            }
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


        private void frm_nhapketquasieuamdoppleer_Load(object sender, EventArgs e)
        {
            kiemtrakequa();
            loatthongtin(_idCLS);
            EnableControl(kiemtra);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            hamin(_idCLS, 0);
        }
        private void EnableControl(bool t)
        {
            GRIVIWEN.OptionsBehavior.Editable = t;
            Txt1.Properties.ReadOnly = !t;
            txt2.Properties.ReadOnly = !t;
            txt3.Properties.ReadOnly = !t;
            txt4.Properties.ReadOnly = !t;
            txt5.Properties.ReadOnly = !t;
            txt6.Properties.ReadOnly = !t;
            txt7.Properties.ReadOnly = !t;
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
            if (loup_nguoithuchien.EditValue != null && loup_nguoithuchien.EditValue != "")
            {
                if (datengaythuchien.DateTime <= DateTime.Now && !string.IsNullOrEmpty(datengaythuchien.Text))
                {
                    string[] kq = new string[15];

                    //for (int a = 0; a < GRIVIWEN.RowCount; a++)
                    //{
                    //    string t = "so" + a;
                    for (int b = 0; b < GRIVIWEN.VisibleColumns.Count; b++)
                    {
                        string t1 = "so" + (b + 1);
                        if (GRIVIWEN.GetRowCellValue(0, t1) != null)
                            kq[b] = GRIVIWEN.GetRowCellValue(0, t1).ToString();
                    }
                    //}
                    kqua = kq[0] + ";" + kq[1] + ";" + kq[2] + ";" + kq[3] + ";" + kq[4] + ";" + kq[5] + ";" + kq[6] + ";" + kq[7] + ";" + kq[8] + ";" + kq[9] + ";" + kq[10] + ";" + kq[11] + ";" + kq[12] + ";" + kq[13] + ";" + kq[14] + ";" + Txt1.Text + ";" + txt2.Text + ";" + txt3.Text + ";" + txt4.Text + ";" + txt5.Text;
                    var luuketqua = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) join h in data.CLScts on cd.IDCD equals h.IDCD join h1 in data.DichVucts on h.MaDVct equals h1.MaDVct select new { h.Id, h1.STT }).ToList();
                    string[] kequacls = new string[6] { kqua, Txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text };
                    for (int t = 0; t < 6; t++)
                    {
                        if (luuketqua.Where(p => p.STT == t + 1).Count() > 0)
                        {
                            int k1 = luuketqua.Where(p => p.STT == t + 1).First().Id;
                            var ketqualu1 = data.CLScts.Single(p => p.Id == k1);
                            ketqualu1.KetQua = kequacls[t];
                            ketqualu1.Status = 1;
                            data.SaveChanges();
                        }
                    }
                    var loid = (from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select h).ToList();
                    foreach (var a in loid)
                    {
                        a.KetLuan = txt6.Text.Trim();
                        a.LoiDan = txt7.Text.Trim();
                        a.NgayTH = datengaythuchien.DateTime;
                        a.Status = 1;
                        data.SaveChanges();
                    }
                    var cls = data.CLS.Single(p => p.IdCLS == _idCLS);
                    cls.Status = 1;
                    cls.MaCBth = loup_nguoithuchien.EditValue == null ? "" : loup_nguoithuchien.EditValue.ToString();
                    if (datengaythuchien != null)
                    {
                        cls.NgayTH = datengaythuchien.DateTime;
                    }
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
                                if (loup_nguoithuchien.EditValue != null)
                                    moi.MaCB = loup_nguoithuchien.EditValue.ToString();
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
                                if (loup_nguoithuchien.EditValue != null)
                                    moi.MaCB = loup_nguoithuchien.EditValue.ToString();
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

                    EnableControl(false);
                    MessageBox.Show("lưu thành công", "Thông báo");
                    if (reload != null)
                        reload();
                    frm_nhapketquasieuamdoppleer_Load(sender, e);
                }
                else
                    MessageBox.Show("Ngày thực hiện không được lớn hơn ngày hiện tại!", "Thông báo");
            }
            else
                MessageBox.Show("Cán bộ thực hiện không được để trống!", "Thông báo");
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            //DialogResult _result = MessageBox.Show("Banj muon xoa du lieu", "Hoir xoa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MessageBox.Show("Bạn muốn xóa dữ liệu", "Hỏi xóa", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                var luuketqua = (from cd in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) join h in data.CLScts on cd.IDCD equals h.IDCD join h1 in data.DichVucts on h.MaDVct equals h1.MaDVct select new { h.Id, h1.STT }).ToList();
                for (int t = 0; t < 6; t++)
                {
                    if (luuketqua.Where(p => p.STT == t + 1).Count() > 0)
                    {
                        int k1 = luuketqua.Where(p => p.STT == t + 1).First().Id;
                        var ketqualu1 = data.CLScts.Single(p => p.Id == k1);
                        ketqualu1.KetQua = "";
                        ketqualu1.Status = 0;
                        data.SaveChanges();
                    }
                }
                var ck = (from nhom in data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                          join dvu in data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                          select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).Select(p => p.MaDV).ToList();
                var loid = (from h in data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select h).ToList();
                foreach (var a in loid)
                {
                    int ID = a.IDCD;
                    var iddt = data.DThuoccts.Where(p => p.IDCD == ID && (ck.Count == 0 || !ck.Contains(p.MaDV ?? 0))).ToList();
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
                    a.MoTa = "";
                    a.KetLuan = "";
                    a.LoiDan = "";
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
                if (reload != null)
                    reload();
                frm_nhapketquasieuamdoppleer_Load(sender, e);
                txt6.Text = "";
                txt7.Text = "";
            }
        }

        private void frm_nhapketquasieuamdoppleer_KeyDown(object sender, KeyEventArgs e)
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


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            hamin(_idCLS, 1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                Txt1.Text = "- Dạng di động: Ngược chiều.  \r\n" +
                        "- Dốc tâm trương: 75 mm/s   \r\n" +
                        "- Tình trạng + dây chằng: Thanh mảnh    \r\n" +
                        "- Mép van: thanh mảnh.   Thanh mảnh    \r\n" +
                        "- Huyết khối nhĩ trái: Không thấy\r\n" +
                        "|Doppler:\r\n" +
                        "- Hở van hai lá: Không độ hở (0/4)\r\n" +
                        "- Diện tích lỗ van: 4,2  cm2  (2D) .  cm2 (PHT).";
                txt2.Text = "- Tình trạng van: thanh mảnh	\r\n" +
                            "- Dạng di động:   Hình hộp .\r\n" +
                            "- Biên độ mở:   mm.\r\n" +
                            "|Doppler:\r\n" +
                            "- Van ĐMC:   không, độ hở (0/4)\r\n" +
                            "- Diện tích lỗ van: 3,4 cm2\r\n";
                txt3.Text = "- Tình trạng van: Thanh mảnh.\r\n" +
                            "- Dạng di động:    Hình hộp.\r\n" +
                            "- Đ.kính gốc ĐMP: 21  mm.\r\n" +
                            "- Áp lực ĐMP ( ước tính): T.thu  :   20.5   mmHg.\r\n" +
                            "|Doppler:\r\n" +
                             "- Hở van ĐMP: Không, độ hở (0/4)\r\n" +
                            "- Cuối tâm trương:  13  mmHg, trung bình: 16 mmHg.";
                txt4.Text = "- T.trạng van: Thanh mảnh\r\n" +
                            "|Doppler:\r\n" +
                            "- Hở van ba lá: Không, độ hở (0/4)";
                txt5.Text = "- Màng ngoài tim không có dịch, không dầy dính";
                txt7.Text = "Không thấy Thông liên thất, thông liên nhĩ, ống động mạch.\r\n" +
                            "Các thành tim vận động bình thường";
                txt6.Text = "Nhịp tim: lần/phút.\r\n" +
                    "Dày vách liên thất và thành sau thất trái.\r\n" +
                    "Chức năng tâm thu thất trái trong giới hạn bình thường.";

            }
            else
            {
                Txt1.Text = "Dạng di động ngược chiều:  \r\n" +
                        "Dốc tâm trương: mm/s   \r\n" +
                        "K.cách hai bờ van:  mm  \r\n" +
                        "T.trang van + dây chằng: thanh mảnh    \r\n" +
                        "Mép van:thanh mảnh    \r\n" +
                        "$" +
                        "DE:  mm     E/A:       E--> VLT: mm\r\n" +
                        "Huyết khối nhĩ trái: Không thấy\r\n" +
                        "|- Doppler: E/A\r\n" +
                        "Gradient:         tối đa:  mmHg\r\n" +
                        "(Nhĩ – thất trái)       Trung bình:  mmHg\r\n" +
                        "Hở van hai lá: không (    /4)\r\n" +
                        "SHoHL – cm2  (TD) =        cm2 (   4B)\r\n" +
                        "D.tích lỗ van:      cm2 (2D): cm2 (PHT)";
                txt2.Text = "Tình trạng van: thanh mảnh	\r\n" +
                            "Biên độ mở van:   mm\r\n" +
                            "ĐK dòng HoC:  mm/ĐRTT: mm\r\n" +
                            "ĐK ĐMC lên:      mm, quai ĐMC:   mm\r\n" +
                            "ĐMC xuống:          mm\r\n" +
                            "$" +
                            "|- Doppler:\r\n" +
                            "Gradient:         tối đa:  mmHg\r\n" +
                            "(Thất trái - đmc)       Trung bình: mmHg\r\n" +
                            "Hở van ĐMC: Không (    /4)\r\n" +
                            "Diện tích lỗ van:      cm2 (2D)";
                txt3.Text = "Tình trạng van: thanh mảnh\r\n" +
                            "Di động:\r\n" +
                            "ĐK gốc ĐMP:     mm, thân ĐMP:   mm,\r\n" +
                            "Nhánh trái :  mm, nhánh phải:    mm,\r\n" +
                            "Áp lực ĐMP (ước tính): T.thu     mmHg\r\n" +
                            "$" +
                            "|- Doppler:\r\n" +
                             "Gradient:           tối đa:   mmHg\r\n" +
                            "(tâm thu - đmc)   Trung bình: mmHg\r\n" +
                           "Hở van ĐMP:không\r\n" +
                            "Cuối t.trương:    mmHg, tr.bình:   mmHg";
                txt4.Text = "T.trạng van: thanh mảnh\r\n" +
                            "$" +
                            "|- Doppler:\r\n" +
                            "Hở van ba lá: không   /4) S   HoBL: cm2\r\n" +
                            "Gradient tâm thu tối đa:   mmHg";
                txt5.Text = "Không có dịch ";
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void loup_nguoithuchien_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void datengaythuchien_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void GRIVIWEN_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void txthoten_Click(object sender, EventArgs e)
        {

        }
    }
    public class CBTH
    {
        public string MaCB { set; get; }
        public string TenCB { set; get; }
        public string MaKPsd { set; get; }
    }
    public class chiso
    {
        public int madvct { set; get; }
        public string so1 { set; get; }
        public string so2 { set; get; }
        public string so3 { set; get; }
        public string so4 { set; get; }
        public string so5 { set; get; }
        public string so6 { set; get; }
        public string so7 { set; get; }
        public string so8 { set; get; }
        public string so9 { set; get; }
        public string so10 { set; get; }
        public string so11 { set; get; }
        public string so12 { set; get; }
        public string so13 { set; get; }
        public string so14 { set; get; }
        public string so15 { set; get; }
    }
}