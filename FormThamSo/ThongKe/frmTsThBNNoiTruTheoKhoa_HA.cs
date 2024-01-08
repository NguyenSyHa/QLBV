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
    public partial class frmTsThBNNoiTruTheoKhoa_HA : DevExpress.XtraEditors.XtraForm
    {
        public frmTsThBNNoiTruTheoKhoa_HA()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            else return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                frmIn frm = new frmIn();
                BaoCao.repThBNNoiTruTheoKhoa_HA rep = new BaoCao.repThBNNoiTruTheoKhoa_HA();
                rep.TuNgay.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                int _nt = 2;
                if (radNoiNgoaiTru.SelectedIndex == 0)//ngoại trú
                {
                    rep.TenBC.Value = "TỔNG HỢP BỆNH NHÂN KHÁM CHỮA BỆNH NGOẠI TRÚ THEO KHOA PHÒNG";
                    _nt = 0;
                }
                if (radNoiNgoaiTru.SelectedIndex == 1)//nội trú
                {
                    rep.TenBC.Value = "TỔNG HỢP BỆNH NHÂN NỘI TRÚ KHÁM BỆNH THEO KHOA PHÒNG";
                    _nt = 1;
                }
                if (radNoiNgoaiTru.SelectedIndex == 2)//cả hai
                {
                    rep.TenBC.Value = "TỔNG HỢP BỆNH NHÂN KHÁM BỆNH THEO KHOA PHÒNG";
                    _nt = 2;
                }
                #region Chọn khoa phòng
                int _kho = 0;
                _kho = Convert.ToInt32(lupKhoa.EditValue);
                #region Không thống kê những BN chuyển PK
                if (radioGroup1.SelectedIndex == 0)//Lươt KB, Ko thống kê BN chuyển PK
                {
                    var id = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay)
                                                   .Where(p => _kho == 0 || p.MaKP == _kho).Where(p => p.PhuongAn != 3)
                              group kb by kb.MaBNhan into kq
                              select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                    var qbn1 = (from q in id
                                join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) on q.IDKB equals bnkb.IDKB
                                join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                                select new { bn.MaBNhan, bn.Tuoi, bn.GTinh, bn.NoiTru, kp.TenKP }).ToList();
                    var qbn = ((from bn in qbn1.Where(p => _nt == 2 || p.NoiTru == _nt)
                                group bn by new { bn.TenKP } into kq
                                select new
                                {
                                    TenKP = kq.Key.TenKP,
                                    TongSo = kq.Select(p => p.MaBNhan).Distinct().Count(),// == 0 ? "": kq.Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Distinct().Count(),//==null?0:kq.Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                    TE15 = kq.Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Distinct().Count(),
                                    T60 = kq.Where(p => p.Tuoi > 60).Where(p => p.Tuoi <= 80).Select(p => p.MaBNhan).Distinct().Count(),
                                    T80 = kq.Where(p => p.Tuoi > 80).Select(p => p.MaBNhan).Distinct().Count(),
                                    TongSoNu = kq.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count(),
                                    TongSoNam = kq.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Distinct().Count()
                                }).ToList());//.Select(x => new { x.TenKP,x.TongSo=x.TongSo==0?0:x.TongSo});

                    rep.DataSource = qbn;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region Thống kê BN chuyển PK
                else if (radioGroup1.SelectedIndex == 1)//Thống kê BN chuyển PK
                {
                    var qbn1 = (from bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay).Where(p => _kho == 0 || p.MaKP == _kho)
                                join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                                select new { bn.MaBNhan, bn.Tuoi, bn.GTinh, bn.NoiTru, kp.TenKP }).ToList();
                    var qbn = ((from bn in qbn1.Where(p => _nt == 2 || p.NoiTru == _nt)
                                group bn by new { bn.TenKP } into kq
                                select new
                                {
                                    TenKP = kq.Key.TenKP,
                                    TongSo = kq.Select(p => p.MaBNhan).Distinct().Count(),
                                    TE6 = kq.Where(p => p.Tuoi <= 6).Select(p => p.MaBNhan).Distinct().Count(),
                                    TE15 = kq.Where(p => p.Tuoi > 6).Where(p => p.Tuoi <= 15).Select(p => p.MaBNhan).Distinct().Count(),
                                    T60 = kq.Where(p => p.Tuoi > 60).Where(p => p.Tuoi <= 80).Select(p => p.MaBNhan).Distinct().Count(),
                                    T80 = kq.Where(p => p.Tuoi > 80).Select(p => p.MaBNhan).Distinct().Count(),
                                    TongSoNu = kq.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count(),
                                    TongSoNam = kq.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Distinct().Count()

                                }).ToList());

                    rep.DataSource = qbn;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
                #endregion
                #endregion
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTsThBNNoiTruTheoKhoa_HA_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.Focus();
            List<KPhong> _lkp = new List<KPhong>();
            _lkp = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
                    select kp).ToList();
            _lkp.Add(new KPhong { TenKP = "Tất cả ", MaKP = 0 }); //
            _lkp.OrderBy(p => p.TenKP);
            if (_lkp.Count > 0)
            {
                lupKhoa.Properties.DataSource = _lkp;
            }
        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}