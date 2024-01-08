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
    public partial class frm_BC_HDPalTuyenHuyen_30005 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_HDPalTuyenHuyen_30005()
        {
            InitializeComponent();
        }

        public class Quy
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
        public class namBC
        {
            public int Value { set; get; }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_HDPalTuyenHuyen_30005_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            if (rdTheoNgay.Checked)
            {
                cbbQuy.Enabled = false;
                cbbNam.Enabled = false;
                lupDenNgay.Enabled = true;
                lupTuNgay.Enabled = true;
            }

            cbbNam.DisplayMember = "Value";
            cbbNam.ValueMember = "Value";

            List<Quy> _listQuy = new List<Quy>();
            _listQuy.Add(new Quy { Value = 1, Text = "Quý 1" });
            _listQuy.Add(new Quy { Value = 2, Text = "Quý 2" });
            _listQuy.Add(new Quy { Value = 3, Text = "Quý 3" });
            _listQuy.Add(new Quy { Value = 4, Text = "Quý 4" });

            cbbQuy.DataSource = _listQuy;
            cbbQuy.DisplayMember = "Text";
            cbbQuy.ValueMember = "Value";
            int namHT = DateTime.Now.Year;
            List<namBC> _list = new List<namBC>();
            for (int i = namHT - 10; i <= namHT; i++)
            {
                namBC obj = new namBC();
                obj.Value = i;
                _list.Add(obj);
            }
            cbbNam.DisplayMember = "Value";
            cbbNam.ValueMember = "Value";
            cbbNam.DataSource = _list;
            cbbNam.SelectedValue = namHT;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.rep_BC_HDPalTuyenHuyen_30005 rep = new BaoCao.rep_BC_HDPalTuyenHuyen_30005();
            DateTime tungay = new DateTime();
            DateTime denngay = new DateTime();
            #region tìm theo ngày
            if (rdTheoNgay.Checked)
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                rep.Ngay.Value = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            }
            #endregion
            #region tìm theo quý
            if (radQuy.Checked)
            {
                int _thang = (int)cbbQuy.SelectedValue;
                int _nam = (int)cbbNam.SelectedValue;
                switch (_thang)
                {
                    case 1:
                        tungay = new DateTime(_nam, 1, 1);
                        denngay = new DateTime(_nam, 4, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                    case 2:
                        tungay = new DateTime(_nam, 3, 1);
                        denngay = new DateTime(_nam, 7, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                    case 3:
                        tungay = new DateTime(_nam, 6, 1);
                        denngay = new DateTime(_nam, 10, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                    case 4:
                        tungay = new DateTime(_nam, 9, 1);
                        denngay = new DateTime(_nam + 1, 1, 1);
                        tungay = DungChung.Ham.NgayTu(tungay);
                        denngay = DungChung.Ham.NgayDen(denngay.AddDays(-1));
                        break;
                }
                rep.Ngay.Value = "Quý " + _thang + " năm " + _nam;
            }
            #endregion

            var BVhientai = (from bv in data.BenhViens.Where(p => p.MaBV.Equals(DungChung.Bien.MaBV)) select new { bv.TuyenBV }).First();
            int hangBVHT = BVhientai.TuyenBV.Trim().Contains("A") ? 1 : (BVhientai.TuyenBV.Trim().Contains("B") ? 2 : (BVhientai.TuyenBV.Trim().Contains("C") ? 3 : 4));
            var qbnCV = (from rv in data.RaViens
                         join bv in data.BenhViens on rv.MaBVC equals bv.MaBV into kq
                         from kq1 in kq.DefaultIfEmpty()
                         select new
                         {
                             rv.MaBNhan,
                             HangBV = kq1 != null ? (kq1.TuyenBV.Trim().Contains("A") ? 1 : (kq1.TuyenBV.Trim().Contains("B") ? 2 : (kq1.TuyenBV.Trim().Contains("C") ? 3 : 4))) : 0,
                             rv.KetQua,
                             rv.Status
                         }).ToList();
            var qttbx = (from tt in data.TTboXungs select tt).ToList();
            var bnkb = (from kb in data.BNKBs select kb).ToList();
            var bn = (from b in data.BenhNhans select b).ToList();
            var qbv = (from b in data.BenhViens.Where(p => p.MaBV.Equals(DungChung.Bien.MaBV)) select new { b.TenBV }).First();
            var qhuyen = (from h in data.DmHuyens.Where(p => p.MaHuyen.Equals(DungChung.Bien.MaHuyen))
                          join t in data.DmTinhs on h.MaTinh equals t.MaTinh
                          select new { h.TenHuyen, t.TenTinh }).ToList();
            var qkp = (from kp in data.KPhongs select kp).ToList();
            rep.TenBV.Value = qbv.TenBV;
            rep.Huyen.Value = qhuyen.Count > 0 ? qhuyen.First().TenHuyen : "";
            rep.Tinh.Value = qhuyen.Count > 0 ? qhuyen.First().TenTinh : "";
            #region I. BN khám, chẩn đoán và điều trị ngoại trú (khoa khám bệnh)
            var qpkham = qkp.Where(p => p.PLoai.Equals("Phòng khám")).ToList();

            var qbnkb = (from k in qpkham
                         join kb in bnkb on k.MaKP equals kb.MaKP
                         join b in bn.Where(p => p.NoiTru == 0).Where(p => p.NNhap >= tungay && p.NNhap <= denngay) on kb.MaBNhan equals b.MaBNhan
                         join t in qttbx on b.MaBNhan equals t.MaBNhan
                         join rv in qbnCV on b.MaBNhan equals rv.MaBNhan into kq
                         from kq1 in kq.DefaultIfEmpty()
                         select new { kb.MaBNhan, kb.MaICD, kb.PhuongAn, HangBV = kq1 == null ? 0 : kq1.HangBV, b.CapCuu, t.DTuongLao, Status = kq1 == null ? 0 : kq1.Status }).ToList();
            rep.SL1.Value = qbnkb.Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI1.Value = qbnkb.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI1.Value = qbnkb.Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();

            var qkhamHoHap = (from kb in qbnkb
                              select new { kb.MaBNhan, kb.MaICD, kb.PhuongAn, kb.HangBV, kb.CapCuu, kb.Status }).ToList();

            #region bệnh đường hô hấp cấp
            //viêm đường hô hấp trên
            rep.SL211.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J30") || p.MaICD.Contains("J31") || p.MaICD.Contains("J32") || p.MaICD.Equals("J33") || p.MaICD.Equals("J34")
                                                || p.MaICD.Equals("J35") || p.MaICD.Equals("J36") || p.MaICD.Equals("J37") || p.MaICD.Equals("J38") || p.MaICD.Equals("J39"))
                                                .Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI211.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J30") || p.MaICD.Contains("J31") || p.MaICD.Contains("J32") || p.MaICD.Equals("J33") || p.MaICD.Equals("J34")
                                                || p.MaICD.Equals("J35") || p.MaICD.Equals("J36") || p.MaICD.Equals("J37") || p.MaICD.Equals("J38") || p.MaICD.Equals("J39"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI211.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J30") || p.MaICD.Contains("J31") || p.MaICD.Contains("J32") || p.MaICD.Equals("J33") || p.MaICD.Equals("J34")
                                                || p.MaICD.Equals("J35") || p.MaICD.Equals("J36") || p.MaICD.Equals("J37") || p.MaICD.Equals("J38") || p.MaICD.Equals("J39"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //viêm phế quản
            rep.SL212.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J20") || p.MaICD.Contains("J41") || p.MaICD.Equals("J68.0")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI212.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J20") || p.MaICD.Contains("J41") || p.MaICD.Equals("J68.0"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI212.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J20") || p.MaICD.Contains("J41") || p.MaICD.Equals("J68.0"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //viêm phổi
            rep.SL213.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69"))
                                              .Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI213.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Equals("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI213.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Equals("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //khác
            rep.SL214.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J42") || p.MaICD.Equals("J18.0") || p.MaICD.Equals("J18.8") | p.MaICD.Equals("C39.0")
                                              || p.MaICD.Equals("J39.9") || p.MaICD.Equals("J39.8") || p.MaICD.Equals("J39.3"))
                                              .Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI214.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J42") || p.MaICD.Equals("J18.0") || p.MaICD.Equals("J18.8") | p.MaICD.Equals("C39.0")
                                              || p.MaICD.Equals("J39.9") || p.MaICD.Equals("J39.8") || p.MaICD.Equals("J39.3"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI214.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J42") || p.MaICD.Equals("J18.0") || p.MaICD.Equals("J18.8") | p.MaICD.Equals("C39.0")
                                              || p.MaICD.Equals("J39.9") || p.MaICD.Equals("J39.8") || p.MaICD.Equals("J39.3"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //cộng 2.1
            rep.Cong21.Value = Convert.ToDouble(rep.SL211.Value) + Convert.ToDouble(rep.SL212.Value) + Convert.ToDouble(rep.SL213.Value) + Convert.ToDouble(rep.SL214.Value);
            rep.CongCC21.Value = Convert.ToDouble(rep.CCI211.Value) + Convert.ToDouble(rep.CCI212.Value) + Convert.ToDouble(rep.CCI213.Value) + Convert.ToDouble(rep.CCI214.Value);
            rep.CongKhacI21.Value = Convert.ToDouble(rep.KhacI211.Value) + Convert.ToDouble(rep.KhacI212.Value) + Convert.ToDouble(rep.KhacI213.Value) + Convert.ToDouble(rep.KhacI214.Value);
            #endregion
            #region bệnh hô hấp mạn tính
            //hen
            rep.SL221.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI221.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI221.Value = qkhamHoHap.Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //COPD
            rep.SL222.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J44")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI222.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J44"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI222.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J44"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //khác
            rep.SL223.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J41") || p.MaICD.Equals("J42") || p.MaICD.Equals("J43")
                                                    || p.MaICD.Equals("J47")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI223.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J41") || p.MaICD.Equals("J42") || p.MaICD.Equals("J43")
                                                    || p.MaICD.Equals("J47"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI223.Value = qkhamHoHap.Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J41") || p.MaICD.Equals("J42") || p.MaICD.Equals("J43") || p.MaICD.Equals("J47"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();

            rep.Cong22.Value = Convert.ToDouble(rep.SL221.Value) + Convert.ToDouble(rep.SL222.Value) + Convert.ToDouble(rep.SL223.Value);
            rep.CongCCI22.Value = Convert.ToDouble(rep.CCI221.Value) + Convert.ToDouble(rep.CCI222.Value) + Convert.ToDouble(rep.CCI223.Value);
            rep.CongKhacI22.Value = Convert.ToDouble(rep.KhacI221.Value) + Convert.ToDouble(rep.KhacI222.Value) + Convert.ToDouble(rep.KhacI223.Value);
            #endregion
            #region khám và điều trị bệnh hô hấp
            rep.SL2.Value = Convert.ToDouble(rep.Cong21.Value) + Convert.ToDouble(rep.Cong22.Value);
            rep.CCI2.Value = Convert.ToDouble(rep.CongCC21.Value) + Convert.ToDouble(rep.CongCCI22.Value);
            rep.KhacI2.Value = Convert.ToDouble(rep.CongKhacI21.Value) + Convert.ToDouble(rep.CongKhacI22.Value);
            #endregion

            rep.SL231.Value = qbnkb.Where(p => p.DTuongLao != null).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI231.Value = qbnkb.Where(p => p.DTuongLao != null)
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI231.Value = qbnkb.Where(p => p.DTuongLao != null)
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();

            var qbnXNDom = (from b in qbnkb
                            join cls in data.CLS on b.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus.Where(p => p.PLoai == 2) on cd.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)) on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { b.MaBNhan, b.CapCuu, b.PhuongAn, b.HangBV, b.Status }).Distinct().ToList();

            rep.SL232.Value = qbnXNDom.Select(p => p.MaBNhan).Distinct().Count();
            rep.CCI232.Value = qbnXNDom.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacI232.Value = qbnXNDom.Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();

            #endregion

            #region II. Tình hình bệnh nhân điều trị nội trú
            var qkhoa = qkp.Where(p => p.PLoai.Equals("Lâm sàng")).ToList();
            var qbndt = (from k in qkhoa
                         join kb in bnkb on k.MaKP equals kb.MaKPdt
                         join b in bn.Where(p => p.NoiTru == 1).Where(p => p.NNhap >= tungay && p.NNhap <= denngay) on kb.MaBNhan equals b.MaBNhan
                         join t in qttbx on b.MaBNhan equals t.MaBNhan
                         join rv in qbnCV on b.MaBNhan equals rv.MaBNhan into kq
                         from kq1 in kq.DefaultIfEmpty()
                         select new { kb.MaBNhan, kb.MaICD, kb.PhuongAn, HangBV = kq1 == null ? 0 : kq1.HangBV, b.CapCuu, t.DTuongLao, KetQua = kq1 == null ? "" : kq1.KetQua, Status = kq1 == null ? 0 : kq1.Status }).ToList();

            rep.SLII11.Value = qbndt.Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII11.Value = qbndt.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII11.Value = qbndt.Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();

            var qdtHoHap = (from kb in qbndt
                            select new { kb.MaBNhan, kb.MaICD, kb.PhuongAn, kb.HangBV, kb.CapCuu, kb.KetQua, kb.Status }).ToList();

            #region đt bệnh đường hô hấp trên
            rep.SLII121.Value = qdtHoHap.Where(p => p.MaICD.Contains("J30") || p.MaICD.Contains("J31") || p.MaICD.Contains("J32") || p.MaICD.Equals("J33") || p.MaICD.Equals("J34")
                                                || p.MaICD.Equals("J35") || p.MaICD.Equals("J36") || p.MaICD.Equals("J37") || p.MaICD.Equals("J38") || p.MaICD.Equals("J39"))
                                                .Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII121.Value = qdtHoHap.Where(p => p.MaICD.Contains("J30") || p.MaICD.Contains("J31") || p.MaICD.Contains("J32") || p.MaICD.Equals("J33") || p.MaICD.Equals("J34")
                                                || p.MaICD.Equals("J35") || p.MaICD.Equals("J36") || p.MaICD.Equals("J37") || p.MaICD.Equals("J38") || p.MaICD.Equals("J39"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII121.Value = qdtHoHap.Where(p => p.MaICD.Contains("J30") || p.MaICD.Contains("J31") || p.MaICD.Contains("J32") || p.MaICD.Equals("J33") || p.MaICD.Equals("J34")
                                                || p.MaICD.Equals("J35") || p.MaICD.Equals("J36") || p.MaICD.Equals("J37") || p.MaICD.Equals("J38") || p.MaICD.Equals("J39"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region đt viêm phế quản
            rep.SLII122.Value = qdtHoHap.Where(p => p.MaICD.Contains("J20") || p.MaICD.Contains("J41") || p.MaICD.Equals("J68.0")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII122.Value = qdtHoHap.Where(p => p.MaICD.Contains("J20") || p.MaICD.Contains("J41") || p.MaICD.Equals("J68.0"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII122.Value = qdtHoHap.Where(p => p.MaICD.Contains("J20") || p.MaICD.Contains("J41") || p.MaICD.Equals("J68.0"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region đt viêm phổi
            rep.SLII123.Value = qdtHoHap.Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII123.Value = qdtHoHap.Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII123.Value = qdtHoHap.Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region đt hen
            rep.SLII124.Value = qdtHoHap.Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII124.Value = qdtHoHap.Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII124.Value = qdtHoHap.Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region COPD
            rep.SLII125.Value = qdtHoHap.Where(p => p.MaICD.Equals("J44")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII125.Value = qdtHoHap.Where(p => p.MaICD.Equals("J44"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII125.Value = qdtHoHap.Where(p => p.MaICD.Equals("J44"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region lao
            rep.SLII126.Value = qdtHoHap.Where(p => p.MaICD.Contains("A15") || p.MaICD.Contains("A16") || p.MaICD.Contains("A17") || p.MaICD.Contains("A18")
                                                || p.MaICD.Contains("A19")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII126.Value = qdtHoHap.Where(p => p.MaICD.Contains("A15") || p.MaICD.Contains("A16") || p.MaICD.Contains("A17") || p.MaICD.Contains("A18")
                                                || p.MaICD.Contains("A19"))
                                         .Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            rep.KhacII126.Value = qdtHoHap.Where(p => p.MaICD.Contains("A15") || p.MaICD.Contains("A16") || p.MaICD.Contains("A17") || p.MaICD.Contains("A18")
                                                || p.MaICD.Contains("A19"))
                                         .Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region hô hấp khác
            rep.SLII127.Value = Convert.ToDouble(rep.SL214.Value) + Convert.ToInt32(rep.SL223.Value);
            rep.CCII127.Value = Convert.ToDouble(rep.CCI214.Value) + Convert.ToInt32(rep.CCI223.Value);
            rep.KhacII127.Value = Convert.ToDouble(rep.KhacI214.Value) + Convert.ToInt32(rep.KhacI223.Value);
            #endregion
            #region bệnh nhân đt do bệnh hô hấp
            rep.SLII12.Value = Convert.ToDouble(rep.SLII121.Value) + Convert.ToDouble(rep.SLII122.Value) + Convert.ToDouble(rep.SLII123.Value) + Convert.ToDouble(rep.SLII124.Value)
                               + Convert.ToDouble(rep.SLII125.Value) + Convert.ToDouble(rep.SLII126.Value) + Convert.ToDouble(rep.SLII127.Value);
            rep.CCII12.Value = Convert.ToDouble(rep.CCII121.Value) + Convert.ToDouble(rep.CCII122.Value) + Convert.ToDouble(rep.CCII123.Value) + Convert.ToDouble(rep.CCII124.Value)
                               + Convert.ToDouble(rep.CCII125.Value) + Convert.ToDouble(rep.CCII126.Value) + Convert.ToDouble(rep.CCII127.Value);
            rep.KhacII12.Value = Convert.ToDouble(rep.KhacII121.Value) + Convert.ToDouble(rep.KhacII122.Value) + Convert.ToDouble(rep.KhacII123.Value) + Convert.ToDouble(rep.KhacII124.Value)
                               + Convert.ToDouble(rep.KhacII125.Value) + Convert.ToDouble(rep.KhacII126.Value) + Convert.ToDouble(rep.KhacII127.Value);
            #endregion
            #region tổng bn cấp cứu
            rep.SLII21.Value = qbndt.Where(p => p.CapCuu == 1).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII21.Value = qbndt.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            //rep.KhacII21.Value = qbndt.Where(p => p.CapCuu == 0 && p.Status == 1).Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
           
            #region cấp cứu hen
            rep.SLII221.Value = qdtHoHap.Where(p => p.CapCuu == 1).Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII221.Value = qdtHoHap.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46"))
                                        .Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region cấp cứu COPD
            rep.SLII222.Value = qdtHoHap.Where(p => p.CapCuu == 1).Where(p => p.MaICD.Equals("J44")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII222.Value = qdtHoHap.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.MaICD.Equals("J44"))
                                        .Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region cấp cứu lao
            rep.SLII223.Value = qdtHoHap.Where(p => p.CapCuu == 1).Where(p => p.MaICD.Contains("A15") || p.MaICD.Contains("A16") || p.MaICD.Contains("A17") || p.MaICD.Contains("A18")
                                                || p.MaICD.Contains("A19")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII223.Value = qdtHoHap.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.MaICD.Contains("A15") || p.MaICD.Contains("A16") || p.MaICD.Contains("A17") || p.MaICD.Contains("A18")
                                                || p.MaICD.Contains("A19"))
                                        .Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region cấp cứu viêm phổi
            rep.SLII224.Value = qdtHoHap.Where(p => p.CapCuu == 1).Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII224.Value = qdtHoHap.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69"))
                                        .Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            #region cấp cứu bệnh hô hấp khác
            rep.SLII225.Value = qdtHoHap.Where(p => p.CapCuu == 1).Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J42") || p.MaICD.Equals("J18.0") || p.MaICD.Equals("J18.8") | p.MaICD.Equals("C39.0")
                                              || p.MaICD.Equals("J39.9") || p.MaICD.Equals("J39.8") || p.MaICD.Equals("J39.3") ||
                                              p.MaICD.Equals("J41") || p.MaICD.Equals("J43") || p.MaICD.Equals("J47")).Select(p => p.MaBNhan).Distinct().Count();
            rep.CCII225.Value = qdtHoHap.Where(p => p.CapCuu == 1 && p.Status == 1).Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J42") || p.MaICD.Equals("J18.0") || p.MaICD.Equals("J18.8") | p.MaICD.Equals("C39.0")
                                              || p.MaICD.Equals("J39.9") || p.MaICD.Equals("J39.8") || p.MaICD.Equals("J39.3") ||
                                              p.MaICD.Equals("J41") || p.MaICD.Equals("J43") || p.MaICD.Equals("J47"))
                                        .Where(p => p.HangBV != 0 && p.HangBV < hangBVHT).Select(p => p.MaBNhan).Distinct().Count();
            #endregion

            #region bn cấp cứu do bệnh hô hấp
            rep.SLII22.Value = Convert.ToDouble(rep.SLII221.Value) + Convert.ToDouble(rep.SLII222.Value) + Convert.ToDouble(rep.SLII223.Value)
                               + Convert.ToDouble(rep.SLII224.Value) + Convert.ToDouble(rep.SLII225.Value);
            rep.CCII22.Value = Convert.ToDouble(rep.CCII221.Value) + Convert.ToDouble(rep.CCII222.Value) + Convert.ToDouble(rep.CCII223.Value)
                               + Convert.ToDouble(rep.CCII224.Value) + Convert.ToDouble(rep.CCII225.Value);
            #endregion

            rep.SLII31.Value = qbndt.Where(p => p.KetQua.Equals("Tử vong")).Select(p => p.MaBNhan).Distinct().Count();

            rep.SLII321.Value = qdtHoHap.Where(p => p.KetQua.Equals("Tử vong")).Where(p => p.MaICD.Contains("J45") || p.MaICD.Equals("J46")).Select(p => p.MaBNhan).Distinct().Count();

            rep.SLII322.Value = qdtHoHap.Where(p => p.KetQua.Equals("Tử vong")).Where(p => p.MaICD.Equals("J44")).Select(p => p.MaBNhan).Distinct().Count();

            rep.SLII323.Value = qdtHoHap.Where(p => p.KetQua.Equals("Tử vong")).Where(p => p.MaICD.Contains("A15") || p.MaICD.Contains("A16") || p.MaICD.Contains("A17") || p.MaICD.Contains("A18")
                                                || p.MaICD.Contains("A19")).Select(p => p.MaBNhan).Distinct().Count();

            rep.SLII324.Value = qdtHoHap.Where(p => p.KetQua.Equals("Tử vong")).Where(p => p.MaICD.Contains("J12") || p.MaICD.Equals("J13") || p.MaICD.Equals("J14") || p.MaICD.Contains("J15") | p.MaICD.Contains("J16")
                                              || p.MaICD.Contains("J17") || p.MaICD.Contains("J18") || p.MaICD.Equals("B01.2") || p.MaICD.Equals("B05.2") || p.MaICD.Equals("B20.6")
                                              || p.MaICD.Equals("B22.1") || p.MaICD.Equals("B25.0") || p.MaICD.Contains("J169") || p.MaICD.Equals("J10.0") || p.MaICD.Equals("J11.0")
                                              || p.MaICD.Contains("P23") || p.MaICD.Contains("J69")).Select(p => p.MaBNhan).Distinct().Count();

            rep.SLII325.Value = qdtHoHap.Where(p => p.KetQua.Equals("Tử vong")).Where(p => p.MaICD.Equals("J40") || p.MaICD.Equals("J42") || p.MaICD.Equals("J18.0") || p.MaICD.Equals("J18.8") | p.MaICD.Equals("C39.0")
                                              || p.MaICD.Equals("J39.9") || p.MaICD.Equals("J39.8") || p.MaICD.Equals("J39.3") ||
                                              p.MaICD.Equals("J41") || p.MaICD.Equals("J43") || p.MaICD.Equals("J47")).Select(p => p.MaBNhan).Distinct().Count();

            rep.SLII32.Value = Convert.ToDouble(rep.SLII321.Value) + Convert.ToDouble(rep.SLII322.Value) + Convert.ToDouble(rep.SLII323.Value)
                               + Convert.ToDouble(rep.SLII324.Value) + Convert.ToDouble(rep.SLII325.Value);

            var qbndtXNDom = (from b in bn
                              join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on b.MaBNhan equals cls.MaBNhan
                              join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              join dv in data.DichVus.Where(p => p.PLoai == 2) on cd.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)) on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new { b.MaBNhan, b.NoiTru }).Distinct().ToList();

            rep.SLIII1.Value = qbndtXNDom.Select(p => p.MaBNhan).Distinct().Count();
            rep.SLIII2.Value = qbndtXNDom.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            #endregion
            frmIn frm = new frmIn();
            //rep.DataSource = _listContent;
            //rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void radQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (radQuy.Checked)
            {
                cbbQuy.Enabled = true;
                cbbNam.Enabled = true;
                lupTuNgay.Enabled = false;
                lupDenNgay.Enabled = false;
            }
        }

        private void rdTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTheoNgay.Checked)
            {
                cbbQuy.Enabled = false;
                cbbNam.Enabled = false;
                lupTuNgay.Enabled = true;
                lupDenNgay.Enabled = true;
            }
        }

    }
}