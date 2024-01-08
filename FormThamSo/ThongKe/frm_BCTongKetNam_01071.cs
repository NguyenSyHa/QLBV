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
    public partial class frm_BCTongKetNam_01071 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTongKetNam_01071()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BCTongKetNam_01071_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now.Date;
            lupngayden.DateTime = DungChung.Ham.NgayDen( DateTime.Now);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = lupNgaytu.DateTime;
            DateTime denngay = lupngayden.DateTime;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qKB = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                       join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                       group a by a into kq
                       select kq).ToList();
            BC bhyt = new BC();
            BC dv = new BC();
            BC TE = new BC();
            BC MienGiam = new BC();
            bhyt.col1 = "BẢO HIỂM Y TẾ";
            dv.col1 = "DỊCH VỤ";
            TE.col1 = "TRẺ EM DƯỚI 6 TUỔI";
            MienGiam.col1 = "MIẾN GIẢM VIỆN PHÍ";

            bhyt.col2 = qKB.Where(p => p.Key.CapCuu != 1).Where(p => p.Key.DTuong == "BHYT").Where(p=> p.Key.Tuoi >= 6).Count();
            dv.col2 = qKB.Where(p => p.Key.CapCuu != 1).Where(p => p.Key.DTuong.ToLower() == "dịch vụ").Count();
            TE.col2 = qKB.Where(p => p.Key.CapCuu != 1).Where(p => p.Key.DTuong == "BHYT").Where(p => p.Key.Tuoi < 6).Count();

            bhyt.col3 = qKB.Where(p => p.Key.CapCuu == 1).Where(p => p.Key.ChuyenKhoa == "Đường sắt" || p.Key.ChuyenKhoa == "Đường bộ" || p.Key.ChuyenKhoa == "Đường sông").Where(p => p.Key.DTuong == "BHYT").Where(p => p.Key.Tuoi >= 6).Count();
            dv.col3 = qKB.Where(p => p.Key.CapCuu == 1).Where(p => p.Key.ChuyenKhoa == "Đường sắt" || p.Key.ChuyenKhoa == "Đường bộ" || p.Key.ChuyenKhoa == "Đường sông").Where(p => p.Key.DTuong.ToLower() == "dịch vụ").Count();
            TE.col3 = qKB.Where(p => p.Key.CapCuu == 1).Where(p => p.Key.ChuyenKhoa == "Đường sắt" || p.Key.ChuyenKhoa == "Đường bộ" || p.Key.ChuyenKhoa == "Đường sông").Where(p => p.Key.DTuong == "BHYT").Where(p => p.Key.Tuoi < 6).Count();

            bhyt.col4 = qKB.Where(p => p.Key.CapCuu == 1).Where(p => p.Key.ChuyenKhoa != "Đường sắt" && p.Key.ChuyenKhoa != "Đường bộ" && p.Key.ChuyenKhoa != "Đường sông").Where(p => p.Key.DTuong == "BHYT").Where(p => p.Key.Tuoi >= 6).Count();
            dv.col4 = qKB.Where(p => p.Key.CapCuu == 1).Where(p => p.Key.ChuyenKhoa != "Đường sắt" && p.Key.ChuyenKhoa != "Đường bộ" && p.Key.ChuyenKhoa != "Đường sông").Where(p => p.Key.DTuong.ToLower() == "dịch vụ").Count();
            TE.col4 = qKB.Where(p => p.Key.CapCuu == 1).Where(p => p.Key.ChuyenKhoa != "Đường sắt" && p.Key.ChuyenKhoa != "Đường bộ" && p.Key.ChuyenKhoa != "Đường sông").Where(p => p.Key.DTuong == "BHYT").Where(p => p.Key.Tuoi < 6).Count();


            bhyt.col5 = bhyt.col2 + bhyt.col3 + bhyt.col4;
            dv.col5 = dv.col2 + dv.col3 + dv.col4;
            TE.col5 = TE.col2 + TE.col3 + TE.col4;

            bhyt.col6 = 0;
            dv.col6 = qKB.Where(p => p.Key.DTuong.ToLower() == "ksk").Where(p=>p.Key.TChung == "Khám sức khỏe định kỳ").Count();
            TE.col6 = 0;

            bhyt.col7 = 0;
            dv.col7 = qKB.Where(p => p.Key.DTuong.ToLower() == "ksk").Where(p => p.Key.TChung != "Khám sức khỏe định kỳ").Count();
            TE.col7 = 0;

            var qDTriDKy0 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                             join vv in data.VaoViens.Where(p => p.NgayVao != null && p.NgayVao < tungay) on bn.MaBNhan equals vv.MaBNhan
                             join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                             from kq1 in kq.DefaultIfEmpty()
                             where (kq1 == null || (kq1 != null && kq1.NgayRa >= tungay))
                             select new { bn, kq1, songayDT = kq1 == null ? 0 : (kq1.SoNgaydt == null ? 0 : (kq1.NgayRa > denngay ? 0 : kq1.SoNgaydt)) } // ngayvao = vv.NgayVao.Value, ngayra =  kq1 ==  null ?  DateTime.Now : kq1.NgayRa.Value, songaydt = kq1 == null ? 0 : kq1.SoNgaydt.Value }
               ).ToList();

            var qDTriDKy = (from bn in qDTriDKy0 select new { bn.bn, songayDT = bn.songayDT.Value } //bn.songaydt > 1 ? bn.songaydt : (int)(bn.ngayra - bn.ngayvao).TotalDays}

               ).ToList();

            
            bhyt.col8 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Count();
            dv.col8 = qDTriDKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Count();
            TE.col8 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Count();

            var qDTriTrongKy0 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                                 join vv in data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay) on bn.MaBNhan equals vv.MaBNhan
                                 join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 select new { bn, kq1, songayDT = kq1 == null ? 0 : (kq1.SoNgaydt == null ? 0 :(kq1.NgayRa > denngay ? 0 : kq1.SoNgaydt)) }// ngayvao = vv.NgayVao.Value, ngayra = kq1 == null ? DateTime.Now : kq1.NgayRa.Value, songaydt = kq1 == null ? 0 : kq1.SoNgaydt.Value }
              ).ToList();

            var qDTriTrongKy = (from bn in qDTriTrongKy0 select new { bn.bn, songayDT = bn.songayDT.Value}//bn.songaydt > 1 ? bn.songaydt : (int)(bn.ngayra - bn.ngayvao).TotalDays }

               ).ToList();
          

            bhyt.col9 = qDTriTrongKy.Where(p => p.bn.CapCuu != 1).Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Count();
            dv.col9 = qDTriTrongKy.Where(p => p.bn.CapCuu != 1).Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Count();
            TE.col9 = qDTriTrongKy.Where(p => p.bn.CapCuu != 1).Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Count();

            bhyt.col10 = qDTriTrongKy.Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Count();
            dv.col10 = qDTriTrongKy.Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Count();
            TE.col10 = qDTriTrongKy.Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Count();

            bhyt.col11 = qDTriTrongKy.Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Count();
            dv.col11 = qDTriTrongKy.Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Count();
            TE.col11 = qDTriTrongKy.Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Count();


            bhyt.col12 = bhyt.col9 + bhyt.col10 + bhyt.col11;
            dv.col12 = dv.col9 + dv.col10 + dv.col11;
            TE.col12 = TE.col9 + TE.col10 + TE.col11;

            

            var qbnrv = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                        
                         join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                          join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                         select new { bn, rv }).ToList();

            bhyt.col13 = qbnrv.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.MaDTuong.ToLower() != "te").Count();
            dv.col13 = qbnrv.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Count();
            TE.col13 = qbnrv.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.MaDTuong.ToLower() == "te").Count();

            bhyt.col14 = qbnrv.Where(p => p.rv.KetQua != null && p.rv.KetQua.ToLower() == "tử vong").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Count();
            dv.col14 = qbnrv.Where(p => p.rv.KetQua != null && p.rv.KetQua.ToLower() == "tử vong").Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Count();
            TE.col14 = qbnrv.Where(p => p.rv.KetQua != null && p.rv.KetQua.ToLower() == "tử vong").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Count();

            bhyt.col15 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.CapCuu != 1).Sum(p => p.songayDT) +  qDTriTrongKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.CapCuu != 1).Sum(p => p.songayDT);
            dv.col15 = qDTriDKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.bn.CapCuu != 1).Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.bn.CapCuu != 1).Sum(p => p.songayDT);
            TE.col15 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.bn.CapCuu != 1).Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.bn.CapCuu != 1).Sum(p => p.songayDT);

            bhyt.col16 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Sum(p => p.songayDT);
            dv.col16 = qDTriDKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Sum(p => p.songayDT);
            TE.col16 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa == "Đường sắt" || p.bn.ChuyenKhoa == "Đường bộ" || p.bn.ChuyenKhoa == "Đường sông").Sum(p => p.songayDT);

            bhyt.col17 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Sum(p => p.songayDT);
            dv.col17 = qDTriDKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Sum(p => p.songayDT);
            TE.col17 = qDTriDKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Sum(p => p.songayDT) + qDTriTrongKy.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.bn.CapCuu == 1).Where(p => p.bn.ChuyenKhoa != "Đường sắt" && p.bn.ChuyenKhoa != "Đường bộ" && p.bn.ChuyenKhoa != "Đường sông").Sum(p => p.songayDT);

            bhyt.col18 = bhyt.col15 + bhyt.col16 + bhyt.col17;
            dv.col18 = dv.col15 + dv.col16 + dv.col17;
            TE.col18 = TE.col15 + TE.col16 + TE.col17;

            bhyt.col19 = qbnrv.Where(p => p.rv.KetQua != null && p.rv.KetQua.ToLower() == "tử vong").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi >= 6).Where(p => p.rv.SoNgaydt != null).Sum(p => p.rv.SoNgaydt.Value);
            dv.col19 = qbnrv.Where(p => p.rv.KetQua != null && p.rv.KetQua.ToLower() == "tử vong").Where(p => p.bn.DTuong.ToLower() == "dịch vụ").Where(p => p.rv.SoNgaydt != null).Sum(p => p.rv.SoNgaydt.Value);
            TE.col19 = qbnrv.Where(p => p.rv.KetQua != null && p.rv.KetQua.ToLower() == "tử vong").Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi < 6).Where(p => p.rv.SoNgaydt != null).Sum(p => p.rv.SoNgaydt.Value);

            var qdv = (from dvu in data.DichVus
                       join tn in data.TieuNhomDVs on dvu.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { dvu.MaDV, tn.TenRG, n.IDNhom , dvu.TenDV}).ToList();
            var qcls = (from cls in data.CLS.Where(p=>p.NgayTH >= tungay && p.NgayTH <= denngay)
                            join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                            join cd in data.ChiDinhs.Where(p=>p.Status ==1) on cls.IdCLS equals cd.IdCLS select new {bn, cd}).ToList();

            //số lượt
            var qallCLS = (from cls in qcls
                           join dvu in qdv on cls.cd.MaDV equals dvu.MaDV
                           select new {cls.bn.Tuoi, cls.bn.MaBNhan, cls.bn.DTuong, cls.bn.MaDTuong, cls.bn.NoiTru, cls.bn.DTNT, cls.bn.CapCuu, dvu.TenRG, dvu.IDNhom,  dvu.TenDV,  dvu.MaDV }).ToList();
            bhyt.col20 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
            dv.col20 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p=>p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
            TE.col20 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();

            bhyt.col21 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
            dv.col21 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
            TE.col21 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();

            bhyt.col22 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
            dv.col22 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
            TE.col22 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();

            bhyt.col23 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
            dv.col23 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
            TE.col23 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();

            bhyt.col24 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Count();
            dv.col24 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Count();
            TE.col24 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong).Count();

            bhyt.col25 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo).Count();
            dv.col25 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo).Count();
            TE.col25 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo).Count();

            bhyt.col26 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Count();
            dv.col26 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Count();
            TE.col26 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Count();

            bhyt.col27 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
            dv.col27 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
            TE.col27 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();

            bhyt.col28 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            dv.col28 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            TE.col28 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();

            bhyt.col29 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("HIV")).Count();
            dv.col29 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("HIV")).Count();
            TE.col29 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac).Where(p => p.TenDV.Contains("HIV")).Count();

            var qXNHIV = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)                       
                        join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        join dvu in data.DichVus.Where(p=>p.TenDV.Contains("HIV")) on cd.MaDV equals dvu.MaDV
                        join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                        select new {bn.Tuoi, bn.MaBNhan, bn.DTuong, bn.MaDTuong, cd.IDCD,KetQua = clsct.KetQua ??""}).ToList();
            var qxnhivDuongTinh = (from xn in qXNHIV.Where(p => p.KetQua.ToLower() == "dương tính" || p.KetQua.ToLower() == "+")
                                   group xn by new { xn.MaBNhan, xn.IDCD, xn.DTuong, xn.MaDTuong, xn.Tuoi} into kq
                                   select new { kq.Key.MaBNhan, kq.Key.IDCD, kq.Key.DTuong, kq.Key.MaDTuong, kq.Key.Tuoi}
                                       ).ToList();


            bhyt.col30 = qxnhivDuongTinh.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Count();
            dv.col30 = qxnhivDuongTinh.Where(p => p.DTuong.ToLower() == "dịch vụ").Count();
            TE.col30 = qxnhivDuongTinh.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Count();


            bhyt.col31 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.IDNhom == 1).Count() - bhyt.col26 - bhyt.col27 - bhyt.col28 - bhyt.col29;
            dv.col31 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.IDNhom == 1).Count() - dv.col26 - dv.col27 - dv.col28 - dv.col29;
            TE.col31 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.IDNhom == 1).Count() - TE.col26 - TE.col27 - TE.col28 - TE.col29;

            bhyt.col32 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi >= 6).Where(p => p.IDNhom == 1).Count();
            dv.col32 = qallCLS.Where(p => p.DTuong.ToLower() == "dịch vụ").Where(p => p.IDNhom == 1).Count();
            TE.col32 = qallCLS.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Where(p => p.IDNhom == 1).Count();

            List<BC> list = new List<BC>();
            list.Add(bhyt);
            list.Add(dv);
            list.Add(TE);
            list.Add(MienGiam);
            BaoCao.rep_BCTongKetNam_01071 rep = new BaoCao.rep_BCTongKetNam_01071();
            if(string.IsNullOrEmpty(txtTieuDe.Text))
            {
                rep.celTit.Text = "KẾT QUẢ KHÁM CHỮA BỆNH";
                rep.celNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            }
            else
            {
                rep.celTit.Text = txtTieuDe.Text.ToUpper();
            }
            frmIn frm = new frmIn();
            rep.DataSource = list;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private class BC
        {
            public string col1 { set; get; }
            public int col2 { set; get;}
            public int col3 { set; get; }
            public int col4 { set; get; }
            public int col5 { set; get; }
            public int col6 { set; get; }
            public int col7 { set; get; }
            public int col8 { set; get; }
            public int col9 { set; get; }
            public int col10 { set; get; }
            public int col11 { set; get; }
            public int col12 { set; get; }
            public int col13 { set; get; }
            public int col14 { set; get; }
            public int col15 { set; get; }
            public int col16 { set; get; }
            public int col17 { set; get; }
            public int col18 { set; get; }
            public int col19 { set; get; }
            public int col20 { set; get; }
            public int col21 { set; get; }
            public int col22 { set; get; }
            public int col23 { set; get; }
            public int col24 { set; get; }
            public int col25 { set; get; }
            public int col26 { set; get; }
            public int col27 { set; get; }
            public int col28 { set; get; }
            public int col29 { set; get; }
            public int col30 { set; get; }
            public int col31 { set; get; }
            public int col32 { set; get; }
         
        }

    }
}