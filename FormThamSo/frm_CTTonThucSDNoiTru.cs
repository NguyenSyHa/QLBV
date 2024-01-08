using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Collections;

namespace QLBV.FormThamSo
{

    public partial class frm_CTTonThucSDNoiTru : DevExpress.XtraEditors.XtraForm
    {
        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        QLBV_Database.QLBVEntities _dataContext;
        List<KhoaPhong> _lKP;
        List<TrongBH> _lTrongBH;
        DateTime ngaytu;
        DateTime ngayden;
        public frm_CTTonThucSDNoiTru()
        {
            InitializeComponent();
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.EditValue = DateTime.Now.Date;
            lupDenNgay.EditValue = DateTime.Now.Date;
            
            var q = (from dt in _dataContext.DThuocs
                    join kp in _dataContext.KPhongs on dt.MaKXuat equals kp.MaKP
                    select new{
                        MaKhoXuat = dt.MaKXuat,
                        TenKhoXuat = kp.TenKP
                    }).Distinct().OrderBy(p=>p.TenKhoXuat).ToList();
            lookUpKhoXuat.Properties.DataSource = q;
            lookUpKhoXuat.Properties.DisplayMember = "TenKhoXuat";
            lookUpKhoXuat.Properties.ValueMember = "MaKhoXuat";
            lookUpKhoXuat.Text = "";

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void frm_CTTonThucSD_Load(object sender, EventArgs e)
        {
            var q = (from kp in _dataContext.KPhongs
                     where(kp.PLoai=="Lâm sàng")
                         select new KhoaPhong()
                         {
                             Check = false,
                             MaKP = kp.MaKP,
                             TenKP = kp.TenKP
                         }).Distinct().ToList();
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP ="Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            bdSourceKP.DataSource = _lKP2;
            grcKhoaPhong.DataSource = bdSourceKP;

            var nhomdv = _dataContext.NhomDVs.Where(d => d.Status == 1).Select(d=>d.TenNhomCT).ToList();
            lupNhomDV.Properties.DataSource = nhomdv;
        }
        public bool kt() {
            int demKP = 0;
          
            if ((lupTuNgay.DateTime.Date - lupDenNgay.DateTime.Date).Days > 0 )
            {
                MessageBox.Show("Ngày từ không thể lớn hơn ngày đến");
                lupTuNgay.Focus();
                return  false;
            }
            if (lookUpKhoXuat.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho xuất!");
                lookUpKhoXuat.Focus();
                return false;
            }
            if (lupNhomDV.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm dịch vụ!");
                lupNhomDV.Focus();
                return false;
            }
            if (chk0.Checked == false && chk1.Checked == false && chk2.Checked == false)
            {
                MessageBox.Show("Bạn chưa chọn danh mục trong Bảo Hiểm!");
                groupControl1.Focus();
                return false;
            }
            for (int i = 1; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i,colCheckGrvKP).ToString() == "True")
                {
                    demKP++;
                }
            }
            if (demKP > 8)
            {
                MessageBox.Show("Tổng số khoa phòng được chọn phải ít hơn 8!");
                grcKhoaPhong.Focus();
                return false;
            }
            if (demKP == 0)
            {
                MessageBox.Show("Bạn chưa chọn Khoa Phòng!");
                grcKhoaPhong.Focus();
                return false;
            }
            
            return true;
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (kt())
            {
            _lKP = new List<KhoaPhong>(grvKhoaPhong.RowCount);
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                KhoaPhong kp2 = new KhoaPhong();
                kp2._check = true;
                kp2._kp = "";
                kp2._maKP = 0;
                _lKP.Add(kp2);
            }

