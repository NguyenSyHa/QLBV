using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frm_danhmucthuocbv : DevExpress.XtraEditors.XtraForm
    {
        public frm_danhmucthuocbv()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_danhmucthuocbv_Load(object sender, EventArgs e)
        {

        }
        public void indanhmuc() {

            var danhmuc = (from h in data.DichVus select new { h.MaTam, h.TenDV, h.SoDK, h.SoTTqd, h.DonVi, h.DonGia, h.Status, h.IDNhom, h.IdTieuNhom, h.NuocSX, h.NhaSX, h.DuongD, h.HamLuong, h.TenHC, h.QCPC }).ToList();
            var nhomdichvu = (from h in data.NhomDVs.Where(p => p.TenNhomCT == "Thuốc trong danh mục BHYT" || p.TenNhomCT == "Vật tư y tế trong danh mục BHYT") select new { h.TenNhomCT, h.IDNhom }).ToList();
            if (radiothuoc.Checked == true){
                BaoCao.rep_DanhmucthuocSDCSDKCB baocaot = new BaoCao.rep_DanhmucthuocSDCSDKCB();
                int id = nhomdichvu.Where(p => p.TenNhomCT ==  "Thuốc trong danh mục BHYT" ).First().IDNhom;
                string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] _tieude = { "STT", "MA_THUOC_BV", "MA_THUOC", "HOAT_CHAT", "MA_DUONG_DUNG", "DUONG_DUNG", "HAM_LUONG", "TEN_THUOC", "SO_DANG_KY", "DONG_GOI", "DON_VI_TINH", "DON_GIA", "SO_LUONG", "MA_CSKCB" };
                int[] _arrWidth = new int[] { };
            if (radiotatca.Checked == true) 
                {
                    baocaot.DataSource = danhmuc.Where(p => p.IDNhom == id).OrderBy(p => p.TenDV).ToList();
                    #region xuat Excel

                    
                    DungChung.Bien.MangHaiChieu = new object[danhmuc.Where(p => p.IDNhom == id).ToList().Count + 3, _tieude.Length];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in danhmuc.Where(p => p.IDNhom == id).OrderBy(p => p.TenDV).ToList())
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num ;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoTTqd;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenHC;
                        DungChung.Bien.MangHaiChieu[num, 4] = "";
                        DungChung.Bien.MangHaiChieu[num, 5] = r.DuongD;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.HamLuong;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.SoDK;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.QCPC;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 12] = "";
                        DungChung.Bien.MangHaiChieu[num, 13] = DungChung.Bien.MaBV;
                        num++;

                    }


                    #endregion
                }
                else if (radiodangsudung.Checked == true)
                {

                    baocaot.DataSource = danhmuc.Where(p => p.IDNhom == id && p.Status == 1).OrderBy(p => p.TenDV).ToList();
                    #region xuat Excel


                    DungChung.Bien.MangHaiChieu = new object[danhmuc.Where(p => p.IDNhom == id).ToList().Count + 3, _tieude.Length];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in danhmuc.Where(p => p.IDNhom == id && p.Status == 1).OrderBy(p => p.TenDV).ToList())
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoTTqd;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenHC;
                        DungChung.Bien.MangHaiChieu[num, 4] = "";
                        DungChung.Bien.MangHaiChieu[num, 5] = r.DuongD;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.HamLuong;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.SoDK;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.QCPC;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 12] = "";
                        DungChung.Bien.MangHaiChieu[num, 13] = DungChung.Bien.MaBV;
                        num++;


                    }


                    #endregion
                 
                }
                else if (radiokhong.Checked==true)
                {
                    baocaot.DataSource = danhmuc.Where(p => p.IDNhom == id && p.Status == 0).OrderBy(p => p.TenDV).ToList();
                       #region xuat Excel


                    DungChung.Bien.MangHaiChieu = new object[danhmuc.Where(p => p.IDNhom == id).ToList().Count + 3, _tieude.Length];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in danhmuc.Where(p => p.IDNhom == id && p.Status == 0).OrderBy(p => p.TenDV).ToList())
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num ;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoTTqd;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenHC;
                        DungChung.Bien.MangHaiChieu[num, 4] = "";
                        DungChung.Bien.MangHaiChieu[num, 5] = r.DuongD;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.HamLuong;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.SoDK;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.QCPC;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 12] = "";
                        DungChung.Bien.MangHaiChieu[num, 13] = DungChung.Bien.MaBV;
                        num++;

                    }


                    #endregion
                
                }
                baocaot.inbaocao();
                baocaot.CreateDocument();
    
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Danh mục thuốc bệnh viện", "C:\\Danhmucthuoc.xls", true, this.Name);
                frm.prcIN.PrintingSystem = baocaot.PrintingSystem;
                frm.ShowDialog(); 
            
            }
            else if (radiovattu.Checked==true)
            {
                BaoCao.rep_danhmucVTYTT baocaot = new BaoCao.rep_danhmucVTYTT();
                int id = nhomdichvu.Where(p => p.TenNhomCT =="Vật tư y tế trong danh mục BHYT").First().IDNhom;
                string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] _tieude = { "STT", "MA_VTYT_BV", "MA_VTYT", "TEN_VTYT", "QUYCACH", "NUOC_SX", "HANG_SX", "DON_VI_TINH", "GIA_THAU", "GIA_BHTT", "DINH_MUC", "SO_LUONG", "MA_CSKCB" };

                int[] _arrWidth = new int[] { };
                if (radiotatca.Checked == true)
                {
                    baocaot.DataSource = danhmuc.Where(p => p.IDNhom == id).OrderBy(p => p.TenDV);
                    #region xuat Excel

                   
                    DungChung.Bien.MangHaiChieu = new object[danhmuc.Where(p => p.IDNhom == id).ToList().Count + 3, _tieude.Length];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in danhmuc.Where(p => p.IDNhom == id).OrderBy(p=>p.TenDV))
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num ;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoTTqd;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.QCPC;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.NuocSX;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.NhaSX;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 8] = "";
                        DungChung.Bien.MangHaiChieu[num, 9] = "";
                        DungChung.Bien.MangHaiChieu[num, 10] = "";
                        DungChung.Bien.MangHaiChieu[num, 11] = "";
                        DungChung.Bien.MangHaiChieu[num, 12] = DungChung.Bien.MaBV;
                        num++;

                    }


                    #endregion
                }
                else if (radiodangsudung.Checked == true)
                {

                    baocaot.DataSource = danhmuc.Where(p => p.IDNhom == id && p.Status == 1).OrderBy(p => p.TenDV);
                    #region xuat Excel


                    DungChung.Bien.MangHaiChieu = new object[danhmuc.Where(p => p.IDNhom == id).ToList().Count + 3, _tieude.Length];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in danhmuc.Where(p => p.IDNhom == id && p.Status == 1).OrderBy(p => p.TenDV))
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoTTqd;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.QCPC;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.NuocSX;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.NhaSX;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 8] = "";
                        DungChung.Bien.MangHaiChieu[num, 9] = "";
                        DungChung.Bien.MangHaiChieu[num, 10] = "";
                        DungChung.Bien.MangHaiChieu[num, 11] = "";
                        DungChung.Bien.MangHaiChieu[num, 12] = DungChung.Bien.MaBV;
                        num++;

                    }


                    #endregion

                }
                else if (radiokhong.Checked == true)
                {
                    baocaot.DataSource = danhmuc.Where(p => p.IDNhom == id && p.Status == 0).OrderBy(p => p.TenDV);
                    #region xuat Excel


                    DungChung.Bien.MangHaiChieu = new object[danhmuc.Where(p => p.IDNhom == id).ToList().Count + 3, _tieude.Length];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in danhmuc.Where(p => p.IDNhom == id && p.Status == 0).OrderBy(p => p.TenDV))
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoTTqd;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.QCPC;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.NuocSX;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.NhaSX;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 8] = "";
                        DungChung.Bien.MangHaiChieu[num, 9] = "";
                        DungChung.Bien.MangHaiChieu[num, 10] = "";
                        DungChung.Bien.MangHaiChieu[num, 11] = "";
                        DungChung.Bien.MangHaiChieu[num, 12] = DungChung.Bien.MaBV;
                        num++;

                    }


                    #endregion
                    

                }

                baocaot.inbaocao();
                baocaot.CreateDocument();
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Danh mục vật tư y tế ", "C:\\Danhmucvattu.xls", true, this.Name);
                frm.prcIN.PrintingSystem = baocaot.PrintingSystem;
                frm.ShowDialog(); 
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            indanhmuc();
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void hamxuatthuoc()
        {
 


        }
        public void hamxuatvattu()
        {



        }
    }
}