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
    public partial class Frm_BCHoatDongPK_cs2_01049 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BCHoatDongPK_cs2_01049()
        {
            InitializeComponent();
        }
        private static QLBV_Database.QLBVEntities Get_Data()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            return _data;
        }
        public class baocaoNamTL2
        {
            private int _tscc;

            public int Tscc
            {
                get { return _tscc; }
                set { _tscc = value; }
            }
            private int _tsccBH;

            public int TsccBH
            {
                get { return _tsccBH; }
                set { _tsccBH = value; }
            }
            private int _tsccDV;

            public int TsccDV
            {
                get { return _tsccDV; }
                set { _tsccDV = value; }
            }
            private int _tsCsi;

            public int TsCsi
            {
                get { return _tsCsi; }
                set { _tsCsi = value; }
            }
            private int _tsCsiBH;

            public int TsCsiBH
            {
                get { return _tsCsiBH; }
                set { _tsCsiBH = value; }
            }
            private int _tsCsiDV;

            public int TsCsiDV
            {
                get { return _tsCsiDV; }
                set { _tsCsiDV = value; }
            }
            private int _tskb_ksk;

            public int Tskb_ksk
            {
                get { return _tskb_ksk; }
                set { _tskb_ksk = value; }
            }
            private int _tskb_kskBH;

            public int Tskb_kskBH
            {
                get { return _tskb_kskBH; }
                set { _tskb_kskBH = value; }
            }
            private int _tskb_kskDV;

            public int Tskb_kskDV
            {
                get { return _tskb_kskDV; }
                set { _tskb_kskDV = value; }
            }
            private int _tskhamnoi;

            public int Tskhamnoi
            {
                get { return _tskhamnoi; }
                set { _tskhamnoi = value; }
            }
            private int _tskhamnoiBH;

            public int TskhamnoiBH
            {
                get { return _tskhamnoiBH; }
                set { _tskhamnoiBH = value; }
            }
            private int _tskhamnoiDV;

            public int TskhamnoiDV
            {
                get { return _tskhamnoiDV; }
                set { _tskhamnoiDV = value; }
            }
            private int _tskhamnoithuthuat;

            public int Tskhamnoithuthuat
            {
                get { return _tskhamnoithuthuat; }
                set { _tskhamnoithuthuat = value; }
            }
            private int _tskhamnoithuthuatBH;

            public int TskhamnoithuthuatBH
            {
                get { return _tskhamnoithuthuatBH; }
                set { _tskhamnoithuthuatBH = value; }
            }
            private int _tskhamnoithuthuatDV;

            public int TskhamnoithuthuatDV
            {
                get { return _tskhamnoithuthuatDV; }
                set { _tskhamnoithuthuatDV = value; }
            }
            private int _tskhamnhi;

            public int Tskhamnhi
            {
                get { return _tskhamnhi; }
                set { _tskhamnhi = value; }
            }
            private int _tskhamnhiBH;

            public int TskhamnhiBH
            {
                get { return _tskhamnhiBH; }
                set { _tskhamnhiBH = value; }
            }
            private int _tskhamnhiDV;

            public int TskhamnhiDV
            {
                get { return _tskhamnhiDV; }
                set { _tskhamnhiDV = value; }
            }
            private int _tskhamnhithuthuat;

            public int Tskhamnhithuthuat
            {
                get { return _tskhamnhithuthuat; }
                set { _tskhamnhithuthuat = value; }
            }
            private int _tskhamnhithuthuatBh;

            public int TskhamnhithuthuatBh
            {
                get { return _tskhamnhithuthuatBh; }
                set { _tskhamnhithuthuatBh = value; }
            }
            private int _tskhamnhithuthuatDV;

            public int TskhamnhithuthuatDV
            {
                get { return _tskhamnhithuthuatDV; }
                set { _tskhamnhithuthuatDV = value; }
            }
            private int _tskhamngoai;

            public int Tskhamngoai
            {
                get { return _tskhamngoai; }
                set { _tskhamngoai = value; }
            }
            private int _tskhamngoaiBH;

            public int TskhamngoaiBH
            {
                get { return _tskhamngoaiBH; }
                set { _tskhamngoaiBH = value; }
            }
            private int _tskhamngoaiDV;

            public int TskhamngoaiDV
            {
                get { return _tskhamngoaiDV; }
                set { _tskhamngoaiDV = value; }
            }
            private int _tskhamngoaithuthuat;

            public int Tskhamngoaithuthuat
            {
                get { return _tskhamngoaithuthuat; }
                set { _tskhamngoaithuthuat = value; }
            }
            private int _tskhamngoaithuthuatBh;

            public int TskhamngoaithuthuatBh
            {
                get { return _tskhamngoaithuthuatBh; }
                set { _tskhamngoaithuthuatBh = value; }
            }
            private int _tskhamngoaithuthuatDV;

            public int TskhamngoaithuthuatDV
            {
                get { return _tskhamngoaithuthuatDV; }
                set { _tskhamngoaithuthuatDV = value; }
            }
            private int _tskhamSan;

            public int TskhamSan
            {
                get { return _tskhamSan; }
                set { _tskhamSan = value; }
            }
            private int _tskhamSanBH;

            public int TskhamSanBH
            {
                get { return _tskhamSanBH; }
                set { _tskhamSanBH = value; }
            }
            private int _tskhamSanDV;

            public int TskhamSanDV
            {
                get { return _tskhamSanDV; }
                set { _tskhamSanDV = value; }
            }
            private int _tskhamSanthuthuat;

            public int TskhamSanthuthuat
            {
                get { return _tskhamSanthuthuat; }
                set { _tskhamSanthuthuat = value; }
            }
            private int _tskhamSanthuthuatBH;

            public int TskhamSanthuthuatBH
            {
                get { return _tskhamSanthuthuatBH; }
                set { _tskhamSanthuthuatBH = value; }
            }
            private int _tskhamSanthuthuatDV;

            public int TskhamSanthuthuatDV
            {
                get { return _tskhamSanthuthuatDV; }
                set { _tskhamSanthuthuatDV = value; }
            }
            private int _tsRHM;

            public int TsRHM
            {
                get { return _tsRHM; }
                set { _tsRHM = value; }
            }
            private int _tsRHMBH;

            public int TsRHMBH
            {
                get { return _tsRHMBH; }
                set { _tsRHMBH = value; }
            }
            private int _tsRHMDV;

            public int TsRHMDV
            {
                get { return _tsRHMDV; }
                set { _tsRHMDV = value; }
            }
            private int _tsRHMthuthuat;

            public int TsRHMthuthuat
            {
                get { return _tsRHMthuthuat; }
                set { _tsRHMthuthuat = value; }
            }
            private int _tsRHMthuthuatBH;

            public int TsRHMthuthuatBH
            {
                get { return _tsRHMthuthuatBH; }
                set { _tsRHMthuthuatBH = value; }
            }
            private int _tsRHMthuthuatDV;

            public int TsRHMthuthuatDV
            {
                get { return _tsRHMthuthuatDV; }
                set { _tsRHMthuthuatDV = value; }
            }
            private int _tsTMH;

            public int TsTMH
            {
                get { return _tsTMH; }
                set { _tsTMH = value; }
            }
            private int _tsTMHBH;

            public int TsTMHBH
            {
                get { return _tsTMHBH; }
                set { _tsTMHBH = value; }
            }
            private int _tsTMHDV;

            public int TsTMHDV
            {
                get { return _tsTMHDV; }
                set { _tsTMHDV = value; }
            }
            private int _tsTMHthuthuat;

            public int TsTMHthuthuat
            {
                get { return _tsTMHthuthuat; }
                set { _tsTMHthuthuat = value; }
            }
            private int _tsTMHthuthuatBh;

            public int TsTMHthuthuatBh
            {
                get { return _tsTMHthuthuatBh; }
                set { _tsTMHthuthuatBh = value; }
            }
            private int _tsTMHthuthuatDV;

            public int TsTMHthuthuatDV
            {
                get { return _tsTMHthuthuatDV; }
                set { _tsTMHthuthuatDV = value; }
            }
            private int _tskhammat;

            public int Tskhammat
            {
                get { return _tskhammat; }
                set { _tskhammat = value; }
            }
            private int _tskhammatBh;

            public int TskhammatBh
            {
                get { return _tskhammatBh; }
                set { _tskhammatBh = value; }
            }
            private int _tskhammatDv;

            public int TskhammatDv
            {
                get { return _tskhammatDv; }
                set { _tskhammatDv = value; }
            }
            private int _tskhammatthuthuat;

            public int Tskhammatthuthuat
            {
                get { return _tskhammatthuthuat; }
                set { _tskhammatthuthuat = value; }
            }
            private int _tskhammatthuthuatBH;

            public int TskhammatthuthuatBH
            {
                get { return _tskhammatthuthuatBH; }
                set { _tskhammatthuthuatBH = value; }
            }
            private int _tskhammatthuthuatDV;

            public int TskhammatthuthuatDV
            {
                get { return _tskhammatthuthuatDV; }
                set { _tskhammatthuthuatDV = value; }
            }
            private int _tsYHCT;

            public int TsYHCT
            {
                get { return _tsYHCT; }
                set { _tsYHCT = value; }
            }
            private int _tsYHCTBH;

            public int TsYHCTBH
            {
                get { return _tsYHCTBH; }
                set { _tsYHCTBH = value; }
            }
            private int _tsYHCTDV;

            public int TsYHCTDV
            {
                get { return _tsYHCTDV; }
                set { _tsYHCTDV = value; }
            }
            private int _tsYHCTthuthuat;

            public int TsYHCTthuthuat
            {
                get { return _tsYHCTthuthuat; }
                set { _tsYHCTthuthuat = value; }
            }
            private int _tsYHCTthuthuatBH;

            public int TsYHCTthuthuatBH
            {
                get { return _tsYHCTthuthuatBH; }
                set { _tsYHCTthuthuatBH = value; }
            }
            private int _tsYHCTthuthuatDV;

            public int TsYHCTthuthuatDV
            {
                get { return _tsYHCTthuthuatDV; }
                set { _tsYHCTthuthuatDV = value; }
            }
            private int _tsksk;

            public int Tsksk
            {
                get { return _tsksk; }
                set { _tsksk = value; }
            }
            private int _tskskBH;

            public int TskskBH
            {
                get { return _tskskBH; }
                set { _tskskBH = value; }
            }
            private int _tskskDV;

            public int TskskDV
            {
                get { return _tskskDV; }
                set { _tskskDV = value; }
            }
            private int _tskskthuthuat;

            public int Tskskthuthuat
            {
                get { return _tskskthuthuat; }
                set { _tskskthuthuat = value; }
            }
            private int _tskskthuthuatBH;

            public int TskskthuthuatBH
            {
                get { return _tskskthuthuatBH; }
                set { _tskskthuthuatBH = value; }
            }
            private int _tskskthuthuatDV;

            public int TskskthuthuatDV
            {
                get { return _tskskthuthuatDV; }
                set { _tskskthuthuatDV = value; }
            }
            private int _tsCLS;

            public int TsCLS
            {
                get { return _tsCLS; }
                set { _tsCLS = value; }
            }
            private int _tsCLSBH;

            public int TsCLSBH
            {
                get { return _tsCLSBH; }
                set { _tsCLSBH = value; }
            }
            private int _tsCLSDV;

            public int TsCLSDV
            {
                get { return _tsCLSDV; }
                set { _tsCLSDV = value; }
            }
            private int _tsXN;

            public int TsXN
            {
                get { return _tsXN; }
                set { _tsXN = value; }
            }
            private int _tsXNBH;

            public int TsXNBH
            {
                get { return _tsXNBH; }
                set { _tsXNBH = value; }
            }
            private int _tsXNDV;

            public int TsXNDV
            {
                get { return _tsXNDV; }
                set { _tsXNDV = value; }
            }
            private int _tsSinhHoa;

            public int TsSinhHoa
            {
                get { return _tsSinhHoa; }
                set { _tsSinhHoa = value; }
            }
            private int _tsSinhHoaBH;

            public int TsSinhHoaBH
            {
                get { return _tsSinhHoaBH; }
                set { _tsSinhHoaBH = value; }
            }
            private int _tsSinhHoaDV;

            public int TsSinhHoaDV
            {
                get { return _tsSinhHoaDV; }
                set { _tsSinhHoaDV = value; }
            }
            private int _tsHuyetHoc;

            public int TsHuyetHoc
            {
                get { return _tsHuyetHoc; }
                set { _tsHuyetHoc = value; }
            }
            private int _tsHuyetHocBH;

            public int TsHuyetHocBH
            {
                get { return _tsHuyetHocBH; }
                set { _tsHuyetHocBH = value; }
            }
            private int _tsHuyetHocDV;

            public int TsHuyetHocDV
            {
                get { return _tsHuyetHocDV; }
                set { _tsHuyetHocDV = value; }
            }
            private int _tsNuoctieu;

            public int TsNuoctieu
            {
                get { return _tsNuoctieu; }
                set { _tsNuoctieu = value; }
            }
            private int _tsNuoctieuBH;

            public int TsNuoctieuBH
            {
                get { return _tsNuoctieuBH; }
                set { _tsNuoctieuBH = value; }
            }
            private int _tsNuoctieuDV;

            public int TsNuoctieuDV
            {
                get { return _tsNuoctieuDV; }
                set { _tsNuoctieuDV = value; }
            }
            private int _tsXNKhac;

            public int TsXNKhac
            {
                get { return _tsXNKhac; }
                set { _tsXNKhac = value; }
            }
            private int _tsXNKhacBH;

            public int TsXNKhacBH
            {
                get { return _tsXNKhacBH; }
                set { _tsXNKhacBH = value; }
            }
            private int _tsXNKhacDV;

            public int TsXNKhacDV
            {
                get { return _tsXNKhacDV; }
                set { _tsXNKhacDV = value; }
            }
            private int _tsXQuang;

            public int TsXQuang
            {
                get { return _tsXQuang; }
                set { _tsXQuang = value; }
            }
            private int _tsXQuangBH;

            public int TsXQuangBH
            {
                get { return _tsXQuangBH; }
                set { _tsXQuangBH = value; }
            }
            private int _tsXQuangDV;

            public int TsXQuangDV
            {
                get { return _tsXQuangDV; }
                set { _tsXQuangDV = value; }
            }
            private int _tsSieuam;

            public int TsSieuam
            {
                get { return _tsSieuam; }
                set { _tsSieuam = value; }
            }
            private int _tsSieuamBH;

            public int TsSieuamBH
            {
                get { return _tsSieuamBH; }
                set { _tsSieuamBH = value; }
            }
            private int _tsSieuamDV;

            public int TsSieuamDV
            {
                get { return _tsSieuamDV; }
                set { _tsSieuamDV = value; }
            }
            private int _ts2D;

            public int Ts2D
            {
                get { return _ts2D; }
                set { _ts2D = value; }
            }
            private int _ts2DBH;

            public int Ts2DBH
            {
                get { return _ts2DBH; }
                set { _ts2DBH = value; }
            }
            private int _ts2DDV;

            public int Ts2DDV
            {
                get { return _ts2DDV; }
                set { _ts2DDV = value; }
            }
            private int _ts4D;

            public int Ts4D
            {
                get { return _ts4D; }
                set { _ts4D = value; }
            }
            private int _ts4DBH;

            public int Ts4DBH
            {
                get { return _ts4DBH; }
                set { _ts4DBH = value; }
            }
            private int _ts4DDV;

            public int Ts4DDV
            {
                get { return _ts4DDV; }
                set { _ts4DDV = value; }
            }
            private int _tsNoisoitieuhoa;

            public int TsNoisoitieuhoa
            {
                get { return _tsNoisoitieuhoa; }
                set { _tsNoisoitieuhoa = value; }
            }
            private int _tsNoisoitieuhoaBH;

            public int TsNoisoitieuhoaBH
            {
                get { return _tsNoisoitieuhoaBH; }
                set { _tsNoisoitieuhoaBH = value; }
            }
            private int _tsNoisoitieuhoaDV;

            public int TsNoisoitieuhoaDV
            {
                get { return _tsNoisoitieuhoaDV; }
                set { _tsNoisoitieuhoaDV = value; }
            }
            private int _tsNoisoiCTC;
            public int TsNoisoiCTC
            {
                get { return _tsNoisoiCTC; }
                set { _tsNoisoiCTC = value; }
            }
            private int _tsNoisoiCTCBH;
            public int TsNoisoiCTCBH
            {
                get { return _tsNoisoiCTCBH; }
                set { _tsNoisoiCTCBH = value; }
            }
            private int _tsNoisoiCTCDV;
            public int TsNoisoiCTCDV
            {
                get { return _tsNoisoiCTCDV; }
                set { _tsNoisoiCTCDV = value; }
            }

            private int _tsNoisoiTMH;
            public int TsNoisoiTMH
            {
                get { return _tsNoisoiTMH; }
                set { _tsNoisoiTMH = value; }
            }
            private int _tsNoisoiTMHBH;
            public int TsNoisoiTMHBH
            {
                get { return _tsNoisoiTMHBH; }
                set { _tsNoisoiTMHBH = value; }
            }
            private int _tsNoisoiTMHDV;
            public int TsNoisoiTMHDV
            {
                get { return _tsNoisoiTMHDV; }
                set { _tsNoisoiTMHDV = value; }
            }

            private int _tscapcuunoi;
            public int Tscapcuunoi
            {
                get { return _tscapcuunoi; }
                set { _tscapcuunoi = value; }
            }

            private int _tscapcuunoiBH;
            public int TscapcuunoiBH
            {
                get { return _tscapcuunoiBH; }
                set { _tscapcuunoiBH = value; }
            }

            private int _tscapcuunoiDV;
            public int TscapcuunoiDV
            {
                get { return _tscapcuunoiDV; }
                set { _tscapcuunoiDV = value; }
            }

            private int _tscapcuungoai;
            public int Tscapcuungoai
            {
                get { return _tscapcuungoai; }
                set { _tscapcuungoai = value; }
            }

            private int _tscapcuungoaiBH;
            public int TscapcuungoaiBH
            {
                get { return _tscapcuungoaiBH; }
                set { _tscapcuungoaiBH = value; }
            }

            private int _tscapcuungoaiDV;
            public int TscapcuungoaiDV
            {
                get { return _tscapcuungoaiDV; }
                set { _tscapcuungoaiDV = value; }
            }

            private int _tscapcuunoithuthuat;
            public int Tscapcuunoithuthuat
            {
                get { return _tscapcuunoithuthuat; }
                set { _tscapcuunoithuthuat = value; }
            }

            private int _tscapcuunoithuthuatBH;
            public int TscapcuunoithuthuatBH
            {
                get { return _tscapcuunoithuthuatBH; }
                set { _tscapcuunoithuthuatBH = value; }
            }

            private int _tscapcuunoithuthuatDV;
            public int TscapcuunoithuthuatDV
            {
                get { return _tscapcuunoithuthuatDV; }
                set { _tscapcuunoithuthuatDV = value; }
            }

            private int _tscapcuungoaithuthuat;
            public int Tscapcuungoaithuthuat
            {
                get { return _tscapcuungoaithuthuat; }
                set { _tscapcuungoaithuthuat = value; }
            }

            private int _tscapcuungoaithuthuatBH;
            public int TscapcuungoaithuthuatBH
            {
                get { return _tscapcuungoaithuthuatBH; }
                set { _tscapcuungoaithuthuatBH = value; }
            }

            private int _tscapcuungoaithuthuatDV;
            public int TscapcuungoaithuthuatDV
            {
                get { return _tscapcuungoaithuthuatDV; }
                set { _tscapcuungoaithuthuatDV = value; }
            }
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            #region Nam Thăng Long
            if (DungChung.Bien.MaBV == "01049"|| DungChung.Bien.MaBV == "01071") // viện 300009 chỉ lấy tt các phòng khám
            {

                DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(txttungay.Text));
                DateTime denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtdenngay.Text));

                List<baocaoNamTL2> _listbaocaoTL2 = new List<baocaoNamTL2>();
                baocaoNamTL2 _baocaoTL2 = new baocaoNamTL2();
                QLBV_Database.QLBVEntities _data = Get_Data();
                if (tungay > denngay)
                {
                    MessageBox.Show("Bạn nhập ngày chưa chính xác, vui lòng nhập lại !");
                }
                else
                {

                    #region Tổng số bệnh nhân cấp cứu
                    var a = (from bn in _data.BenhNhans.Where(p => p.CapCuu == 1)
                             join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                             join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kphong.MaKP
                             select new { bn.MaBNhan, bn.DTuong }).Distinct().ToList();
                    int tscc = a.Count; _baocaoTL2.Tscc = tscc;
                    int tsbnccBH = a.Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsccBH = tsbnccBH;
                    int tsbnccDV = a.Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsccDV = tsbnccDV;
                    #endregion

                    #region Tổng số bệnh nhân chuyển viện
                    var b = (from bn in _data.BenhNhans
                             join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                             join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kphong.MaKP
                             join rv in _data.RaViens.Where(p => p.MaBVC == "01071") on bn.MaBNhan equals rv.MaBNhan
                             select new { bn.MaBNhan, bn.DTuong }).Distinct().ToList();
                    int tscsi = b.Count; _baocaoTL2.TsCsi = tscsi;
                    int tscsiBH = b.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsCsiBH = tscsiBH;
                    int tscsiDV = b.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsCsiDV = tscsiDV;
                    #endregion

                    #region Tổng số bệnh nhân trong từng phòng khám
                    var c = (from bn in _data.BenhNhans
                             join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                             join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kphong.MaKP
                             select new
                             {
                                 bn.MaBNhan,
                                 bn.DTuong,
                                 kphong.TenKP
                             }).Distinct().ToList();

                    int tskb_ksk = c.Count; _baocaoTL2.Tskb_ksk = tskb_ksk;
                    int tskb_kskBH = c.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.Tskb_kskBH = tskb_kskBH;
                    int tskb_kskDV = c.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.Tskb_kskDV = tskb_kskDV;

                    // phòng khám nội
                    int tskhamnoi = c.Where(p => p.TenKP.Contains("Phòng Khám Nội")).Count(); _baocaoTL2.Tskhamnoi = tskhamnoi;
                    int tskhamnoiBH = c.Where(p => p.TenKP.Contains("Phòng Khám Nội")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TskhamnoiBH = tskhamnoiBH;
                    int tskhamnoiDV = c.Where(p => p.TenKP.Contains("Phòng Khám Nội")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamnoiDV = tskhamnoiDV;

                    //Phòng khám nhi
                    int tskhamnhi = c.Where(p => p.TenKP.Contains("Phòng khám Nhi")).Count(); _baocaoTL2.Tskhamnhi = tskhamnhi;
                    int tskhamnhiBH = c.Where(p => p.TenKP.Contains("Phòng khám Nhi")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TskhamnhiBH = tskhamnhiBH;
                    int tskhamnhiDV = c.Where(p => p.TenKP.Contains("Phòng khám Nhi")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamnhiDV = tskhamnhiDV;

                    //Phòng khám ngoại
                    int tskhamngoai = c.Where(p => p.TenKP.Contains("Phòng Khám Ngoại")).Count(); _baocaoTL2.Tskhamngoai = tskhamngoai;
                    int tskhamngoaiBH = c.Where(p => p.TenKP.Contains("Phòng Khám Ngoại")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TskhamngoaiBH = tskhamngoaiBH;
                    int tskhamngoaiDV = c.Where(p => p.TenKP.Contains("Phòng Khám Ngoại")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamngoaiDV = tskhamngoaiDV;

                    //Phòng khám Sản
                    int tskhamsan = c.Where(p => p.TenKP.Contains("Phòng Khám Sản")).Count(); _baocaoTL2.TskhamSan = tskhamsan;
                    int tskhamsanBH = c.Where(p => p.TenKP.Contains("Phòng Khám Sản")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TskhamSanBH = tskhamsanBH;
                    int tskhamsanDV = c.Where(p => p.TenKP.Contains("Phòng Khám Sản")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamSanDV = tskhamsanDV;

                    //Phòng khám RHM
                    int tsRHM = c.Where(p => p.TenKP.Contains("Phòng Khám RHM")).Count(); _baocaoTL2.TsRHM = tsRHM;
                    int tsRHMBH = c.Where(p => p.TenKP.Contains("Phòng Khám RHM")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsRHMBH = tsRHMBH;
                    int tsRHMDV = c.Where(p => p.TenKP.Contains("Phòng Khám RHM")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsRHMDV = tsRHMDV;

                    //Phòng khám TMH
                    int tsTMH = c.Where(p => p.TenKP.Contains("Phòng Khám TMH")).Count(); _baocaoTL2.TsTMH = tsTMH;
                    int tsTMHBH = c.Where(p => p.TenKP.Contains("Phòng Khám TMH")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsTMHBH = tsTMHBH;
                    int tsTMHDV = c.Where(p => p.TenKP.Contains("Phòng Khám TMH")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsTMHDV = tsTMHDV;


                    //Phòng khám mắt
                    int tskhammat = c.Where(p => p.TenKP.Contains("Phòng Khám Mắt")).Count(); _baocaoTL2.Tskhammat = tskhammat;
                    int tskhammatBH = c.Where(p => p.TenKP.Contains("Phòng Khám Mắt")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TskhammatBh = tskhammatBH;
                    int tskhammatDV = c.Where(p => p.TenKP.Contains("Phòng Khám Mắt")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhammatDv = tskhammatDV;

                    //Phòng khám YHCT
                    int tskhamYHCT = c.Where(p => p.TenKP.Contains("Phòng Khám Đông Y") || p.TenKP.Contains("Phòng Khám YHCT")).Count(); _baocaoTL2.TsYHCT = tskhamYHCT;
                    int tskhamYHCTBH = c.Where(p => p.TenKP.Contains("Phòng Khám Đông Y") || p.TenKP.Contains("Phòng Khám YHCT")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsYHCTBH = tskhamYHCTBH;
                    int tskhamYHCTDV = c.Where(p => p.TenKP.Contains("Phòng Khám Đông Y") || p.TenKP.Contains("Phòng Khám YHCT")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsYHCTDV = tskhamYHCTDV;

                    //Phòng cấp cứu nội
                    int tscapcuunoi = c.Where(p => p.TenKP.Contains("Phòng cấp cứu nội")).Count(); _baocaoTL2.Tscapcuunoi = tscapcuunoi;
                    int tscapcuunoiBH = c.Where(p => p.TenKP.Contains("Phòng cấp cứu nội")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TscapcuunoiBH = tscapcuunoiBH;
                    int tscapcuunoiDV = c.Where(p => p.TenKP.Contains("Phòng cấp cứu nội")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TscapcuunoiDV = tscapcuunoiDV;

                    //Phòng cấp cứu ngoại
                    int tscapcuungoai = c.Where(p => p.TenKP.Contains("Phòng cấp cứu ngoại")).Count(); _baocaoTL2.Tscapcuungoai = tscapcuungoai;
                    int tscapcuungoaiBH = c.Where(p => p.TenKP.Contains("Phòng cấp cứu ngoại")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TscapcuungoaiBH = tscapcuungoaiBH;
                    int tscapcuungoaiDV = c.Where(p => p.TenKP.Contains("Phòng cấp cứu ngoại")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TscapcuungoaiDV = tscapcuungoaiDV;

                    //Phòng khám sức khỏe

                    #endregion

                    #region Tổng số bệnh nhân làm thủ thuật
                    var d = (from bn in _data.BenhNhans.Where(p => p.DTuong != "KSK")
                             join cls in _data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on bn.MaBNhan equals cls.MaBNhan
                             join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                             join kp in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on cls.MaKPth equals kp.MaKP
                             join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                             join tieunhom in _data.TieuNhomDVs.Where(p => p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tieunhom.IdTieuNhom
                             select new
                             {
                                 bn.DTuong,
                                 chidinh.MaDV,
                                 cls.MaKPth,
                                 kp.TenKP
                             }).ToList();

                    // khám nội có thủ thuật
                    int tskhamnoithuthuat = d.Where(x => x.TenKP.Contains("Phòng Khám Nội")).Count(); _baocaoTL2.Tskhamnoithuthuat = tskhamnoithuthuat;
                    int tskhamnoithuthuatBH = d.Where(x => x.TenKP.Contains("Phòng Khám Nội")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TskhamnoithuthuatBH = tskhamnoithuthuatBH;
                    int tskhamnoithuthuatDV = d.Where(x => x.TenKP.Contains("Phòng Khám Nội")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamnoithuthuatDV = tskhamnoithuthuatDV;

                    //khám nhi có thủ thuật

                    int tskhamnhithuthuat = d.Where(x => x.TenKP.Contains("Phòng khám Nhi")).Count(); _baocaoTL2.Tskhamnhithuthuat = tskhamnhithuthuat;
                    int tskhamnhithuthuatBH = d.Where(x => x.TenKP.Contains("Phòng khám Nhi")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TskhamnhithuthuatBh = tskhamnhithuthuatBH;
                    int tskhamnhithuthuatDV = d.Where(x => x.TenKP.Contains("Phòng khám Nhi")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamnhithuthuatDV = tskhamnhithuthuatDV;

                    //Phòng khám Sản có thủ thuật

                    int tskhamsanthuthuat = d.Where(x => x.TenKP.Contains("Phòng Khám Sản")).Count(); _baocaoTL2.TskhamSanthuthuat = tskhamsanthuthuat;
                    int tskhamsanthuthuatBH = d.Where(x => x.TenKP.Contains("Phòng Khám Sản")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TskhamSanthuthuatBH = tskhamsanthuthuatBH;
                    int tskhamsanthuthuatDV = d.Where(x => x.TenKP.Contains("Phòng Khám Sản")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamSanthuthuatDV = tskhamsanthuthuatDV;

                    //Phòng khám Ngoại có thủ thuât

                    int tskhamngoaithuthuat = d.Where(x => x.TenKP.Contains("Phòng Khám Ngoại")).Count(); _baocaoTL2.Tskhamngoaithuthuat = tskhamngoaithuthuat;
                    int tskhamngoaithuthuatBH = d.Where(x => x.TenKP.Contains("Phòng Khám Ngoại")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TskhamngoaithuthuatBh = tskhamngoaithuthuatBH;
                    int tskhamngoaithuthuatDV = d.Where(x => x.TenKP.Contains("Phòng Khám Ngoại")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhamngoaithuthuatDV = tskhamngoaithuthuatDV;

                    // Phòng khám RHM có thủ thuật
                    int tsRHMthuthuat = d.Where(x => x.TenKP.Contains("Phòng Khám RHM")).Count(); _baocaoTL2.TsRHMthuthuat = tsRHMthuthuat;
                    int tsRHMthuthuatBH = d.Where(x => x.TenKP.Contains("Phòng Khám RHM")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsRHMthuthuatBH = tsRHMthuthuatBH;
                    int tsRHMthuthuatDV = d.Where(x => x.TenKP.Contains("Phòng Khám RHM")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsRHMthuthuatDV = tsRHMthuthuatDV;

                    //Phòng khám TMH có thủ thuật
                    int tsTHMthuthuat = d.Where(x => x.TenKP.Contains("Phòng Khám TMH")).Count(); _baocaoTL2.TsTMHthuthuat = tsTHMthuthuat;
                    int tsTHMthuthuatBH = d.Where(x => x.TenKP.Contains("Phòng Khám TMH")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsTMHthuthuatBh = tsTHMthuthuatBH;
                    int tsTHMthuthuatDV = d.Where(x => x.TenKP.Contains("Phòng Khám TMH")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsTMHthuthuatDV = tsTHMthuthuatDV;

                    //Phòng khám mắt có thủ thuật
                    int tskhammatthuthuat = d.Where(x => x.TenKP.Contains("Phòng Khám Mắt")).Count(); _baocaoTL2.Tskhammatthuthuat = tskhammatthuthuat;
                    int tskhammatthuthuatBH = d.Where(x => x.TenKP.Contains("Phòng Khám Mắt")).Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TskhammatthuthuatBH = tskhammatthuthuatBH;
                    int tskhammatthuthuatDV = d.Where(x => x.TenKP.Contains("Phòng Khám Mắt")).Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TskhammatthuthuatDV = tskhammatthuthuatDV;

                    //Phòng khám YHCT có thủ thuật
                    int tskhamthuthuatYHCT = d.Where(p => p.TenKP.Contains("Phòng Khám Đông Y")).Count(); _baocaoTL2.TsYHCTthuthuat = tskhamthuthuatYHCT;
                    int tskhamthuthuatYHCTBH = d.Where(p => p.TenKP.Contains("Phòng Khám Đông Y")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsYHCTthuthuatBH = tskhamthuthuatYHCTBH;
                    int tskhamthuthuatYHCTDV = d.Where(p => p.TenKP.Contains("Phòng Khám Đông Y")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsYHCTthuthuatDV = tskhamthuthuatYHCTDV;

                    //Phòng cấp cứu nội có thủ thuật
                    int tscapcuunoithuthuat = d.Where(p => p.TenKP.Contains("Phòng cấp cứu nội")).Count(); _baocaoTL2.Tscapcuunoithuthuat = tscapcuunoithuthuat;
                    int tscapcuunoithuthuatBH = d.Where(p => p.TenKP.Contains("Phòng cấp cứu nội")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TscapcuunoithuthuatBH = tscapcuunoithuthuatBH;
                    int tscapcuunoithuthuatDV = d.Where(p => p.TenKP.Contains("Phòng cấp cứu nội")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TscapcuunoithuthuatDV = tscapcuunoithuthuatDV;

                    //Phòng cấp cứu ngoại có thủ thuật
                    int tscapcuungoaithuthuat = d.Where(p => p.TenKP.Contains("Phòng cấp cứu ngoại")).Count(); _baocaoTL2.Tscapcuungoaithuthuat = tscapcuungoaithuthuat;
                    int tscapcuungoaithuthuatBH = d.Where(p => p.TenKP.Contains("Phòng cấp cứu ngoại")).Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TscapcuungoaithuthuatBH = tscapcuungoaithuthuatBH;
                    int tscapcuungoaithuthuatDV = d.Where(p => p.TenKP.Contains("Phòng cấp cứu ngoại")).Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TscapcuungoaithuthuatDV = tscapcuungoaithuthuatDV;

                    //Phòng khám sức khỏe có thủ thuật

                    #endregion

                    #region Tổng số bệnh nhân xét nghiệm

                    //          SELECT     KPhong.PLoai, DichVu.IDNhom, CLS.NgayTH, BenhNhan.DTuong
                    //FROM         CLS INNER JOIN
                    //                      ChiDinh ON CLS.IdCLS = ChiDinh.IdCLS INNER JOIN
                    //                      KPhong ON CLS.MaKP = KPhong.MaKP INNER JOIN
                    //                      DichVu ON ChiDinh.MaDV = DichVu.MaDV INNER JOIN
                    //                      BenhNhan ON CLS.MaBNhan = BenhNhan.MaBNhan
                    //WHERE     (KPhong.PLoai = N'phòng khám') AND (DichVu.IDNhom = 1) AND (CLS.NgayTH BETWEEN CONVERT(DATETIME, '2019-09-01 00:00:00', 102) AND CONVERT(DATETIME, '2019-09-30 23:59:59', 102)) 
                    //                      AND (BenhNhan.DTuong <> N'KSK')

                    var k = (from bn in _data.BenhNhans
                             join cls in _data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on bn.MaBNhan equals cls.MaBNhan
                             join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                             //group new {bn,cls,chidinh} by new {bn.MaBNhan,bn.DTuong,cls.MaKP,chidinh.MaDV} into kq
                             select new
                             {
                                 bn.MaBNhan,
                                 bn.DTuong,
                                 cls.MaKP,
                                 chidinh.MaDV
                             }).ToList();
                    var k2 = (from item in k
                              join dichvu in _data.DichVus.Where(p => p.IDNhom == 1) on item.MaDV equals dichvu.MaDV
                              join tieunhomdv in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP
                              // group new {item,dichvu,tieunhomdv,kphong} by new {item.MaBNhan,tieunhomdv.TenRG,item.DTuong} into kq
                              select new
                              {
                                  item.MaBNhan,
                                  tieunhomdv.TenRG,
                                  item.DTuong
                              }).ToList();
                    // tổng toàn bộ xét nghiệm
                    int tsxn = k2.Count; _baocaoTL2.TsXN = tsxn;
                    int tsxnBh = k2.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsXNBH = tsxnBh;
                    int tsxnDV = k2.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsXNDV = tsxnDV;

                    // xet nghiệm sinh hóa 
                    int xnsinhhoa = k2.Where(p => p.TenRG == "XN hóa sinh máu").Count(); _baocaoTL2.TsSinhHoa = xnsinhhoa;
                    int xnsinhhoaBH = k2.Where(p => p.TenRG == "XN hóa sinh máu").Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsSinhHoaBH = xnsinhhoaBH;
                    int xnsinhhoaDV = k2.Where(p => p.TenRG == "XN hóa sinh máu").Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsSinhHoaDV = xnsinhhoaDV;
                    // xét nghiệm huyết học
                    int xnhuyethoc = k2.Where(p => p.TenRG == "XN huyết học").Count(); _baocaoTL2.TsHuyetHoc = xnhuyethoc;
                    int xnhuyethocBH = k2.Where(p => p.TenRG == "XN huyết học").Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsHuyetHocBH = xnhuyethocBH;
                    int xnhuyethocDV = k2.Where(p => p.TenRG == "XN huyết học").Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsHuyetHocDV = xnhuyethocDV;
                    // xét nghiệm nước tiểu
                    int xnnuoctieu = k2.Where(p => p.TenRG == "XN nước tiểu").Count(); _baocaoTL2.TsNuoctieu = xnnuoctieu;
                    int xnnuoctieuBH = k2.Where(p => p.TenRG == "XN nước tiểu").Where(x => x.DTuong == "BHYT").Count(); _baocaoTL2.TsNuoctieuBH = xnnuoctieuBH;
                    int xnnuoctieuDV = k2.Where(p => p.TenRG == "XN nước tiểu").Where(x => x.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsNuoctieuDV = xnnuoctieuDV;
                    //xét nghiệm khác
                    int xnkhac = tsxn - xnsinhhoa - xnhuyethoc - xnnuoctieu; _baocaoTL2.TsXNKhac = xnkhac;
                    int xnkhacBH = tsxnBh - xnsinhhoaBH - xnhuyethocBH - xnnuoctieuBH; _baocaoTL2.TsXNKhacBH = xnkhacBH;
                    int xnkhacDV = tsxnDV - xnsinhhoaDV - xnhuyethocDV - xnnuoctieuDV; _baocaoTL2.TsXNKhacDV = xnkhacDV;

                    #endregion

                    #region Tổng số bệnh nhân X-Quang

                    //            SELECT COUNT(ChiDinh.MaDV) AS Expr1
                    //FROM ChiDinh INNER JOIN
                    //CLS ON ChiDinh.IdCLS = CLS.IdCLS INNER JOIN
                    //DichVu ON ChiDinh.MaDV = DichVu.MaDV INNER JOIN
                    //TieuNhomDV ON DichVu.IdTieuNhom = TieuNhomDV.IdTieuNhom INNER JOIN
                    //KPhong ON CLS.MaKP = KPhong.MaKP INNER JOIN
                    //BenhNhan ON CLS.MaBNhan = BenhNhan.MaBNhan
                    //WHERE (KPhong.PLoai = N'Phòng khám') AND (BenhNhan.DTuong <> N'KSK') AND (CLS.NgayTH > CONVERT(DATETIME, '2019-09-01 00:00:00', 102)) AND
                    //(CLS.NgayTH < CONVERT(DATETIME, '2019-10-01 00:00:00', 102)) AND (TieuNhomDV.TenRG LIKE N'%X-Quang%')

                    var f2 = (from item in k
                              join dichvu in _data.DichVus on item.MaDV equals dichvu.MaDV
                              join tieunhomdv in _data.TieuNhomDVs.Where(p => p.TenRG.Contains("X-Quang")) on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP
                              select new
                              {
                                  item.MaBNhan,
                                  tieunhomdv.TenRG,
                                  item.DTuong
                              }).ToList();
                    // Tổng số bệnh nhận làm X-Quang
                    int xquang = f2.Count; _baocaoTL2.TsXQuang = xquang;
                    int xquangBH = f2.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsXQuangBH = xquangBH;
                    int xquangDV = f2.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsXQuangDV = xquangDV;

                    var f3 = (from item in k
                              join dichvu in _data.DichVus on item.MaDV equals dichvu.MaDV
                              join tieunhomdv in _data.TieuNhomDVs.Where(p => p.TenRG.Contains("Siêu âm")) on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP
                              select new
                              {
                                  item.MaBNhan,
                                  tieunhomdv.TenRG,
                                  item.DTuong
                              }).ToList();
                    //Tổng số bệnh nhân làm Siêu âm
                    int tsSieuam = f3.Count; _baocaoTL2.TsSieuam = tsSieuam;
                    int tsSieuamBH = f3.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsSieuamBH = tsSieuamBH;
                    int tsSieuamDV = f3.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsSieuamDV = tsSieuamDV;
                    //Tổng số bệnh nhân làm Siêu âm 2D
                    int tsSieuam2D = f3.Where(p => p.TenRG == "Siêu âm").Count(); _baocaoTL2.Ts2D = tsSieuam2D;
                    int tsSieuam2DBH = f3.Where(x => x.TenRG == "Siêu âm").Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.Ts2DBH = tsSieuam2DBH;
                    int tsSieuam2DDV = f3.Where(x => x.TenRG == "Siêu âm").Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.Ts2DDV = tsSieuam2DDV;
                    //Tổng số bệnh nhân làm Siêu âm 4D
                    int tsSieuam4D = f3.Where(p => p.TenRG == "Siêu âm ( Doppler )").Count(); _baocaoTL2.Ts4D = tsSieuam4D;
                    int tsSieuam4DBH = f3.Where(x => x.TenRG == "Siêu âm ( Doppler )").Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.Ts4DBH = tsSieuam4DBH;
                    int tsSieuam4DDV = f3.Where(x => x.TenRG == "Siêu âm ( Doppler )").Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.Ts4DDV = tsSieuam4DDV;
                    #endregion

                    #region Tổng số bệnh nhân nội soi tiêu hóa
                    // nội soi tiêu hóa
                    var f4 = (from item in k
                              join dichvu in _data.DichVus on item.MaDV equals dichvu.MaDV
                              join tieunhomdv in _data.TieuNhomDVs.Where(p => p.TenRG == "Nội soi") on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP
                              select new
                              {
                                  item.MaBNhan,
                                  tieunhomdv.TenRG,
                                  item.DTuong
                              }).ToList();

                    int tsNoisoi = f4.Count; _baocaoTL2.TsNoisoitieuhoa = tsNoisoi;
                    int tsNoisoiBH = f4.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsNoisoitieuhoaBH = tsNoisoiBH;
                    int tsNoisoiDV = f4.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsNoisoitieuhoaDV = tsNoisoiDV;
                    #endregion

                    #region Tổng số bệnh nhân nội soi tai mũi họng
                    //nội soi tai mũi họng
                    var f5 = (from item in k
                              join dichvu in _data.DichVus on item.MaDV equals dichvu.MaDV
                              join tieunhomdv in _data.TieuNhomDVs.Where(p => p.TenRG == "Nội soi Tai-Mũi-Họng") on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP
                              select new
                              {
                                  item.MaBNhan,
                                  tieunhomdv.TenRG,
                                  item.DTuong
                              }).ToList();

                    int tsNoisoiTMH = f5.Count; _baocaoTL2.TsNoisoiTMH = tsNoisoiTMH;
                    int tsNoisoiTMHBH = f5.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsNoisoiTMHBH = tsNoisoiTMHBH;
                    int tsNoisoiTMHDV = f5.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsNoisoiTMHDV = tsNoisoiTMHDV;
                    #endregion

                    #region Tổng số bệnh nhân nội soi cổ tử cung
                    //nội soi cổ tử cung
                    var f6 = (from item in k
                              join dichvu in _data.DichVus on item.MaDV equals dichvu.MaDV
                              join tieunhomdv in _data.TieuNhomDVs.Where(p => p.TenRG == "Nội soi cổ tử cung") on dichvu.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on item.MaKP equals kphong.MaKP
                              select new
                              {
                                  item.MaBNhan,
                                  tieunhomdv.TenRG,
                                  item.DTuong
                              }).ToList();

                    int tsNoisoiCTC = f6.Count; _baocaoTL2.TsNoisoiCTC = tsNoisoiCTC;
                    int tsNoisoiCTCBH = f6.Where(p => p.DTuong == "BHYT").Count(); _baocaoTL2.TsNoisoiCTCBH = tsNoisoiCTCBH;
                    int tsNoisoiCTCDV = f6.Where(p => p.DTuong == "Dịch vụ").Count(); _baocaoTL2.TsNoisoiCTCDV = tsNoisoiCTCDV;
                    #endregion

                    // Tổng số bn thực hiện CLS
                    int tsCls = tsxn + xquang + tsSieuam + tsNoisoi + tsNoisoiCTC + tsNoisoiTMH; _baocaoTL2.TsCLS = tsCls;
                    int tsClsBH = tsxnBh + xquangBH + tsSieuamBH + tsNoisoiBH + tsNoisoiCTCBH + tsNoisoiTMHBH; _baocaoTL2.TsCLSBH = tsClsBH;
                    int tsClsDV = tsxnDV + xquangDV + tsSieuamDV + tsNoisoiDV + tsNoisoiCTCDV + tsNoisoiTMHDV; _baocaoTL2.TsCLSDV = tsClsDV;


                    _listbaocaoTL2.Add(_baocaoTL2);
                    Dictionary<string, object> _dic = new Dictionary<string, object>();
                    _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
                    _dic.Add("TenCQ", DungChung.Bien.TenCQ);
                    string s = "Từ ngày " + tungay.Day.ToString() + " tháng " + tungay.Month.ToString() + " năm " + DateTime.Now.Year.ToString() + " ,đến ngày " + denngay.Day.ToString() + " tháng " + denngay.Month.ToString() + " năm " + denngay.Year.ToString();
                    _dic.Add("Ngaythang", s);

                    DungChung.Ham.Print(DungChung.PrintConfig.Rp_HoatDongPhongKham_NamThangLong_cs2, _listbaocaoTL2, _dic, false);
                }
            }
            #endregion
            else
            #region VIỆN KHÁC
            {
                if (ckcInDoc.Checked == true)
                {
                    DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(txttungay.Text));
                    DateTime denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtdenngay.Text));
                    QLBV_Database.QLBVEntities _data = Get_Data();
                    if (tungay > denngay)
                    {
                        MessageBox.Show("Bạn nhập ngày chưa chính xác, vui lòng nhập lại !");
                    }
                    else
                    {
                        var c = (from bn in _data.BenhNhans
                                 join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                                 join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kphong.MaKP
                                 select new
                                 {
                                     bn.MaBNhan,
                                     bn.DTuong,
                                     kphong.TenKP,
                                 }).ToList();
                        var d = (from qc in c
                                 group new { qc } by new { qc.TenKP} into k
                                 select new
                                 {
                                     TenPK = k.Key.TenKP,
                                     SoLuongTong = k.Count(),
                                     SoLuongBHYT = k.Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongDV = k.Where(p => p.qc.DTuong == "Dịch vụ").Count(),
                                 }).OrderBy(p => p.TenPK).ToList();

                        Dictionary<string, object> _dic = new Dictionary<string, object>();
                        _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
                        _dic.Add("TenCQ", DungChung.Bien.TenCQ);
                        string s = "Từ ngày " + tungay.Day.ToString() + " tháng " + tungay.Month.ToString() + " năm " + tungay.Year.ToString() + " ,đến ngày " + denngay.Day.ToString() + " tháng " + denngay.Month.ToString() + " năm " + denngay.Year.ToString();
                        _dic.Add("Ngaythang", s);

                        DungChung.Ham.Print(DungChung.PrintConfig.Rp_HoatDongPhongKham_30009, d, _dic, false);
                    }

                }
                else if (ckcInNgang.Checked == true) // nhukt 04/01/2022
                {
                    DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(txttungay.Text));
                    DateTime denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtdenngay.Text));
                    QLBV_Database.QLBVEntities _data = Get_Data();
                    if (tungay > denngay)
                    {
                        MessageBox.Show("Bạn nhập ngày chưa chính xác, vui lòng nhập lại !");
                    }
                    else
                    {
                        var c = (from bn in _data.BenhNhans
                                 join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan
                                 join kphong in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kphong.MaKP
                                 select new TTLuotKham
                                 {
                                     MaBNhan = bn.MaBNhan,
                                     DTuong = bn.DTuong,
                                     KPhong = kphong.TenKP,
                                     NgayKham = bnkb.NgayKham,
                                 }).OrderBy(p => p.NgayKham).ToList();

                        List<TTLuotKham> kp = new List<TTLuotKham>();
                        foreach (var item in c)
                        {
                            TTLuotKham q = new TTLuotKham();
                            q.MaBNhan = item.MaBNhan;
                            q.KPhong = item.KPhong;
                            q.DTuong = item.DTuong;
                            q.NgayKham = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.NgayKham)));
                            kp.Add(q);
                        }
                        var d = (from qc in kp
                                 group new { qc } by new {qc.NgayKham } into k
                                 select new
                                 {
                                     NgayKham = k.Key.NgayKham,
                                     SoLuongBHYTPK1 = k.Where(p => p.qc.KPhong == "Phòng khám 1 - Nội").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK2 = k.Where(p => p.qc.KPhong == "Phòng khám 2-Nội").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK3 = k.Where(p => p.qc.KPhong == "Phòng khám 3 - Nội").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK4 = k.Where(p => p.qc.KPhong == "Phòng khám 4 - Tiểu đường").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK5 = k.Where(p => p.qc.KPhong == "Phòng khám 5 Nội - Tiểu đường").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK6 = k.Where(p => p.qc.KPhong == "Phòng khám 6 - Nhi").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK7 = k.Where(p => p.qc.KPhong == "Phòng khám 7 - ngoại").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPK8 = k.Where(p => p.qc.KPhong == "Phòng khám 8 - đông y").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPKCOPD = k.Where(p => p.qc.KPhong == "Phòng khám bệnh phổi tắc nghẽn mãn tính  (COPD)").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPKMat = k.Where(p => p.qc.KPhong == "Phòng khám Mắt").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPKPhuSan = k.Where(p => p.qc.KPhong == "Phòng khám Phụ Sản").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPKRHM = k.Where(p => p.qc.KPhong == "phòng Khám Răng Hàm Mặt").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPKTMH = k.Where(p => p.qc.KPhong == "Phòng khám Tai - Mũi - Họng").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongBHYTPKTruyenNhiem = k.Where(p => p.qc.KPhong == "Phòng Khám Truyền Nhiễm").Where(p => p.qc.DTuong == "BHYT").Count(),
                                     SoLuongDV = k.Where(p => p.qc.DTuong == "Dịch vụ").Count(),
                                     SoLuongBH = k.Where(p => p.qc.DTuong == "BHYT").Count(),
                                 }).OrderBy(p => p.NgayKham).ToList();
                        Dictionary<string, object> _dic = new Dictionary<string, object>();
                        _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
                        _dic.Add("TenCQ", DungChung.Bien.TenCQ);
                        string s = "Từ ngày " + tungay.Day.ToString() + " tháng " + tungay.Month.ToString() + " năm " + tungay.Year.ToString() + ", đến ngày " + denngay.Day.ToString() + " tháng " + denngay.Month.ToString() + " năm " + denngay.Year.ToString();
                        _dic.Add("NgayThang", s);

                        DungChung.Ham.Print(DungChung.PrintConfig.Rp_HoatDongPhongKham_30009_Ngang, d, _dic, false);
                    }
                }
            }
            #endregion
        }
        public class ListKP
        {
            public string TenKP { get; set; }
        }

        public class TTLuotKham
        {
            public int MaBNhan { get; set; }
            public string DTuong { get; set; }
            public string KPhong { get; set; }
            public DateTime? NgayKham { get; set; }
        }

        private void Frm_BCHoatDongPK_cs2_01049_Load(object sender, EventArgs e)
        {
            txttungay.Text = DateTime.Now.Date.ToString();
            txtdenngay.Text = DateTime.Now.Date.ToString();
            if (DungChung.Bien.MaBV == "30009")
            {
                ckcInDoc.Visible = true;
                ckcInNgang.Visible = true;
            }
        }

        private void ckcInDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcInDoc.Checked == true)
            {
                ckcInNgang.Checked = false;
            }
        }

        private void ckcInNgang_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcInNgang.Checked == true)
            {
                ckcInDoc.Checked = false;
            }
        }
    }
    public class HoatDongPK {
        public DateTime NgayKham { get; set; }
        public string PK1 { get; set; }
        public string PK2 { get; set; }
        public string PK3 { get; set; }
        public string PK4 { get; set; }
        public string PK5 { get; set; }
        public string PK6 { get; set; }
        public string PK7 { get; set; }
        public string PK8 { get; set; }
        public string PK9 { get; set; }
        public string PK10 { get; set; }
        public string PK11 { get; set; }
        public string PK12 { get; set; }
        public string PK13 { get; set; }
        public string PK14 { get; set; }
        public string PK15 { get; set; }
        public string PK16 { get; set; }
        public string PK17 { get; set; }
    }
}