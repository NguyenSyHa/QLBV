using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    public partial class frmCPChuaTT : DevExpress.XtraEditors.XtraForm
    {
        public frmCPChuaTT()
        {
            InitializeComponent();
        }

                  
        private bool KTtaoBcChuaTT()
        {
            
            if (lupTuNgay.EditValue ==null) 
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboDoiTuong.Text)) {
                MessageBox.Show("Bạn chưa chọn đối tượng");
                cboDoiTuong.Focus();
                return false;
            }
          return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (ckQuy.Checked == true)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " QÚY I NĂM 2014";
                        break;
                    case 2:
                        quy = " QÚY II NĂM 2014 ";
                        break;
                    case 3:
                        quy = " QUÝ III NĂM 2014";
                        break;
                    case 4:
                        quy = " QÚY IV NĂM 2014";
                        break;
                }

            }
            else
            {
                quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }

        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(lupKhoaphong.EditValue.ToString());
            int makp = 0;
            int trongBH = 0;
            string _doituong = "";
            if (!string.IsNullOrEmpty(cboDoiTuong.Text))
            {
                _doituong = cboDoiTuong.Text;
            }
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            int _noitru = 0;
            if (lupKhoaphong.EditValue != null)
                makp = Convert.ToInt32( lupKhoaphong.EditValue);
            if (KTtaoBcChuaTT())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                //i = cmbNoiNgoaiTru.SelectedIndex;
                _noitru = radNoiTru.SelectedIndex;
                frmIn frm = new frmIn();
                BaoCao.repBNChuaTT rep = new BaoCao.repBNChuaTT();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                //.NoiNgoaiTru.Value = radNoiTru.Text;
                if (radNoiTru.SelectedIndex == 1)
                {
                    if (_doituong == "BHYT")
                    {
                        rep.noingoaitru.Value = "CHO NGƯỜI BỆNH BHYT ĐIỀU TRỊ NỘI TRÚ";
                        trongBH = 1;
                    }
                    else
                        rep.noingoaitru.Value = "CHO NGƯỜI BỆNH NHÂN DÂN ĐIỀU TRỊ NỘI TRÚ";
                }
                else
                {
                    if (_doituong == "BHYT")
                    {
                        trongBH = 1;
                        rep.noingoaitru.Value = "CHO NGƯỜI BỆNH BHYT ĐIỀU TRỊ NGOẠI TRÚ";
                    }
                    else
                        rep.noingoaitru.Value = "CHO NGƯỜI BỆNH NHÂN DÂN ĐIỀU TRỊ NGOẠI TRÚ";
                }
                rep.thoigian.Value = "từ ngày : "+ngaytu.ToShortDateString() + " đến ngày: " +ngayden.ToShortDateString() ;
                rep.TenCQ.Value = DungChung.Bien.TenCQ;
                rep.TenCQCQ.Value = "Công Ty CP giải pháp Phần Mềm Việt-VSSOFT";
                //select BN dùng dịch vụ nhưng chưa thanh toán.
                var q = (from dt in data.DThuocs
                          where (dt.MaKP== makp)
                          where !(from vp in data.VienPhis
                                  select vp.MaBNhan).Contains(dt.MaBNhan)
                          where (ngaytu <= dt.NgayKe && dt.NgayKe <= ngayden)
                          join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          where (bn.DTuong == _doituong && bn.NoiTru == _noitru)
                          join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                          select new {
                                MaBN = dt.MaBNhan,
                                TenBN = bn.TenBNhan,
                                DiaChi = bn.DChi,
                                DTuong = bn.DTuong,
                                NgayKe = dt.NgayKe,
                                IdDT = dt.IDDon,
                                KhoaPhong = kp.TenKP,
                                TenDV = dv.TenDV,
                                SoLuong = dtct.SoLuong,
                                DonVi = dtct.DonVi,
                                DonGia = dtct.DonGia,
                                ThanhTien = dtct.ThanhTien
                          }).ToList();
                rep.DataSource = q.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                #region xuat excel
            //    if (chkXuatExel.Checked)
            //    {
            //        COMExcel.Application exApp = new COMExcel.Application();
            //        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            //        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            //        exSheet.Activate();
            //        exSheet.Name = "bcCPChuaTT";
            //        int i = 0;
            //        string[] _arr = new string[6] { "MaBN", "TenBN", "KhoaPhong", "DoiTuong","DiaChi","NgayKe" };
            //        foreach (var item in _arr)
            //        {
            //            i++;
            //            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, i];
            //            r.Value2 = item.ToString();
            //            r.Columns.AutoFit();
            //        }
            //        int k = 1;
            //        foreach (var item in q)
            //        {
            //            k++;
            //            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[k, 1];
            //            r1.NumberFormat = "@";
            //            r1.Value2 = item.MaBN.ToString();
            //            r1.Columns.AutoFit();
            //            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[k, 2];
            //            r2.NumberFormat = "@";
            //            r2.Value2 = item.TenBN.ToString();
            //            r2.Columns.AutoFit();
            //            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[k, 3];
            //            r3.NumberFormat = "@";
            //            r3.Value2 = item.KhoaPhong.ToString();
            //            r3.Columns.AutoFit();
            //            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[k, 4];
            //            r4.NumberFormat = "@";
            //            r4.Value2 = item.DTuong.ToString();
            //            r4.Columns.AutoFit();
            //            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[k, 5];
            //            r5.NumberFormat = "@";
            //            r5.Value2 = item.DiaChi.ToString();
            //            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[k, 6];
            //            r6.NumberFormat = "@";
            //            r6.Value2 = item.NgayKe.ToString();
            //            r6.Columns.AutoFit();
            //        }
            //        exApp.Visible = true;
            //        exQLBV.SaveAs("C:\\BCChuaTT.xls",COMExcel.XlFileFormat.xlWorkbookNormal,null,null, false, false,
            //                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
            //                                        false, false, false, false, false);
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                //    }
                #endregion xuat excel
            }
            

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTsBcMau20_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kp = data.KPhongs.Where(k => k.Status == 1).OrderBy(k => k.TenKP).ToList();
            lupKhoaphong.Properties.DataSource = kp;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
        }

        private void lupTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== (Keys.Enter))
            {
                simpleButton1_Click(sender, e);
            }
        }

        private void lupDenNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== (Keys.Enter))
            {
                simpleButton1_Click(sender, e);
            }
        }
    }
}