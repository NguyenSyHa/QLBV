using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_TongHopCLS_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TongHopCLS_27022()
        {
            InitializeComponent();
        }

        private void frm_BC_TongHopCLS_27022_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            #region đọc file XML và gán ds DV đã chọn vào list
            List<int> maDV = new List<int>();
            string fileName = "DSDV.xml";
            if (System.IO.File.Exists(fileName))
            {
                XElement xel = XElement.Load(fileName);
                foreach (XElement xmlE in xel.Elements("dsDV"))
                {
                    if (xmlE.Attribute("dsDV").Value.ToString() == "dsDV")
                    {
                        foreach (XElement i in xmlE.Elements("ma_dv"))
                        {
                            maDV.Add(Convert.ToInt32(i.Value));
                        }
                    }
                }
            }
            #endregion
            var ldv = (from dv in data.DichVus.Where(p => p.PLoai == 2 && p.Status == 1)
                       select new { dv.TenDV, dv.MaDV }).Distinct().OrderBy(p => p.TenDV).ToList();
            var kham = ldv.Where(p => p.TenDV.ToLower().Contains("tiền công khám")).ToList();
            if (kham.Count > 0)
            {
                foreach (var k in kham)
                {
                    CongKham ckham = new CongKham();
                    ckham.Check = true;
                    ckham.TenDV = k.TenDV;
                    ckham.MaDV = k.MaDV;
                    _lKham.Add(ckham);
                }
                cklCongKham.DataSource = _lKham;
                for (int i = 0; i < cklCongKham.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCongKham.GetItemValue(i)) == item)
                                cklCongKham.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCongKham.SetItemChecked(i, true);
                }
            }
            #region DV1
            var dv1 = ldv.Where(p => p.TenDV.ToLower().Contains("thử thị lực")).ToList();
            if (dv1.Count > 0)
            {
                foreach (var k in dv1)
                {
                    DV1 dvu1 = new DV1();
                    dvu1.Check = true;
                    dvu1.TenDV = k.TenDV;
                    dvu1.MaDV = k.MaDV;
                    _lDV1.Add(dvu1);
                }
                cklCot1.DataSource = _lDV1;
                for (int i = 0; i < cklCot1.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot1.GetItemValue(i)) == item)
                                cklCot1.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot1.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV2
            var dv2 = ldv.Where(p => p.TenDV.ToLower().Contains("đo khúc xạ")).ToList();
            if (dv2.Count > 0)
            {
                foreach (var k in dv2)
                {
                    DV2 dvu2 = new DV2();
                    dvu2.Check = true;
                    dvu2.TenDV = k.TenDV;
                    dvu2.MaDV = k.MaDV;
                    _lDV2.Add(dvu2);
                }
                cklCot2.DataSource = _lDV2;
                for (int i = 0; i < cklCot2.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot2.GetItemValue(i)) == item)
                                cklCot2.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot2.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV3
            var dv3 = ldv.Where(p => p.TenDV.ToLower().Contains("đo nhãn áp")).ToList();
            if (dv3.Count > 0)
            {
                foreach (var k in dv3)
                {
                    DV3 dvu3 = new DV3();
                    dvu3.Check = true;
                    dvu3.TenDV = k.TenDV;
                    dvu3.MaDV = k.MaDV;
                    _lDV3.Add(dvu3);
                }
                cklCot3.DataSource = _lDV3;
                for (int i = 0; i < cklCot3.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot3.GetItemValue(i)) == item)
                                cklCot3.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot3.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV4
            var dv4 = ldv.Where(p => p.TenDV.ToLower().Contains("soi đáy mắt")).ToList();
            if (dv4.Count > 0)
            {
                foreach (var k in dv4)
                {
                    DV4 dvu4 = new DV4();
                    dvu4.Check = true;
                    dvu4.TenDV = k.TenDV;
                    dvu4.MaDV = k.MaDV;
                    _lDV4.Add(dvu4);
                }
                cklCot4.DataSource = _lDV4;
                for (int i = 0; i < cklCot4.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot4.GetItemValue(i)) == item)
                                cklCot4.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot4.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV5
            var dv5 = ldv.Where(p => p.TenDV.ToLower().Contains("thử kính")).ToList();
            if (dv5.Count > 0)
            {
                foreach (var k in dv5)
                {
                    DV5 dvu5 = new DV5();
                    dvu5.Check = true;
                    dvu5.TenDV = k.TenDV;
                    dvu5.MaDV = k.MaDV;
                    _lDV5.Add(dvu5);
                }
                cklCot5.DataSource = _lDV5;
                for (int i = 0; i < cklCot5.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot5.GetItemValue(i)) == item)
                                cklCot5.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot5.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV6
            var dv6 = ldv.Where(p => p.TenDV.ToLower().Contains("lấy dị vật") || p.TenDV.ToLower().Contains("cắt chỉ") || p.TenDV.ToLower().Contains("soi trực tiếp")).ToList();
            if (dv6.Count > 0)
            {
                foreach (var k in dv6)
                {
                    DV6 dvu6 = new DV6();
                    dvu6.Check = true;
                    dvu6.TenDV = k.TenDV;
                    dvu6.MaDV = k.MaDV;
                    _lDV6.Add(dvu6);
                }
                cklCot6.DataSource = _lDV6;
                for (int i = 0; i < cklCot6.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot6.GetItemValue(i)) == item)
                                cklCot6.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot6.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV7
            var dv7 = ldv.Where(p => p.TenDV.ToLower().Contains("chích chắp") || p.TenDV.ToLower().Contains("siêu âm") || p.TenDV.ToLower().Contains("soi trực tiếp nhuộm soi")).ToList();
            if (dv7.Count > 0)
            {
                foreach (var k in dv7)
                {
                    DV7 dvu7 = new DV7();
                    dvu7.Check = true;
                    dvu7.TenDV = k.TenDV;
                    dvu7.MaDV = k.MaDV;
                    _lDV7.Add(dvu7);
                }
                cklCot7.DataSource = _lDV7;
                for (int i = 0; i < cklCot7.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot7.GetItemValue(i)) == item)
                                cklCot7.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot7.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV8
            var dv8 = ldv.Where(p => p.TenDV.ToLower().Contains("thông lệ đạo") || p.TenDV.ToLower().Contains("tiêm hậu nhãn")).ToList();
            if (dv8.Count > 0)
            {
                foreach (var k in dv8)
                {
                    DV8 dvu8 = new DV8();
                    dvu8.Check = true;
                    dvu8.TenDV = k.TenDV;
                    dvu8.MaDV = k.MaDV;
                    _lDV8.Add(dvu8);
                }
                cklCot8.DataSource = _lDV8;
                for (int i = 0; i < cklCot8.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot8.GetItemValue(i)) == item)
                                cklCot8.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot8.SetItemChecked(i, true);
                }
            }
            #endregion
            #region DV9
            var dv9 = ldv.Where(p => p.TenDV.ToLower().Contains("lấy sạn vôi") || p.TenDV.ToLower().Contains("đánh bờ mi")).ToList();
            if (dv9.Count > 0)
            {
                foreach (var k in dv9)
                {
                    DV9 dvu9 = new DV9();
                    dvu9.Check = true;
                    dvu9.TenDV = k.TenDV;
                    dvu9.MaDV = k.MaDV;
                    _lDV9.Add(dvu9);
                }
                cklCot9.DataSource = _lDV9;
                for (int i = 0; i < cklCot9.ItemCount; i++)
                {
                    if (maDV.Count > 0)
                    {
                        foreach (var item in maDV)
                        {
                            if (Convert.ToInt32(cklCot9.GetItemValue(i)) == item)
                                cklCot9.SetItemChecked(i, true);
                        }
                    }
                    else
                        cklCot9.SetItemChecked(i, true);
                }
            }
            #endregion
        }

        List<CongKham> _lKham = new List<CongKham>();
        List<DV1> _lDV1 = new List<DV1>();
        List<DV2> _lDV2 = new List<DV2>();
        List<DV3> _lDV3 = new List<DV3>();
        List<DV4> _lDV4 = new List<DV4>();
        List<DV5> _lDV5 = new List<DV5>();
        List<DV6> _lDV6 = new List<DV6>();
        List<DV7> _lDV7 = new List<DV7>();
        List<DV8> _lDV8 = new List<DV8>();
        List<DV9> _lDV9 = new List<DV9>();

        #region class DVu
        public class CongKham
        {
            private bool check;

            public bool Check
            {
                get { return check; }
                set { check = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
        }
        public class DVChon
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
        }
        public class DV1
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV2
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV3
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV4
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV5
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV6
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV7
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV8
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        public class DV9
        {
            public bool Check { get; set; }
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
        }
        #endregion
        #region class BenhNhan
        public class BenhNhan
        {
            public int? MaBNhan { get; set; }
            public int? MaDV { get; set; }
            public string QuyenHD { get; set; }
            public string SoHD { get; set; }
            public string Ngay { get; set; }
            public string Hoten { get; set; }
            public string DiaChi { get; set; }
            public double? TTKham { get; set; }
            public string DV1 { get; set; }
            public double? TTDV1 { get; set; }
            public string DV2 { get; set; }
            public double? TTDV2 { get; set; }
            public string DV3 { get; set; }
            public double? TTDV3 { get; set; }
            public string DV4 { get; set; }
            public double? TTDV4 { get; set; }
            public string DV5 { get; set; }
            public double? TTDV5 { get; set; }
            public string DV6 { get; set; }
            public double? TTDV6 { get; set; }
            public string DV7 { get; set; }
            public double? TTDV7 { get; set; }
            public string DV8 { get; set; }
            public double? TTDV8 { get; set; }
            public string DV9 { get; set; }
            public double? TTDV9 { get; set; }
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KTBC()
        {
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Chưa chọn ngày bắt đầu in báo cáo", "Thông báo");
                dtTuNgay.Focus();
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Chưa chọn ngày kết thúc in báo cáo", "Thông báo");
                dtDenNgay.Focus();
                return false;
            }
            if (Convert.ToDateTime(dtTuNgay.EditValue) > Convert.ToDateTime(dtTuNgay.EditValue))
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu", "Thông báo");
                dtDenNgay.Focus();
                return false;
            }
            return true;
        }

        List<THCLS> _lKQ = new List<THCLS>();
        List<DVChon> _lDVChon = new List<DVChon>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            BaoCao.Rep_BC_TongHopCLS_27022 rep = new BaoCao.Rep_BC_TongHopCLS_27022();
            if (KTBC())
            {
                #region lấy danh sách dịch vụ chọn theo từng cột
                _lDVChon.Clear();
                for (int i = 0; i < cklCongKham.ItemCount; i++)
                {
                    if (cklCongKham.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCongKham.GetItemValue(i)), TenDV = cklCongKham.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot1.ItemCount; i++)
                {
                    if (cklCot1.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot1.GetItemValue(i)), TenDV = cklCot1.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot2.ItemCount; i++)
                {
                    if (cklCot2.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot2.GetItemValue(i)), TenDV = cklCot2.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot3.ItemCount; i++)
                {
                    if (cklCot3.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot3.GetItemValue(i)), TenDV = cklCot3.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot4.ItemCount; i++)
                {
                    if (cklCot4.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot4.GetItemValue(i)), TenDV = cklCot4.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot5.ItemCount; i++)
                {
                    if (cklCot5.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot5.GetItemValue(i)), TenDV = cklCot5.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot6.ItemCount; i++)
                {
                    if (cklCot6.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot6.GetItemValue(i)), TenDV = cklCot6.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot7.ItemCount; i++)
                {
                    if (cklCot7.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot7.GetItemValue(i)), TenDV = cklCot7.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot8.ItemCount; i++)
                {
                    if (cklCot8.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot8.GetItemValue(i)), TenDV = cklCot8.GetItemText(i) });
                    }
                }
                for (int i = 0; i < cklCot9.ItemCount; i++)
                {
                    if (cklCot9.GetItemCheckState(i) == CheckState.Checked)
                    {
                        _lDVChon.Add(new DVChon { MaDV = Convert.ToInt32(cklCot9.GetItemValue(i)), TenDV = cklCot9.GetItemText(i) });
                    }
                }
                #endregion
                XuatXML(_lDVChon, "DSDV.xml");
                var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 2) select dv).ToList();
                var qtamung = (from tu in data.TamUngs
                               select new
                               {
                                   tu.MaBNhan,
                                   tu.IDTamUng,
                                   tu.QuyenHD,
                                   tu.SoHD,
                                   tu.SoTien,
                                   TienUng = tu.SoTien + ((tu.PhanLoai == 4) ? tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0)) - ((tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.TienChenh : 0),
                                   TienThua = (tu.PhanLoai == 4) ? tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0),
                                   ThuThem = (tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.TienChenh : 0
                               }).ToList();

                var qvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                           join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           join bn in data.BenhNhans.Where(p => p.SThe == null || p.SThe == "") on vp.MaBNhan equals bn.MaBNhan
                           select new { vp.MaBNhan, vpct.MaDV, vpct.ThanhTien, bn.TenBNhan, bn.DChi, vpct.IDTamUng, vp.NgayTT }).ToList();
                var qTHcls = (from t in qtamung
                              join v in qvp on t.IDTamUng equals v.IDTamUng
                              join d in qdv on v.MaDV equals d.MaDV
                              join b in _lDVChon on v.MaDV equals b.MaDV
                              group new { t, v, b, d } by new { t.QuyenHD, t.SoHD, v.MaBNhan, v.TenBNhan, v.DChi, v.NgayTT, t.TienUng, t.TienThua, t.ThuThem } into kq
                              select new
                              {
                                  TTKham = kq.Where(p => p.b.TenDV.ToLower().Contains("công khám")).Sum(p => p.v.ThanhTien),
                                  QuyenHD = kq.Key.QuyenHD,
                                  SoHD = kq.Key.SoHD,
                                  kq.Key.MaBNhan,
                                  TienUng = kq.Key.TienUng,
                                  TienThua = kq.Key.TienThua,
                                  ThuThem = kq.Key.ThuThem,
                                  Ngay = String.Format("{0:dd/MM}", kq.Key.NgayTT),
                                  kq.Key.NgayTT,
                                  Hoten = kq.Key.TenBNhan,
                                  DiaChi = kq.Key.DChi,
                                  TTDV1 = kq.Where(p => p.b.TenDV.ToLower().Contains("thử thị lực")).Sum(p => p.v.ThanhTien),
                                  TTDV2 = kq.Where(p => p.b.TenDV.ToLower().Contains("đo khúc xạ")).Sum(p => p.v.ThanhTien),
                                  TTDV3 = kq.Where(p => p.b.TenDV.ToLower().Contains("đo nhãn áp")).Sum(p => p.v.ThanhTien),
                                  TTDV4 = kq.Where(p => p.b.TenDV.ToLower().Contains("soi đáy mắt")).Sum(p => p.v.ThanhTien),
                                  TTDV5 = kq.Where(p => p.b.TenDV.ToLower().Contains("thử kính")).Sum(p => p.v.ThanhTien),
                                  TTDV6 = kq.Where(p => p.b.TenDV.ToLower().Contains("lấy dị vật") || p.b.TenDV.ToLower().Contains("cắt chỉ") || p.b.TenDV.ToLower().Contains("soi trực tiếp")).Sum(p => p.v.ThanhTien),
                                  TTDV7 = kq.Where(p => p.b.TenDV.ToLower().Contains("chích chắp") || p.b.TenDV.ToLower().Contains("siêu âm") || p.b.TenDV.ToLower().Contains("soi trực tiếp nhuộm soi")).Sum(p => p.v.ThanhTien),
                                  TTDV8 = kq.Where(p => p.b.TenDV.ToLower().Contains("thông lệ đạo") || p.b.TenDV.ToLower().Contains("tiêm hậu nhãn")).Sum(p => p.v.ThanhTien),
                                  TTDV9 = kq.Where(p => p.b.TenDV.ToLower().Contains("lấy sạn vôi") || p.b.TenDV.ToLower().Contains("đánh bờ mi")).Sum(p => p.v.ThanhTien),
                                  Tong = kq.Where(p => p.b.TenDV.ToLower().Contains("thử thị lực") || p.b.TenDV.ToLower().Contains("đo khúc xạ")
                                                  || p.b.TenDV.ToLower().Contains("đo nhãn áp") || p.b.TenDV.ToLower().Contains("soi đáy mắt")
                                                  || p.b.TenDV.ToLower().Contains("thử kính") || p.b.TenDV.ToLower().Contains("tiền công khám")
                                                  || p.b.TenDV.ToLower().Contains("lấy dị vật") || p.b.TenDV.ToLower().Contains("cắt chỉ") || p.b.TenDV.ToLower().Contains("soi trực tiếp")
                                                  || p.b.TenDV.ToLower().Contains("chích chắp") || p.b.TenDV.ToLower().Contains("siêu âm") || p.b.TenDV.ToLower().Contains("soi trực tiếp nhuộm soi")
                                                  || p.b.TenDV.ToLower().Contains("thông lệ đạo") || p.b.TenDV.ToLower().Contains("tiêm hậu nhãn")
                                                  || p.b.TenDV.ToLower().Contains("lấy sạn vôi") || p.b.TenDV.ToLower().Contains("đánh bờ mi")).Sum(p => p.v.ThanhTien)
                              }).OrderBy(p => p.NgayTT).ThenBy(p => p.Hoten).ToList();

                double tienbangchu = 0;
                foreach (var item in qTHcls)
                {
                    tienbangchu += item.Tong;
                }
                frmIn frm = new frmIn();
                rep.DataSource = qTHcls;//.OrderBy(p => p.Ngay).ThenBy(p => p.Hoten).ToList();
                rep.lblTuNgay.Text = ("Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy")).ToUpper();
                rep.lblTienBangChu.Text = "Tổng tiền bằng chữ: " + DungChung.Ham.DocTienBangChu(tienbangchu, " đồng chẵn./.");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        #region xuất XML
        private void XuatXML(List<DVChon> _list, string path)
        {
            try
            {
                var xEle = new XElement("DVChon",
                            from item in _list
                            select new XElement("dsDV",
                                new XAttribute("dsDV", "dsDV"),
                                           new XElement("ma_dv", "" + item.MaDV + ""),
                                           new XElement("ten_dv", "" + item.TenDV + "")
                                       ));
                xEle.Save(path);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region class THCLS
        public class THCLS
        {
            public int? MaBNhan { get; set; }
            public string QuyenHD { get; set; }
            public string SoHD { get; set; }
            public string Ngay { get; set; }
            public string Hoten { get; set; }
            public string DiaChi { get; set; }
            public double? TienUng { get; set; }
            public double? TTKham { get; set; }
            public double? TTDV1 { get; set; }
            public double? TTDV2 { get; set; }
            public double? TTDV3 { get; set; }
            public double? TTDV4 { get; set; }
            public double? TTDV5 { get; set; }
            public double? TTDV6 { get; set; }
            public double? TTDV7 { get; set; }
            public double? TTDV8 { get; set; }
            public double? TTDV9 { get; set; }
            public double? Tong { get; set; }
            public double? TienThua { get; set; }
            public double? ThuThem { get; set; }
        }
        #endregion
    }
}