            int j = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                {
                    _lKP[j].Check = true;
                    _lKP[j]._maKP = grvKhoaPhong.GetRowCellValue(i, colmaKP)== null ? 0 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));
                    if (grvKhoaPhong.GetRowCellValue(i, colTenKP) != null)
                        _lKP[j]._kp = grvKhoaPhong.GetRowCellValue(i, colTenKP).ToString();
                    j++;
                }

            }
            _lTrongBH = new List<TrongBH>();
            if (chk0.Checked)
            {
                _lTrongBH.Add(new TrongBH { Value = 0, chkTrongBH = true });
            }
            if (chk1.Checked)
            {
                _lTrongBH.Add(new TrongBH { Value = 1, chkTrongBH = true });
            }
            if (chk2.Checked)
            {
                _lTrongBH.Add(new TrongBH { Value = 2, chkTrongBH = true });
            }
            int khoxuat = lookUpKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lookUpKhoXuat.EditValue);
                string tennhomct = lupNhomDV.EditValue.ToString();
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                BaoCao.repCTTonThucSDNoiTru rep = new BaoCao.repCTTonThucSDNoiTru(_lKP);
                //Kiếm tra xem là nội trú
                int a1 = -1, a2 = -1, a3 =-1;
                if (chk0.Checked)
                    a1 = 0;
                if (chk1.Checked)
                    a2 = 1;
                if (chk2.Checked)
                    a3 = 2;
                if (rdNoiTru.EditValue.ToString()== ("rdNoiTru"))
                {
                    //Kiểm tra xem là tồn
                    if (rdChiTiet.EditValue.ToString()== ("rdTon"))
                    {
                        var _lBaoCao =
                       (from lkp in _lKP
                        join dt in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= ngaytu && p.NgayKe <= ngayden && p.MaKXuat == khoxuat) on lkp.MaKP equals dt.MaKP
                        where !(from v in _dataContext.VienPhis select v.MaBNhan).Contains(dt.MaBNhan)
                        join bn in _dataContext.BenhNhans.Where(b => b.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan

                        join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                        where(dtct.TrongBH== a1 || dtct.TrongBH ==a2 || dtct.TrongBH==a3 )
                        join dv in _dataContext.DichVus.Where(d => d.PLoai == 1) on dtct.MaDV equals dv.MaDV
                        join nhomdv in _dataContext.NhomDVs.Where(d => d.TenNhomCT== (tennhomct)) on dv.IDNhom equals nhomdv.IDNhom
                        group new { dv, dtct, dt } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into gr
                        select new
                        {
                            MaDV = gr.Key.MaDV,
                            TenDV = gr.Key.TenDV,
                            DonVi = gr.Key.DonVi,
                            DonGia = gr.Key.DonGia,
                            sl1 = gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl2 = gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl3 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl5 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl7 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                            sl8 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),

                            tt1 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt2 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt3 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt5 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt7 = gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            tt8 = gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                            SoLuong = gr.Sum(t => t.dtct.SoLuong),
                            ThanhTien = gr.Sum(t => t.dtct.ThanhTien)
                        }).OrderBy(p => p.TenDV).ToList();
                        rep.paramTenBC.Value = "BÁO CÁO CHI TIẾT TỒN THỰC SỬ DỤNG " + tennhomct.ToUpper() + " TẠI CÁC KHOA \n BẢO HIỂM Y TẾ NỘI TRÚ";
                        rep.DataSource = _lBaoCao;
                    }
                        //thực sử dụng
                    else
                    {
                        var _lBaoCao =
                               (from lkp in _lKP
                                join dt in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= ngaytu && p.NgayKe <= ngayden && p.MaKXuat == khoxuat) on lkp.MaKP equals dt.MaKP
                                where (from v in _dataContext.VienPhis select v.MaBNhan).Contains(dt.MaBNhan)
                                join bn in _dataContext.BenhNhans.Where(b => b.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
                                join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                join bh in
                                    (from trgBh in _lTrongBH
                                     select trgBh.Value) on dtct.TrongBH equals bh
                                join dv in _dataContext.DichVus.Where(d => d.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                join nhomdv in _dataContext.NhomDVs.Where(d => d.TenNhomCT== (tennhomct)) on dv.IDNhom equals nhomdv.IDNhom
                                group new { dv, dtct, dt } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into gr
                                select new
                                {
                                    MaDV = gr.Key.MaDV,
                                    TenDV = gr.Key.TenDV,
                                    DonVi = gr.Key.DonVi,
                                    DonGia = gr.Key.DonGia,
                                    sl1 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl2 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl3 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl5 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl7 = gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl8 = gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),

                                    tt1 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt2 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt3 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt5 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt6 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt7 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt8 = gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    SoLuong = gr.Sum(t => t.dtct.SoLuong),
                                    ThanhTien = gr.Sum(t => t.dtct.ThanhTien)
                                }).OrderBy(p => p.TenDV).ToList();
                        rep.paramTenBC.Value = "BÁO CÁO CHI TIẾT THỰC SỬ DỤNG " + tennhomct.ToUpper() + "\n TẠI CÁC KHOA BẢO HIỂM Y TẾ NỘI TRÚ";
                        rep.DataSource = _lBaoCao;
                    }
                }
                //Bệnh nhân điều trị ngoại trú
                else
                {
                    //Tồn
                    if (rdChiTiet.EditValue.ToString()== ("rdTon"))
                    {
                        var _lBaoCao =
                                   (from lkp in _lKP
                                    join dt in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= ngaytu && p.NgayKe <= ngayden && p.MaKXuat == khoxuat) on lkp.MaKP equals dt.MaKP
                                    where !(from v in _dataContext.VienPhis select v.MaBNhan).Contains(dt.MaBNhan)
                                    join bn in _dataContext.BenhNhans.Where(b => b.NoiTru == 0) on dt.MaBNhan equals bn.MaBNhan
                                    join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                    join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                    join bh in
                                        (from trgBh in _lTrongBH
                                         select trgBh.Value) on dtct.TrongBH equals bh
                                    join dv in _dataContext.DichVus.Where(d => d.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                    join nhomdv in _dataContext.NhomDVs.Where(d => d.TenNhomCT== (tennhomct)) on dv.IDNhom equals nhomdv.IDNhom
                                    group new { dv, dtct, dt } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into gr
                                    select new
                                    {
                                        MaDV = gr.Key.MaDV,
                                        TenDV = gr.Key.TenDV,
                                        DonVi = gr.Key.DonVi,
                                        DonGia = gr.Key.DonGia,
                                        sl1 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl2 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl3 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl5 = gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl7 = gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                        sl8 = gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),

                                        tt1 = gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt2 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt3 = gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt4 = gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt5 = gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt7 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        tt8 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                        SoLuong = gr.Sum(t => t.dtct.SoLuong),
                                        ThanhTien = gr.Sum(t => t.dtct.ThanhTien)
                                    }).OrderBy(p => p.TenDV).ToList();
                        rep.paramTenBC.Value = "BÁO CÁO CHI TIẾT TỒN THỰC SỬ DỤNG " + tennhomct.ToUpper() + " TẠI CÁC KHOA \n BẢO HIỂM Y TẾ ĐIỀU TRỊ NGOẠI TRÚ";
                        rep.DataSource = _lBaoCao;
                    }
                    else
                    {
                        var _lBaoCao =
                               (from lkp in _lKP
                                join dt in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= ngaytu && p.NgayKe <= ngayden && p.MaKXuat == khoxuat) on lkp.MaKP equals dt.MaKP
                                where (from v in _dataContext.VienPhis select v.MaBNhan).Contains(dt.MaBNhan)
                                join bn in _dataContext.BenhNhans.Where(b => b.NoiTru == 0) on dt.MaBNhan equals bn.MaBNhan
                                join vv in _dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                join bh in
                                    (from trgBh in _lTrongBH
                                     select trgBh.Value) on dtct.TrongBH equals bh
                                join dv in _dataContext.DichVus.Where(d => d.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                join nhomdv in _dataContext.NhomDVs.Where(d => d.TenNhomCT== (tennhomct)) on dv.IDNhom equals nhomdv.IDNhom
                                group new { dv, dtct, dt } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into gr
                                select new
                                {
                                    MaDV = gr.Key.MaDV,
                                    TenDV = gr.Key.TenDV,
                                    DonVi = gr.Key.DonVi,
                                    DonGia = gr.Key.DonGia,
                                    sl1 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl2 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl3 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl5 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl7 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    sl8 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.SoLuong),
                                    tt1 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(0).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt2 = gr.Where(p => p.dt.MaKP == _lKP.Skip(1).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt3 = gr.Where(p => p.dt.MaKP == _lKP.Skip(2).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt4 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(3).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt5 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(4).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt6 = gr.Where(p => p.dt.MaKP == _lKP.Skip(5).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt7 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(6).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    tt8 =  gr.Where(p => p.dt.MaKP == _lKP.Skip(7).Take(1).First().MaKP).Sum(p => p.dtct.ThanhTien),
                                    SoLuong = gr.Sum(t => t.dtct.SoLuong),
                                    ThanhTien = gr.Sum(t => t.dtct.ThanhTien)
                                }).OrderBy(p => p.TenDV).ToList();
                        rep.paramTenBC.Value = "BÁO CÁO CHI TIẾT THỰC SỬ DỤNG " + tennhomct.ToUpper() + " TẠI CÁC KHOA \n BẢO HIỂM Y TẾ ĐIỀU TRỊ NGOẠI TRÚ";
                        rep.DataSource = _lBaoCao;
                    }
                }
                
                //var ht = _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= ngaytu && p.NgayKe <= ngayden && p.MaKXuat == khoxuat).ToList();
                //var ht2 = (from dt in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= ngaytu && p.NgayKe <= ngayden && p.MaKXuat == khoxuat)
                //           join dt in _dataContext.VienPhis on dt.MaBNhan equals dt.MaBNhan into gr
                //          from sub in gr
                //          //where sub.MaBNhan == null
                //          select  sub).ToList();
                rep.paramTenCS.Value = DungChung.Bien.TenCQ;
                rep.paramMaCS.Value = DungChung.Bien.MaBV;
                rep.paramNgayThang.Value = "Từ ngày " + ngaytu.ToShortDateString() + " đến ngày " + ngayden.ToShortDateString();
                
                rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        public class TrongBH
        {
            public int values;
            public bool trong;

            public int Value { get { return values; } set { values = value; } }
            public bool chkTrongBH { get { return trong; } set { trong = value; } }
        }
        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP"){
                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {

                        }
                        else {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                    }
                    
                }
            }
        }

        private void grvKhoaPhong_Click(object sender, EventArgs e)
        {
            
        }

        private void grvKhoaPhong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void grcTrongBH_Click(object sender, EventArgs e)
        {

        }

        private void frm_CTTonThucSDNoiTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIn_Click(sender, new EventArgs());
            }
        }
    }
}