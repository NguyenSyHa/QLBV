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
    public partial class frm_BaoCaoCLSTrongNgay_12121 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoCLSTrongNgay_12121()
        {
            InitializeComponent();
        }

        List<DichVu> _ldvAll = new List<DichVu>();
        List<TieuNhomDV> qtn = new List<TieuNhomDV>();
        bool load = false;
        private void frm_BangTHCongNo_12121_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            //memoEdit1.Text = "Báo cáo cận lâm sàng trong ngày" + "\n" + " - Tick chọn tiểu nhóm dịch vụ rồi chọn các dịch vụ để hiển thị" + "\n" + 
            //    " - Có thể chọn hiển thị theo tiểu nhóm hoặc theo tên dịch vụ" + "\n" + " - Báo cáo lấy theo số lượt dịch vụ được thực hiện" + "\n" + " - Các khoa phòng được dải động, lần lượt theo nhóm khoa phòng lâm sàng rồi đến nhóm khoa phòng là phòng khám, thứ tự trong mỗi nhóm là theo tên khoa phòng ";
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            qtn = data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3).ToList();
            qtn.Insert(0, new TieuNhomDV { IdTieuNhom = 0, TenTN = "Tất cả" });
            cklTieuNhom.DataSource = qtn;

            for (int i = 0; i < cklTieuNhom.ItemCount; i++)
            {
                cklTieuNhom.SetItemChecked(i, true);
            }
            _ldvAll = (from tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3)
                       join dv in data.DichVus.Where(p => p.PLoai == 2) on tn.IdTieuNhom equals dv.IdTieuNhom
                       select dv).ToList();
            _ldvAll.Insert(0, new DichVu { MaDV = 0, TenDV = "Tất cả" });
            cklDichVu.DataSource = _ldvAll;
            for (int i = 0; i < cklDichVu.ItemCount; i++)
            {
                cklDichVu.SetItemChecked(i, true);
            }
            load = true;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);

            var qcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                        join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        select new { cls.MaKP, cd.MaDV }).ToList();
            List<int> lMaDV = new List<int>();
          
            for (int i = 0; i < cklDichVu.ItemCount; i++)
            {
                if (cklDichVu.GetItemCheckState(i) == CheckState.Checked)
                    lMaDV.Add(Convert.ToInt32(cklDichVu.GetItemValue(i)));
            }

            List<KPhong> lKp = data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng").OrderBy(p => p.PLoai).ToList();
            int[] lMaKP = new int[]{0,0,0,0,0,0,0,0};
             BaoCao.rep_BaoCaoCLSTrongNgay_12121 rep = new BaoCao.rep_BaoCaoCLSTrongNgay_12121(ckHTTieuNhom.Checked, ckHTDichVu.Checked);
                frmIn frm = new frmIn();
            for (int i = 1; i < 9; i++)
            {
                if (i <= lKp.Count)
                {
                    lMaKP[i - 1] = lKp.Skip(i - 1).Select(p => p.MaKP).FirstOrDefault();
                    switch(i)
                    {
                        case 1:
                            rep.celTit1.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 2:
                            rep.celTit2.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 3:
                            rep.celTit3.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 4:
                            rep.celTit4.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 5:
                            rep.celTit5.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 6:
                            rep.celTit6.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 7:
                            rep.celTit7.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                        case 8:
                            rep.celTit8.Text = lKp.Skip(i - 1).Select(p => p.TenKP).FirstOrDefault();
                            break;
                    }
                }
                else
                    break;
            }

            var qTH = (from cls in qcls
                       join madv in lMaDV on cls.MaDV equals madv
                       join dv in _ldvAll on madv equals dv.MaDV
                       join tn in qtn on dv.IdTieuNhom equals tn.IdTieuNhom
                       group new { cls, dv, tn } by new { tn.TenTN, dv.TenDV, tn.IdTieuNhom } into kq
                       select new
                       {
                           kq.Key.TenTN,
                           kq.Key.TenDV,                           
                           SoLuong1  = kq.Where(p=>p.cls.MaKP == lMaKP[0]).Count(),
                           SoLuong2  = kq.Where(p=>p.cls.MaKP == lMaKP[1]).Count(),
                           SoLuong3 = kq.Where(p => p.cls.MaKP == lMaKP[2]).Count(),
                           SoLuong4 = kq.Where(p => p.cls.MaKP == lMaKP[3]).Count(),
                           SoLuong5 = kq.Where(p => p.cls.MaKP == lMaKP[4]).Count(),
                           SoLuong6 = kq.Where(p => p.cls.MaKP == lMaKP[5]).Count(),
                           SoLuong7 = kq.Where(p => p.cls.MaKP == lMaKP[6]).Count(),
                           SoLuong8 = kq.Where(p => p.cls.MaKP == lMaKP[7]).Count(),
                       }).ToList();
            var qTH2 = (from q in qTH
                        select new { q.TenTN, q.TenDV, q.SoLuong1, q.SoLuong2, q.SoLuong3, q.SoLuong4, q.SoLuong5, q.SoLuong6, q.SoLuong7, q.SoLuong8, Cong = q.SoLuong1 + q.SoLuong2 + q.SoLuong3 + q.SoLuong4 + q.SoLuong5 + q.SoLuong6 + q.SoLuong7 + q.SoLuong8 }).OrderBy(p=>p.TenTN).ThenBy(p=>p.TenDV).ToList();
            if (qTH2.Count == 0)
                MessageBox.Show("Không có dữ liệu");
            else
            {
                
                if (DungChung.Ham.NgayTu(denngay) == tungay)
                    rep.celNgayThang.Text = "Trong ngày";
                else
                    rep.celNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = qTH2;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }



        private void cklTieuNhom_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (load)
            {
                if (e.Index == 0)
                {
                    if (cklTieuNhom.GetItemCheckState(0) == CheckState.Checked)
                    {
                        for (int i = 0; i < cklTieuNhom.ItemCount; i++)
                        {
                            cklTieuNhom.SetItemChecked(i, true);
                        }
                        for (int i = 0; i < cklDichVu.ItemCount; i++)
                        {
                            cklDichVu.SetItemChecked(i, true);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < cklTieuNhom.ItemCount; i++)
                        {
                            cklTieuNhom.SetItemChecked(i, false);
                        }
                        for (int i = 0; i < cklDichVu.ItemCount; i++)
                        {
                            cklDichVu.SetItemChecked(i, false);
                        }
                    }
                }
                else
                {
                    // Lấy ra tất cả MaDV được chọn
                    List<int> lMaDV= new List<int>();
                    for (int i = 0; i < cklDichVu.ItemCount; i++)
                    {
                        if (cklDichVu.GetItemCheckState(i) == CheckState.Checked)
                            lMaDV.Add(Convert.ToInt32(cklDichVu.GetItemValue(i)));
                    }

                    // Lấy ra tất cả IdTieuNhomDV được chọn
                    List<int> lIDTieuNhom = new List<int>();
                    for (int i = 0; i < cklTieuNhom.ItemCount; i++)
                    {
                        if (cklTieuNhom.GetItemCheckState(i) == CheckState.Checked)
                            lIDTieuNhom.Add(Convert.ToInt32(cklTieuNhom.GetItemValue(i)));
                    }


                    List<DichVu> ldv = (from dv in _ldvAll join tn in lIDTieuNhom on dv.IdTieuNhom equals tn select dv).ToList();
                    ldv.Insert(0, new DichVu { MaDV = 0, TenDV = "Tất cả" });
                    cklDichVu.DataSource = ldv;
                    for (int i = 0; i < cklDichVu.ItemCount; i++)
                    {
                        int maDV = Convert.ToInt32(cklDichVu.GetItemValue(i));
                        if (lMaDV.Contains(maDV))
                            cklDichVu.SetItemChecked(i, true);
                        else
                            cklDichVu.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void cklDichVu_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (load && e.Index == 0)
            {
                if (cklDichVu.GetItemCheckState(0) == CheckState.Checked)
                {
                    for (int i = 0; i < cklDichVu.ItemCount; i++)
                    {
                        cklDichVu.SetItemChecked(i, true);
                    }
                }
                else
                    for (int i = 0; i < cklDichVu.ItemCount; i++)
                    {
                        cklDichVu.SetItemChecked(i, false);
                    }
            }

        }

        private void ckHTTieuNhom_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckHTDichVu_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}