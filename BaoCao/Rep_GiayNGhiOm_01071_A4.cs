using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;

namespace QLBV.BaoCao
{
    public partial class Rep_GiayNGhiOm_01071_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        int _idkb = 0;
        public Rep_GiayNGhiOm_01071_A4(int idkb)
        {
            InitializeComponent();
            _idkb = idkb;
        }

        private void _InSubReport(XRSubreport repsub,string lien)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var kq = (from bnkb in _data.BNKBs.Where(p => p.IDKB == _idkb)
                      join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                      join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                      join ttbx in _data.TTboXungs on bnkb.MaBNhan equals ttbx.MaBNhan
                      join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.SThe,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bnkb.NgayHen,
                          bnkb.NgayNghi,
                          GTinh = (bn.GTinh == 1) ? "Nam" : "Nữ",
                          bnkb.ChanDoan,
                          bnkb.BenhKhac,
                          cb.TenCB,
                          ttbx.NoiLV,
                          bnkb.SoNghiOm,
                          kp.TenKP,
                          ttbx.NThan
                      }).ToList();
            int _mabn = 0;
            BaoCao.rep_NghiOm_TT56_01071 rep = new BaoCao.rep_NghiOm_TT56_01071();

            foreach (var item in kq)
            {
                if (item.NThan != null)
                {
                    if (item.NThan.Contains(";"))
                    {
                        string[] arrtt = item.NThan.Split(';');
                        if (arrtt.Length > 1 && !string.IsNullOrEmpty(arrtt[0]))
                        {
                            string[] arrttt = arrtt[0].Split(',');
                            if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                            {
                                rep.hotencha.Value = arrttt[0];
                                rep.hotenme.Value = arrttt[1];
                            }
                        }
                    }
                    else
                    {
                        if (item.NThan.Contains(","))
                        {
                            string[] arrttt = item.NThan.Split(',');
                            if (arrttt.Length > 1 && !string.IsNullOrEmpty(arrttt[0]))
                            {
                                rep.hotencha.Value = arrttt[0];
                                rep.hotenme.Value = arrttt[1];
                            }
                        }
                        else
                            rep.hotencha.Value = item.NThan;
                    }
                }

                _mabn = item.MaBNhan;
                rep.idkb.Value = "Số: " + _mabn;
                string ngaysinh = "";
                if (!String.IsNullOrEmpty(item.NgaySinh))
                {
                    ngaysinh += item.NgaySinh + "/";
                }
                if (!String.IsNullOrEmpty(item.ThangSinh))
                {
                    ngaysinh += item.ThangSinh + "/";
                }
                if (!String.IsNullOrEmpty(item.NamSinh))
                {
                    ngaysinh += item.NamSinh;
                }
                rep.hoten.Value = item.TenBNhan.ToUpper();
                rep.ngaysinh.Value = ngaysinh;
                rep.bhyt.Value = item.SThe;
                rep.gioitinh.Value = item.GTinh;
                rep.chandoan.Value = item.ChanDoan + "; " + item.BenhKhac;
                rep.bacsykcb.Value = item.TenCB;
                rep.PhongKham.Value = item.TenKP;
                rep.CQ.Value = DungChung.Bien.TenCQ.ToUpper();
                if (item.NgayHen != null && item.NgayNghi != null)
                {
                    int day = (item.NgayHen.Value.Date - item.NgayNghi.Value.Date).Days + 1;
                    if (day.ToString().Length == 1)
                    {
                        rep.songaynghi.Value = "0" + day + " ngày";
                    }
                    else
                        rep.songaynghi.Value = day + " ngày";
                    rep.khoangngay.Value = " (Từ ngày: " + item.NgayNghi.Value.ToShortDateString() + "    đến ngày: " + item.NgayHen.Value.ToShortDateString() + " )";
                }
                else rep.khoangngay.Value = " (Từ ngày: " + "                    đến hết ngày:                    " + " )";
                if (item.NoiLV != null && item.NoiLV != "")
                    rep.donvi.Value = "Đơn vị làm việc: " + item.NoiLV;
                else rep.donvi.Value = "Đơn vị làm việc: ";
                rep.soseri.Value = item.SoNghiOm;
                rep.ngay.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

            }
            rep.lien.Value = lien;
            repsub.ReportSource = rep;
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            _InSubReport(xrSubreport1, "Liên số 1");
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            _InSubReport(xrSubreport2, "Liên số 2");
        }
    }
}
