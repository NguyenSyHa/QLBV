using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.CLS
{
    public partial class frm_NgayCD : Form
    {
        private readonly int _idCLS;
        private readonly int _maBN;
        private readonly string _TenTN;
        private readonly string _TenDV;
        private readonly string _TenRG;
        public frm_NgayCD(int idCLS,string TenTN,string TenDV,string TenRG,int Mabn)
        {
            InitializeComponent();
            _TenDV = TenDV;
            _TenTN = TenTN;
            _idCLS = idCLS;
            _TenRG = TenRG;
            _maBN = Mabn;
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);


        private void btn_cdNgay_Click(object sender, EventArgs e)
        {
            DateTime _ngayhen = new DateTime();
            _ngayhen = dtCD.DateTime;
            {
                string _inMauCD = "A4";
                if (_idCLS != null && _TenRG != null)
                {
                    frmIn frm = new frmIn();
                    if (_TenTN == "X-Quang" || _TenTN == "Siêu âm" || _TenTN == "Điện tim" || _TenTN == "Nội soi" || _TenTN == "Nội soi Tai-Mũi-Họng" || _TenTN == "Thủ thuật" || _TenTN == "Phẫu thuật" || _TenTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiDaDay || _TenTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiCoTuCung)
                    {
                        int ID_CDHA = 2;
                        string TenPhieu = "PHIẾU CHỈ ĐỊNH CĐHA";
                        CLS.InPhieu.InChiDinh_SL_Ngay(_data, frm, _inMauCD, _maBN, _idCLS, true, "", ID_CDHA, TenPhieu,_ngayhen);                                    
                    }
                    else if (_TenTN == "Thăm dò chức năng")
                    {
                        frmIn frmxn = new frmIn();
                        int ID_ThamDo = 3;
                        string TenPhieu = "PHIẾU CHỈ ĐỊNH THĂM GIÒ CHỨC NĂNG";
                        CLS.InPhieu.InChiDinh_SL_Ngay(_data, frmxn, _inMauCD, _maBN, _idCLS, true, "", ID_ThamDo, TenPhieu, _ngayhen);
                    }
                    else
                    {
                        frmIn frmxn = new frmIn();
                        int ID_XN = 1;
                        string TenPhieu = "PHIẾU CHỈ ĐỊNH XÉT NGHIỆM";
                        CLS.InPhieu.InChiDinh_SL_Ngay(_data, frmxn, _inMauCD, _maBN, _idCLS, true, "", ID_XN, TenPhieu, _ngayhen);
                    }
                }
                return;
            }
        }

        private void frm_NgayCD_Load(object sender, EventArgs e)
        {
            dtCD.DateTime = System.DateTime.Now;
        }
    }
}
