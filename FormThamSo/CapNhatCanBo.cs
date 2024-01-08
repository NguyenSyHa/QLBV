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
    public partial class CapNhatCanBo : DevExpress.XtraEditors.XtraForm
    {
        public CapNhatCanBo()
        {
            InitializeComponent();
        }
        
        private void btnupdate_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<CanBo> _lcb = data.CanBoes.ToList();                                                                                                                                                                                    
            DialogResult Result = MessageBox.Show("Trước khi chạy cập nhật cần kiểm tra dấu cách trong tên,\n chứng chỉ hành nghề phải giống nhau của những cán bộ trùng ?\nChạy cập nhật ngay ?", "Hỏi cập nhật", MessageBoxButtons.YesNo);
            if (Result == DialogResult.Yes)
            {
                try
                {
                    List<TamUng> _lTamUng = data.TamUngs.ToList();
                    List<SoBienLai> _lSoBienLai = data.SoBienLais.ToList();
                    List<CLSct> _lCLSct = data.CLScts.ToList();
                    List<BBHC> _lBBHC = data.BBHCs.ToList();
                    List<BNKB> _lBNKB = data.BNKBs.ToList();
                    List<HSHuy> _lHSHuy = data.HSHuys.ToList();
                    List<RaVien> _lRaVien = data.RaViens.ToList();
                    List<KhoaDL> _lKhoaDL = data.KhoaDLs.ToList();
                    List<TTTH> _lTTTH = data.TTTHs.ToList();
                    List<NhapD> _lNhapD = data.NhapDs.ToList();
                    List<DThuocct> _lDThuocct = data.DThuoccts.ToList();
                    List<DThuoc> _lDThuoc = data.DThuocs.ToList();
                    List<BenhNhan> _lBenhNhan = data.BenhNhans.ToList();
                    List<VienPhict> _lVienPhict = data.VienPhicts.ToList();
                    List<CL> _lCLS = data.CLS.ToList();
                    List<DienBien> _lDienBien = data.DienBiens.ToList();
                    List<ADMIN> _lADMIN = data.ADMINs.ToList();
                    List<DThuocMau> _lDThuocMau = data.DThuocMaus.ToList();
                    List<VienPhi> _lVienPhi = data.VienPhis.ToList();

                    List<CanBobak> _lcbbak = data.CanBobaks.ToList();
                    foreach (var item in _lcbbak)
                    {
                        string MaCbNew = item.MaCB;
                        var cb = _lcb.Where(p => p.MaCBBak == item.MaCB).ToList();
                        if (cb.Count > 1)
                        {
                            List<string> Macbcu = cb.Select(p => p.MaCB).ToList();
                            //1
                            var UpdateTamUng = (from a in _lTamUng
                                                join b in Macbcu on a.MaCB equals b
                                                select a).ToList();
                            foreach (var i in UpdateTamUng)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //2
                            //var UpdatePhanUngT = (from a in _lPhanUngT
                            //                    join b in Macbcu on a.MaCB equals b
                            //                    select a).ToList();
                            //foreach (var i in UpdatePhanUngT)
                            //{
                            //    i.MaCB = MaCbNew;
                            //}
                            //data.SaveChanges();
                            //3
                            var UpdateSoBienLai = (from a in _lSoBienLai
                                                   join b in Macbcu on a.MaCB equals b
                                                   select a).ToList();
                            foreach (var i in UpdateSoBienLai)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //4
                            //var UpdateDuTru = (from a in _lDuTru
                            //                       join b in Macbcu on a.MaCB equals b
                            //                       select a).ToList();
                            //foreach (var i in UpdateDuTru)
                            //{
                            //    i.MaCB = MaCbNew;
                            //}
                            //data.SaveChanges();
                            //5
                            var UpdateCLSct = (from a in _lCLSct
                                               join b in Macbcu on a.MaCB equals b
                                               select a).ToList();
                            foreach (var i in UpdateCLSct)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //6
                            var UpdateBBHC = (from a in _lBBHC
                                              join b in Macbcu on a.MaCB equals b
                                              select a).ToList();
                            foreach (var i in UpdateBBHC)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //7
                            var UpdateHSHuy = (from a in _lHSHuy
                                               join b in Macbcu on a.MaCB equals b
                                               select a).ToList();
                            foreach (var i in UpdateHSHuy)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //8
                            var UpdateRaVien = (from a in _lRaVien
                                                join b in Macbcu on a.MaCB equals b
                                                select a).ToList();
                            foreach (var i in UpdateRaVien)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //9
                            var UpdateKhoaDL = (from a in _lKhoaDL
                                                join b in Macbcu on a.MaCB equals b
                                                select a).ToList();
                            foreach (var i in UpdateKhoaDL)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //10
                            var UpdateTTTH = (from a in _lTTTH
                                              join b in Macbcu on a.MaCB equals b
                                              select a).ToList();
                            foreach (var i in UpdateTTTH)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //11
                            //var UpdateHis_ExPort = (from a in _lHis_ExPort
                            //                  join b in Macbcu on a.MaCB equals b
                            //                  select a).ToList();
                            //foreach (var i in UpdateHis_ExPort)
                            //{
                            //    i.MaCB = MaCbNew;
                            //}
                            //data.SaveChanges();
                            //12
                            var UpdateNhapD = (from a in _lNhapD
                                               join b in Macbcu on a.MaCB equals b
                                               select a).ToList();
                            foreach (var i in UpdateNhapD)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //13
                            var UpdateDThuocct = (from a in _lDThuocct
                                                  join b in Macbcu on a.MaCB equals b
                                                  select a).ToList();
                            foreach (var i in UpdateDThuocct)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //14
                            var UpdateDThuoc = (from a in _lDThuoc
                                                join b in Macbcu on a.MaCB equals b
                                                select a).ToList();
                            foreach (var i in UpdateDThuoc)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //15
                            var UpdateBenhNhan = (from a in _lBenhNhan
                                                  join b in Macbcu on a.MaCB equals b
                                                  select a).ToList();
                            foreach (var i in UpdateBenhNhan)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //16
                            //var UpdateChiPhiDV = (from a in _lChiPhiDV
                            //                      join b in Macbcu on a.MaCB equals b
                            //                      select a).ToList();
                            //foreach (var i in UpdateChiPhiDV)
                            //{
                            //    i.MaCB = MaCbNew;
                            //}
                            //data.SaveChanges();
                            //17
                            var UpdateVienPhict = (from a in _lVienPhict
                                                   join b in Macbcu on a.MaCB equals b
                                                   select a).ToList();
                            foreach (var i in UpdateVienPhict)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //18
                            var UpdateDienBien = (from a in _lDienBien
                                                  join b in Macbcu on a.MaCB equals b
                                                  select a).ToList();
                            foreach (var i in UpdateDienBien)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //19
                            var UpdateADMIN = (from a in _lADMIN
                                               join b in Macbcu on a.MaCB equals b
                                               select a).ToList();
                            foreach (var i in UpdateADMIN)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //20
                            var UpdateDThuocMau = (from a in _lDThuocMau
                                                   join b in Macbcu on a.MaCB equals b
                                                   select a).ToList();
                            foreach (var i in UpdateDThuocMau)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                            //21
                            var UpdateVienPhi = (from a in _lVienPhi
                                                 join b in Macbcu on a.MaCB equals b
                                                 select a).ToList();
                            foreach (var i in UpdateVienPhi)
                            {
                                i.MaCB = MaCbNew;
                            }
                            data.SaveChanges();
                        }
                    }
                    MessageBox.Show("Update cán bộ thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Update: " + ex.ToString());
                }

            }
        }

        private void CapNhatCanBo_Load(object sender, EventArgs e)
        {

        }

        private void btnbackup_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<CanBo> _lcb = data.CanBoes.ToList();
            var cbbak = data.CanBobaks.ToList();
            if (cbbak.Count > 0)
            {
                MessageBox.Show("Đã chạy backup cán bộ");

            }
            else
            {
                DialogResult Result = MessageBox.Show("Trước khi chạy cập nhật cần kiểm tra dấu cách trong tên,\n chứng chỉ hành nghề phải giống nhau của những cán bộ trùng ?\nChạy cập nhật ngay ?", "Hỏi cập nhật", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    try
                    {
                        var _lcbcx = (from a in _lcb
                                      group a by new { a.TenCB, a.MaCCHN } into kq
                                      select new { kq.Key.TenCB, kq.Key.MaCCHN }).Distinct().ToList();
                        foreach (var item in _lcbcx)
                        {
                            CanBo Sua = _lcb.Where(p => p.TenCB == item.TenCB && p.MaCCHN == item.MaCCHN).FirstOrDefault();
                            CanBobak moi = new CanBobak();
                            moi.MaCB = Sua.MaCB;
                            moi.TenCB = Sua.TenCB;
                            moi.Status = Sua.Status;
                            moi.MaKP = Sua.MaKP;
                            moi.MaCCHN = Sua.MaCCHN;
                            moi.Khoa = Sua.Khoa;
                            moi.MaDT = Sua.MaDT;
                            moi.GioiTinh = Sua.GioiTinh;
                            moi.FileAnh = Sua.FileAnh;
                            moi.DiaChi = Sua.DiaChi;
                            moi.CapBac = Sua.CapBac;
                            moi.ChucVu = Sua.ChucVu;
                            moi.MaKPsd = Sua.MaKPsd;
                            moi.SoDT = Sua.SoDT;
                            moi.BangCap = Sua.BangCap;
                            data.CanBobaks.Add(moi);
                            var _lcbthua = _lcb.Where(p => p.TenCB == item.TenCB && p.MaCCHN == item.MaCCHN).ToList();
                            foreach (var i in _lcbthua)
                            {
                                i.MaCBBak = Sua.MaCB;
                            }
                            data.SaveChanges();
                        }

                        List<CanBobak> _lcbbak1 = data.CanBobaks.ToList();
                        foreach (var item in _lcbbak1)
                        {
                            var cb = _lcb.Where(p => p.MaCBBak == item.MaCB).ToList();
                            if (cb.Count > 1)
                            {
                                string dskp = "";
                                foreach (var i in cb)
                                {
                                    dskp += i.MaKPsd;
                                }
                                item.MaKPsd = dskp;
                            }
                        }
                        data.SaveChanges();
                        MessageBox.Show("Backup thành công");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Bảng cán bộ đã đc back up");
                    }
                }
            }
        }
    }
}