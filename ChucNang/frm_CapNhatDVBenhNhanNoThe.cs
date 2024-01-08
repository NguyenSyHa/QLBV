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
    public partial class frm_CapNhatDVBenhNhanNoThe : DevExpress.XtraEditors.XtraForm
    {
        bool load = true;
      
        int _MaBN = 0;// giá trị mã bệnh nhân truyền vào // constant
        bool ktraBNBHYT = false;// kiểm tra bệnh nhân BHYT
        List<HT> qdtCapNhat = new List<HT>();
        ///1: Cập nhật từ BN dịch vụ sang BN bảo hiểm; 2: Cập nhật từ BN BH sang BN dịch vụ
        int _status = 1;

        public frm_CapNhatDVBenhNhanNoThe()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maBN"></param>
        /// <param name="status">1: Cập nhật từ BN dịch vụ sang BN bảo hiểm; 2: Cập nhật từ BN BH sang BN dịch vụ</param>
        public frm_CapNhatDVBenhNhanNoThe(int maBN, int status)
        {
            _MaBN = maBN;
            _status = status;
            InitializeComponent();
        }
        DateTime _ghthetu = DateTime.Now;
        DateTime _ghtheden = DateTime.Now;
        private void frm_CapNhatDVBenhNhanNoThe_Load(object sender, EventArgs e)
        {
            if (_status == 2)
            {
                this.Text = "Cập nhật chi phí trong danh mục sang ngoài danh mục";
                ckcChekgh.Visible = false;
            }
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            //Kiểm tra bệnh nhân BHYT
            try
            {
                var qbn = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN) join dtbn in data.DTBNs.Where(p => p.DTBN1 == "BHYT") on bn.IDDTBN equals dtbn.IDDTBN select bn).ToList();
                if (qbn.Count > 0)
                {
                    ktraBNBHYT = true;
                    if (qbn.First().HanBHTu != null)
                        _ghthetu = qbn.First().HanBHTu.Value;
                    if (qbn.First().HanBHDen != null)
                        _ghtheden = qbn.First().HanBHDen.Value;
                }
            }
            catch (Exception ex)
            {

            }
           
            List<KPhong> _lkp = new List<KPhong>();
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).OrderBy(p => p.PLoai).ToList();
            _lkp.Insert(0, new KPhong { TenKP = "Tất cả", MaKP = 0 });
            lupKP.Properties.DataSource = _lkp;

            //if (DungChung.Bien.MaBV == "01071"|| DungChung.Bien.MaBV == "01049")
            //{
            //if (DungChung.Bien.PLoaiKP == "Admin" || DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {

                //lupKP.Properties.ReadOnly = false;
                lupKP.EditValue = 0;
            }
            else
            {
                //lupKP.Properties.ReadOnly = true;
                lupKP.EditValue = DungChung.Bien.MaKP;
            }
            //}

            loadChiTietDV(_MaBN);
        }

      
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maBN"></param>
        /// <param name="trongBH">0: trong bảo hiểm = 0; 1: trong bảo hiểm =1 => đã sửa</param>
        private void  loadChiTietDV(int maBN)
        {
          
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);   
            qdtCapNhat = new List<HT>();
            //Nếu bệnh nhân đã thanh toán thì không lấy
            VienPhi vp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();

            int Makp = 0;
            if(lupKP.EditValue != null)
            {
                Makp = Convert.ToInt32(lupKP.EditValue);
            }
            bool ckc = ckcChekgh.Checked;
            if ((ktraBNBHYT == false && _status == 1) || (ktraBNBHYT == true && _status == 2) || vp != null)
            {
                if (_status == 1)
                    MessageBox.Show("Bệnh nhân chưa bổ xung thẻ BHYT");
                else if (_status == 2)
                    MessageBox.Show("Bệnh nhân chưa đc chuyển thành bệnh nhân dịch vụ");
                return;
            }
            else
            {
                #region _status = 1
                if (_status == 1)
                {
                    var qdt = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBN).Where(p => p.PLDV == 2 ? true : ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") && p.PLDV == 1))
                               join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                               join dtct in data.DThuoccts.Where(p => (Makp == 0) ? true : p.MaKP == Makp).Where(p => ckc == true ? (p.NgayNhap <= _ghtheden && p.NgayNhap >= _ghthetu) : true) on dt.IDDon equals dtct.IDDon
                               join dv in data.DichVus//.Where(p=>p.PLoai == 2) 
                               on dtct.MaDV equals dv.MaDV
                               join n in data.NhomDVs on dv.IDNhom equals n.IDNhom
                               where (dtct.TrongBH == 0 && dtct.ThanhToan == 0)// trừ những chi phí trong danh mục và chi phí thu thẳng
                               select new { dt.PLDV, dt.IDDon, dtct.NgayNhap, dv.TenDV, n.TenNhomCT, dv.MaDV, kp.MaKP, kp.TenKP, dtct.IDDonct, dtct.DonGia, dtct.SoLuong, dtct.TrongBH, dtct.ThanhTien, dtct.TienBH, dtct.TienBN, dtct.TienChenh }).ToList();

                    qdtCapNhat = (from dt in qdt
                                  select new HT
                                  {
                                      ckChon = true,// dt.TenNhomCT == "Khám bệnh" ? false :  
                                      PLDV = dt.PLDV,
                                      IDDon = dt.IDDon,
                                      IDDonct = dt.IDDonct,
                                      TenKP = dt.TenKP,
                                      NgayKe = dt.NgayNhap.Value.ToString("dd/MM/yyyy"),
                                      TenDV = dt.TenDV,
                                      MaDV = dt.MaDV,
                                      SoLuong = dt.SoLuong,
                                      DonGia = dt.DonGia,
                                      ThanhTien = dt.ThanhTien,
                                      TienBH = dt.TienBH,
                                      TienBN = dt.TienBN,
                                      TrongBH = dt.TrongBH == 1 ? "Trong DM" : (dt.TrongBH == 0 ? "Ngoài DM" : ""),
                                  }).ToList();

                    grc_ChiTietThuoc_DV.DataSource = qdtCapNhat.OrderBy(p => p.IDDon).ThenBy(p => p.TenDV).ToList();


                    #region hiển thị dịch vụ CLS chưa thực hiện
                    var qdtAll = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBN).Where(p => p.PLDV == 2)
                                  join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                                  select new { dtct.IDCD }).Where(p => p.IDCD != null).Select(p => p.IDCD.Value).Distinct().ToList();
                    var qcls = (from cls in data.CLS.Where(panelControl1 => panelControl1.MaBNhan == maBN)
                                join kp in data.KPhongs.Where(p => (Makp == 0) ? true : p.MaKP == Makp) on cls.MaKP equals kp.MaKP
                                join cd in data.ChiDinhs.Where(p => (p.SoPhieu == null || p.SoPhieu == 0) && p.TrongBH == 0) on cls.IdCLS equals cd.IdCLS
                                join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                where (qdtAll.Count == 0 || !qdtAll.Contains(cd.IDCD))
                                select new { ckChon = true, cd.IDCD, kp.TenKP, dv.TenDV, cd.DonGia, TrongBH = cd.TrongBH == 1 ? "Trong DM" : (cd.TrongBH == 0 ? "Ngoài DM" : ""), dv.MaDV, cls.NgayThang }).ToList();
                    grcCLS.DataSource = qcls.OrderBy(p => p.IDCD).ThenBy(p => p.TenDV).ToList();
                    #endregion
                }
                #endregion
                #region _status =2
                else
                {
                    var qdt = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBN).Where(p => p.PLDV == 2 ? true : ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") && p.PLDV == 1))
                               join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                               join dtct in data.DThuoccts.Where(p => (Makp == 0) ? true : p.MaKP == Makp) on dt.IDDon equals dtct.IDDon
                               join dv in data.DichVus//.Where(p=>p.PLoai == 2) 
                               on dtct.MaDV equals dv.MaDV
                               join n in data.NhomDVs on dv.IDNhom equals n.IDNhom
                               where (dtct.ThanhToan == 0)
                               where (dtct.TrongBH == 1 || dtct.TrongBH == 3)// trừ những chi phí trong danh mục và chi phí thu thẳng
                               select new { dt.PLDV, n.TenNhomCT, dt.IDDon, dtct.NgayNhap, dv.TenDV, dv.MaDV, kp.MaKP, kp.TenKP, dtct.IDDonct, dtct.DonGia, dtct.SoLuong, dtct.TrongBH, dtct.ThanhTien, dtct.TienBH, dtct.TienBN, dtct.TienChenh }).ToList();

                    qdtCapNhat = (from dt in qdt
                                  select new HT
                                  {
                                      ckChon = true,//dt.TenNhomCT == "Khám bệnh" ? false : 
                                      PLDV = dt.PLDV,
                                      IDDon = dt.IDDon,
                                      IDDonct = dt.IDDonct,
                                      TenKP = dt.TenKP,
                                      NgayKe = dt.NgayNhap.Value.ToString("dd/MM/yyyy"),
                                      TenDV = dt.TenDV,
                                      MaDV = dt.MaDV,
                                      SoLuong = dt.SoLuong,
                                      DonGia = dt.DonGia,
                                      ThanhTien = dt.ThanhTien,
                                      TienBH = dt.TienBH,
                                      TienBN = dt.TienBN,
                                      TrongBH = dt.TrongBH == 1 ? "Trong DM" : (dt.TrongBH == 0 ? "Ngoài DM" : ""),
                                  }).ToList();

                    grc_ChiTietThuoc_DV.DataSource = qdtCapNhat.OrderBy(p => p.IDDon).ThenBy(p => p.TenDV).ToList();

                    #region hiển thị dịch vụ CLS chưa thực hiện
                    var qdtAll = (from dt in data.DThuocs.Where(p => p.MaBNhan == maBN).Where(p => p.PLDV == 2)
                                  join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                                  select new { dtct.IDCD }).Where(p => p.IDCD != null).Select(p => p.IDCD.Value).Distinct().ToList();
                    var qcls = (from cls in data.CLS.Where(panelControl1 => panelControl1.MaBNhan == maBN)
                                join kp in data.KPhongs.Where(p => (Makp == 0) ? true : p.MaKP == Makp) on cls.MaKP equals kp.MaKP
                                join cd in data.ChiDinhs.Where(p => (p.SoPhieu == null || p.SoPhieu == 0) && p.TrongBH == 1) on cls.IdCLS equals cd.IdCLS
                                join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                where (qdtAll.Count == 0 || !qdtAll.Contains(cd.IDCD))
                                select new { ckChon = true, cd.IDCD, kp.TenKP, dv.TenDV, cd.DonGia, TrongBH = cd.TrongBH == 1 ? "Trong DM" : (cd.TrongBH == 0 ? "Ngoài DM" : ""), dv.MaDV, cls.NgayThang }).ToList();
                    grcCLS.DataSource = qcls.OrderBy(p => p.IDCD).ThenBy(p => p.TenDV).ToList();
                    #endregion
                }
                #endregion
            }
        }
        public class HT
        {
            public bool ckChon { set; get; }
            public int IDDon { set; get; }
            public int IDDonct { set; get; }
            public string TenKP { set; get; }
            public string NgayKe { set; get; }
            public int MaDV { set; get; }
            public string TenDV { set; get; }
            public double SoLuong { set; get; }
            public double DonGia { set; get; }
            public double ThanhTien { set; get; }
            public double TienBH { set; get; }
            public double TienBN { set; get; }
            public string TrongBH { set; get; }

            public int? PLDV { get; set; }
        }

        private void ckCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCheckAll.Checked)
            {
                ckDelSelect.Checked = false;
                for (int i = 0; i < grv_ChiTietThuoc_DV.RowCount; i++)
                {
                    grv_ChiTietThuoc_DV.SetRowCellValue(i, colCk, true);
                }
                for (int i = 0; i < grvCLS.RowCount; i++)
                {
                    grvCLS.SetRowCellValue(i, colChonCLS, true);
                }
            }
        }

        private void ckDelSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDelSelect.Checked)
            {
                ckCheckAll.Checked = false;
                for (int i = 0; i < grv_ChiTietThuoc_DV.RowCount; i++)
                {
                    grv_ChiTietThuoc_DV.SetRowCellValue(i, colCk, false);
                }
                for (int i = 0; i < grvCLS.RowCount; i++)
                {
                    grvCLS.SetRowCellValue(i, colChonCLS, false);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int count = 0;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _list = (from dt in data.DThuocs.Where(p => p.MaBNhan == _MaBN)
                         join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                         join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                         select new { dt.PLDV, dv.DonGia, dv.DonGiaBHYT, dv.TenDV, dtct.IDDonct, dtct.MaDV, dv.TrongDM, dv.DonGia2, dv.DonGiaDV2, dtct.TyLeTT, dv.DonGiaTT15, dv.DonGiaTT39 }).ToList();
            var _lChiDinh = (from dt in data.CLS.Where(p => p.MaBNhan == _MaBN)
                             join dtct in data.ChiDinhs on dt.IdCLS equals dtct.IdCLS
                             join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                             select new { dv.DonGia, dv.DonGiaBHYT, dv.TenDV,dtct.IDCD,  dtct.MaDV, dv.TrongDM, dv.DonGia2, dv.DonGiaDV2, dv.DonGiaTT15, dv.DonGiaTT39 }).ToList();

            var _ldvCongKham = (from tn in data.TieuNhomDVs.Where(p => p.IDNhom == 13)
                                join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                select new { dv.MaDV }).Distinct().ToList();
            DateTime ngaynhap = DateTime.Now;
            BenhNhan bn = data.BenhNhans.Where(p=>p.MaBNhan == _MaBN).FirstOrDefault();
            int giacu = DungChung.Ham.GiaCu(_MaBN, 1);
            if(bn != null)
                ngaynhap = bn.NNhap.Value;
            for (int i = 0; i < grv_ChiTietThuoc_DV.RowCount; i++)
            {
                int iddonct = 0;
                int madv = 0;
                if (grv_ChiTietThuoc_DV.GetRowCellValue(i, colCk) != null && grv_ChiTietThuoc_DV.GetRowCellValue(i, colDonct) != null && grv_ChiTietThuoc_DV.GetRowCellValue(i, colMaDV) != null)
                { 
                    if(Convert.ToBoolean(grv_ChiTietThuoc_DV.GetRowCellValue(i, colCk)))
                    {
                        iddonct = Convert.ToInt32(grv_ChiTietThuoc_DV.GetRowCellValue(i,colDonct));
                        var q = _list.Single(p => p.IDDonct == iddonct);
                        madv = q.MaDV ?? 0;
                        DThuocct dthuocct = data.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                        var KtraCongKham = _ldvCongKham.Where(p => p.MaDV == madv).FirstOrDefault();

                        if (dthuocct != null) 
                        {
                            if (KtraCongKham != null)
                            {
                                if (dthuocct.ThanhToan == 0)
                                {
                                    DateTime ngaykham = Convert.ToDateTime(dthuocct.NgayNhap);
                                    int idkb = dthuocct.IDKB;
                                    data.DThuoccts.Remove(dthuocct);
                                    data.SaveChanges();
                                    //DungChung.Ham.Update_Delete_CongKham(_MaBN, idkb, false, ngaykham);
                                    DungChung.Ham.Update_Delete_CongKham(_MaBN, idkb, true, ngaykham);
                                    count++;
                                }
                            }
                            else
                            {
                                #region status = 1
                                if (_status == 1)
                                {
                                    if (q.PLDV == 2 && q.TrongDM == 1)
                                    {
                                        dthuocct.TrongBH = 1;
                                        //bool giacu = DungChung.Ham.GiaCu(_MaBN, 1, ngaynhap);
                                        //if (giacu)
                                        //    dthuocct.DonGia = q.DonGiaTT15;
                                        //else
                                        //    dthuocct.DonGia = q.DonGiaTT39;

                                       
                                        if (giacu == 15)
                                            dthuocct.DonGia = q.DonGiaTT15;
                                        else if (giacu == 39)
                                            dthuocct.DonGia = q.DonGiaTT39;
                                        else if (giacu == 0)
                                            dthuocct.DonGia = q.DonGiaBHYT;

                                        //if (DungChung.Bien.ngayGiaMoi == null)
                                        //    dthuocct.DonGia = q.DonGia;
                                        //else if (ngaynhap >= DungChung.Bien.ngayGiaMoi)
                                        //{
                                        //    if (q.DonGiaBHYT > 0)
                                        //        dthuocct.DonGia = q.DonGiaBHYT;
                                        //}
                                        //else
                                        //{
                                        //    if (q.DonGia > 0)
                                        //        dthuocct.DonGia = q.DonGia;
                                        //}

                                        dthuocct.ThanhTien = (double)dthuocct.DonGia * dthuocct.SoLuong * dthuocct.TyLeTT / 100;
                                        if (dthuocct.IDCD != null && dthuocct.IDCD > 0)
                                        {
                                            ChiDinh qcd = data.ChiDinhs.Where(panelControl1 => panelControl1.IDCD == dthuocct.IDCD.Value).FirstOrDefault();
                                            if (qcd != null)
                                            {
                                                qcd.TrongBH = 1;
                                                qcd.DonGia = dthuocct.DonGia;
                                            }
                                        }
                                        //thêm phụ thu (nếu có)
                                        var giaphuthu = data.DichVus.Where(p => p.MaDV == madv).Select(p => p.GiaPhuThu).FirstOrDefault();
                                        if (giaphuthu > 0)
                                        {
                                            DThuocct moi = new DThuocct();
                                            moi.IDDon = dthuocct.IDDon;
                                            moi.MaDV = dthuocct.MaDV;
                                            moi.DonVi = dthuocct.DonVi;
                                            moi.DonGia = giaphuthu;
                                            moi.SoLuong = 1;
                                            moi.ThanhTien = giaphuthu;
                                            moi.TienBN = giaphuthu;
                                            moi.TrongBH = 3;
                                            moi.NgayNhap = dthuocct.NgayNhap;
                                            moi.Status = dthuocct.Status;
                                            moi.IDCD = dthuocct.IDCD;
                                            moi.MaCB = dthuocct.MaCB;
                                            moi.MaKP = dthuocct.MaKP;
                                            moi.IDKB = dthuocct.IDKB;
                                            moi.Loai = dthuocct.Loai;
                                            moi.ThanhToan = dthuocct.ThanhToan;
                                            moi.TyLeTT = 100;
                                            moi.LoaiDV = dthuocct.LoaiDV;
                                            data.DThuoccts.Add(moi);
                                            data.SaveChanges();
                                        }
                                        count++;
                                    }
                                    if (q.PLDV == 1 && q.TrongDM == 1)
                                    {
                                        dthuocct.TrongBH = 1;
                                        count++;
                                    }
                                    
                                }

                                #endregion
                                #region status = 2
                                else
                                {
                                    if (q.PLDV == 2)
                                    {
                                        if (dthuocct.TrongBH == 3)
                                        {
                                            data.DThuoccts.Remove(dthuocct);
                                            data.SaveChanges();
                                            count++;
                                        }
                                        else
                                        {
                                            dthuocct.TrongBH = 0;
                                            if (DungChung.Bien.ngayGiaMoiDV == null)
                                                dthuocct.DonGia = q.DonGia2;
                                            else if (ngaynhap >= DungChung.Bien.ngayGiaMoiDV)
                                            {
                                                if (q.DonGiaDV2 > 0)
                                                    dthuocct.DonGia = q.DonGiaDV2;
                                            }
                                            else
                                            {
                                                if (q.DonGia2 > 0)
                                                    dthuocct.DonGia = q.DonGia2;
                                            }

                                            dthuocct.ThanhTien = (double)dthuocct.DonGia * dthuocct.SoLuong;
                                            dthuocct.TyLeTT = 100;
                                            if (dthuocct.IDCD != null && dthuocct.IDCD > 0)
                                            {
                                                ChiDinh qcd = data.ChiDinhs.Where(panelControl1 => panelControl1.IDCD == dthuocct.IDCD.Value).FirstOrDefault();
                                                if (qcd != null)
                                                {
                                                    qcd.TrongBH = 0;
                                                    qcd.DonGia = dthuocct.DonGia;
                                                }
                                            }
                                            count++;
                                        }
                                    }
                                    if (q.PLDV == 1)
                                    {
                                        dthuocct.TrongBH = 0;
                                        count++;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }

               
            }
            for (int i = 0; i < grvCLS.RowCount; i++)
            {
                int madv = 0;
                if (grvCLS.GetRowCellValue(i, colChonCLS) != null && grvCLS.GetRowCellValue(i, colIDCD) != null && grvCLS.GetRowCellValue(i, colMaDVCD) != null)
                {
                    if (Convert.ToBoolean(grvCLS.GetRowCellValue(i, colChonCLS)))
                    {
                        int idcd = Convert.ToInt32(grvCLS.GetRowCellValue(i, colIDCD));
                        var q = _lChiDinh.Where(p => p.IDCD == idcd).FirstOrDefault();
                        ChiDinh chidinh = data.ChiDinhs.Single(p => p.IDCD == idcd);
                        madv = q.MaDV ?? 0;


                        if (q != null)
                        {
                            #region status = 1
                            if (_status == 1)
                            {
                                if (q.TrongDM == 1)
                                {
                                    chidinh.TrongBH = 1;
                                   // int giacu = DungChung.Ham.GiaCu(_MaBN, 1);
                                    if (giacu == 15)
                                        chidinh.DonGia = q.DonGiaTT15;
                                    else if (giacu == 39)
                                        chidinh.DonGia = q.DonGiaTT39;
                                    else if (giacu == 0)
                                        chidinh.DonGia = q.DonGiaBHYT;
                                   

                                    //if (DungChung.Bien.ngayGiaMoi == null)
                                    //    chidinh.DonGia = q.DonGia;
                                    //else if (ngaynhap >= DungChung.Bien.ngayGiaMoi)
                                    //{
                                    //    if (q.DonGiaBHYT > 0)
                                    //        chidinh.DonGia = q.DonGiaBHYT;
                                    //}
                                    //else
                                    //{
                                    //    if (q.DonGia > 0)
                                    //        chidinh.DonGia = q.DonGia;
                                    //}
                                    count++;

                                }

                            }
                            #endregion
                            #region status = 2
                            else
                            {
                                chidinh.TrongBH = 0;

                                if (DungChung.Bien.ngayGiaMoiDV == null)
                                    chidinh.DonGia = q.DonGia2;

                                else if (ngaynhap >= DungChung.Bien.ngayGiaMoiDV)
                                {
                                    if (q.DonGiaDV2 > 0)
                                        chidinh.DonGia = q.DonGiaDV2;
                                }
                                else
                                {
                                    if (q.DonGia2 > 0)
                                        chidinh.DonGia = q.DonGia2;
                                }
                                count++;
                            }
                            #endregion
                        }
                    }
                }
            }
            data.SaveChanges();
            MessageBox.Show("Cập nhật thành công " + count + " bản ghi");
            if (count > 0)
            {
                loadChiTietDV(_MaBN);
            }
            

        }

        private void lupKP_EditValueChanged(object sender, EventArgs e)
        {
            loadChiTietDV(_MaBN);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            loadChiTietDV(_MaBN);
        }
        //static bool GiaCu(int mabn, int TrongDM, DateTime ngaychidinh)
        //{
        //    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    bool giacu = false;

        //    var ttbn = data.BenhNhans.Where(p => p.MaBNhan == mabn).ToList();
        //    if (ttbn.Count > 0)
        //    {
        //        string dtuong = ttbn.First().DTuong;
        //        DateTime ngaythang = DungChung.Ham.NgayTu(DungChung.Bien.ngayGiaMoiDV);
        //        if (dtuong == "BHYT" && TrongDM == 1)
        //            ngaythang = DungChung.Ham.NgayTu(DungChung.Bien.ngayGiaMoi);
        //        if (ngaychidinh.Date < ngaythang)
        //        {
        //            giacu = true;
        //        }
        //        else
        //        {
        //            giacu = false;
        //        }
        //        var vaovien = (from vv in data.VaoViens.Where(p => p.MaBNhan == mabn)
        //                       select vv.NgayVao).ToList();

        //        if (ttbn.Count > 0 && vaovien.Count > 0 && vaovien.First() != null && vaovien.First().Value.Date < ngaythang)
        //        {
        //            giacu = true;
        //        }
        //        if (vaovien.Count <= 0 && ttbn.First().NoiTru == 0 && ttbn.First().NNhap.Value.Date < ngaythang)//dùng chung DungChung.Bien.MaBV=="30007"
        //            giacu = true;
        //    }
        //    return giacu;
        //}

    }
}