using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace QLBV.FormThamSo
{
    public partial class frmThongKeTTPT : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKeTTPT()
        {
            InitializeComponent();
        }
        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        List<KhoaPhong> _lKP = new List<KhoaPhong>();
        private void frmThongKeTTPT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtThang.DateTime = DateTime.Now;
            _lKP.Clear();
            _lKP = (from kp in _db.KPhongs
                    where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                    select new KhoaPhong()
                    {
                        Check = false,
                        MaKP = kp.MaKP,
                        TenKP = kp.TenKP
                    }).Distinct().OrderBy(p => p.TenKP).ToList();
            _lKP.Insert(0, new KhoaPhong { MaKP = 0, TenKP = "Tất cả", });
            cklKP.DataSource = null;
            cklKP.DataSource = _lKP;
            cklKP.CheckAll();

            cboTieuNhom.Properties.DataSource = (from dv in _db.DichVus join tn in _db.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom join nhom in _db.NhomDVs.Where(p => p.TenNhomCT == "Thủ thuật, phẫu thuật") on tn.IDNhom equals nhom.IDNhom select new {dv.TenDV,dv.MaDV }).Distinct().OrderBy(p=>p.TenDV).ToList();
       

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KhoaPhong> _dsKP = new List<KhoaPhong>();
            var kphong = data.KPhongs.ToList();

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    _dsKP.Add(new KhoaPhong { MaKP = Convert.ToInt32(cklKP.GetItemValue(i)), TenKP = cklKP.GetItemText(i) });
                }
            }
            int status = radiStatus.SelectedIndex;
            int thang = 0, nam = 0;
            thang = dtThang.DateTime.Month;
            nam = dtThang.DateTime.Year;
            string title = "DANH SÁCH BỆNH NHÂN THỰC HIỆN THỦ THUẬT - PHẪU THUẬT \n"+ cboTieuNhom.Text.ToUpper() +"\n Tháng " + thang + " năm " + nam;
            List<l_DSBN> _ldsBN = new List<l_DSBN>();
            List<l_DSBN> _ldsBNDS = new List<l_DSBN>();

            int madv = 0;
            if (cboTieuNhom.EditValue != null)
                madv = Convert.ToInt32(cboTieuNhom.EditValue);
            //var dichvu = (from dv in data.DichVus
            //              join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //              where tn.TenRG == cboTieuNhom.Text
            //              select new { dv.MaDV }).ToList();
            if (madv > 0)
            {

                var ds_rep = (from bn in data.BenhNhans
                              join cl in data.CLS on bn.MaBNhan equals cl.MaBNhan
                              join cd in data.ChiDinhs.Where(p => p.MaDV == madv) on cl.IdCLS equals cd.IdCLS
                              join clct in data.CLScts on cd.IDCD equals clct.IDCD
                              where (status == 0 ? (cl.NgayThang != null && cl.NgayThang.Value.Month == thang && cl.NgayThang.Value.Year == nam) :
                             (cd.NgayTH != null && cd.NgayTH.Value.Month == thang && cd.NgayTH.Value.Year == nam))
                          && cd.Status == status

                              select new { bn.TenBNhan, bn.Tuoi, bn.DTuong, cl.MaBNhan, cl.MaICD, cl.MaKP, NgayThang = status == 0 ? cl.NgayThang : cd.NgayTH, cd.MaDV }).ToList();
                var ds2 = (from ds in ds_rep
                           //join dv in dichvu on ds.MaDV equals dv.MaDV
                           join kp in _dsKP on ds.MaKP equals kp.MaKP
                           select new { ds.MaICD, ds.DTuong, ds.TenBNhan, ds.Tuoi, ds.NgayThang, ds.MaDV, ds.MaKP, ds.MaBNhan, kp.TenKP }).ToList();

                foreach (var a in ds2)
                {
                    l_DSBN moi = new l_DSBN();
                    moi.TenBNhan = a.TenBNhan;
                    moi.MaBNhan = a.MaBNhan ?? 0;
                    moi.Tuoi = a.Tuoi == null ? "" : a.Tuoi.ToString();
                    if (a.DTuong == "BHYT")
                        moi.BHYT = "X";
                    else
                        moi.DichVu = "X";
                    if (chkhienbenh.Checked)
                    moi.CanBenh = a.MaICD;
                    var khoaphongcd = kphong.Where(p => p.MaKP == a.MaKP).FirstOrDefault();
                    if (khoaphongcd != null)
                    {
                        if (khoaphongcd.PLoai == "Phòng khám")
                            moi.PhongKham = "X";
                        else
                            if (khoaphongcd.TenKP.ToLower().Contains("nội"))
                                moi.KhoaNoi = "X";
                            else
                                if (khoaphongcd.TenKP.ToLower().Contains("ngoại"))
                                    moi.KhoaNgoai = "X";
                                else
                                    moi.Khoa = "X";


                    }

                    int tong = 0;
                    moi.Ngay = new string[32];

                    for (int i = 1; i <= 31; i++)
                    {
                        if (a.NgayThang.Value.Day == i)
                        {
                            moi.Ngay[i] = "X";
                            tong++;
                        }
                        else
                        {
                            moi.Ngay[i] = "";
                        }

                    }
                    moi.Tong = tong;
                    _ldsBN.Add(moi);
                }
                _ldsBNDS = (from b in _ldsBN
                         group b by new {b.CanBenh, b.TenBNhan, b.MaBNhan, b.Tuoi, b.BHYT, b.DichVu, b.PhongKham, b.KhoaNoi, b.KhoaNgoai, b.Khoa } into kq
                            select new l_DSBN {CanBenh=kq.Key.CanBenh, TenBNhan = kq.Key.TenBNhan, MaBNhan = kq.Key.MaBNhan,Tuoi= kq.Key.Tuoi,BHYT= kq.Key.BHYT,DichVu= kq.Key.DichVu,PhongKham= kq.Key.PhongKham,KhoaNoi= kq.Key.KhoaNoi,KhoaNgoai= kq.Key.KhoaNgoai,Khoa= kq.Key.Khoa }).ToList();
                foreach (var item in _ldsBNDS)
                {
                    item.Ngay = new string[33];
                    foreach (var item2 in _ldsBN)
                    {
                        if (item.MaBNhan == item2.MaBNhan && ((item.Khoa == item2.Khoa && item.Khoa == "X") || (item.PhongKham == item2.PhongKham && item.PhongKham == "X") || (item.KhoaNgoai == item2.KhoaNgoai && item.KhoaNgoai == "X") || (item.KhoaNoi == item2.KhoaNoi && item.KhoaNoi == "X")))
                        {
                            item.Tong += item2.Tong;
                            for(int i=1;i<=31;i++)
                            {
                                if (item.Ngay[i] == null || item.Ngay[i] == "")
                                    item.Ngay[i] = item2.Ngay[i];
                            }
                        }
                    }
                }
                string[] tongngay = new string[40];
                tongngay[0] = _ldsBNDS.Where(p => p.BHYT == "X").ToList().Count.ToString();
                tongngay[1] = _ldsBNDS.Where(p => p.DichVu == "X").ToList().Count.ToString();
                tongngay[2] = _ldsBNDS.Where(p => p.PhongKham == "X").ToList().Count.ToString();
                tongngay[3] = _ldsBNDS.Where(p => p.KhoaNoi == "X").ToList().Count.ToString();
                tongngay[4] = _ldsBNDS.Where(p => p.KhoaNgoai == "X").ToList().Count.ToString();
                tongngay[5] = _ldsBNDS.Where(p => p.Khoa == "X").ToList().Count.ToString();
                string[] tongdv = new string[40];
                for (int i = 0; i < 31; i++)
                {
                    tongdv[i] = _ldsBNDS.Where(p => p.Ngay[i + 1] == "X").ToList().Count.ToString();
                }

                BaoCao.rep_ThongKeTTPT rep = new BaoCao.rep_ThongKeTTPT();

                rep.labTieuDe.Text = title;
                int j = 0;
                frmIn frm = new frmIn();
                foreach (XRTableCell cell in rep.xrTableRow3)
                {
                    if (j > 38)
                        break;
                    if (cell.Index == (j + 1))
                    {
                        if (j <6)
                        {
                            if (tongdv.Length >= j)
                                cell.Text = tongngay[j];
                            j++;
                        }
                        else
                        {
                            if (tongdv.Length >= j - 6)
                                cell.Text = tongdv[j - 6];
                            j++;
                        }
                    }



                }
                rep.DataSource = _ldsBNDS;
                rep.BindingData();
                rep.CreateDocument();

                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        public class l_DSBN
        {
            public string tenbn, canbenh;
            string tuoi;
            string bhyt, dichVu;
            int mabn;

            public string TenBNhan
            {
                set { tenbn = value; }
                get { return tenbn; }
            }
            public string CanBenh
            {
                set { canbenh = value; }
                get { return canbenh; }
            }
            public string BHYT
            {
                set { bhyt = value; }
                get { return bhyt; }
            }
            public string DichVu
            {
                set { dichVu = value; }
                get { return dichVu; }
            }
            public string Tuoi
            {
                set { tuoi = value; }
                get { return tuoi; }
            }
            public int MaBNhan
            {
                set { mabn = value; }
                get { return mabn; }
            }
            string khoa;
            public string Khoa
            {
                set { khoa = value; }
                get { return khoa; }
            }
            string khoanoi;
            public string KhoaNoi
            {
                set { khoanoi = value; }
                get { return khoanoi; }
            }
            string khoangoai;
            public string KhoaNgoai
            {
                set { khoangoai = value; }
                get { return khoangoai; }
            }
            string phongKham;
            public string PhongKham
            {
                set { phongKham = value; }
                get { return phongKham; }
            }
            string[] ngay;
            public string[] Ngay
            {
                set { ngay = value; }
                get { return ngay; }
            }
            int tong;
            public int Tong
            {
                set { tong = value; }
                get { return tong; }
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKP.GetItemChecked(0) == true)
                    cklKP.CheckAll();
                else
                    cklKP.UnCheckAll();
            }
        }

    }
}