using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.DungChung
{
  public  class LayDinhDanh
    {
        public int MaBNhan { set; get; }
        public string PID { set; get; }// mã định danh cá nhân (13 ký tự)
        public string fULLNAME { set; get; }// họ tên bệnh nhân (bắt buộc)
        public string sEX { set; get; }// giới tính : 1- nam; 2- nữ (bắt buộc)
        public string dATEOFBIRTH { set; get; }// ngày sinh 19881020 (bắt buộc)
        public string pLACEOFBIRTH { set; get; } // quê quán        
        public string hOMETOWN { set; get; } // quê quán
        public string hEALTHINSURANCEIDCARD { set; get; } // mã thẻ BHYT
        public string iDENTIICATIONIDCARD { set; get; }// chứng minh thư, thẻ căn cước công dân
        public string pHONE { set; get; } // số điện thoại
        public string cURENTPROVICECODE { set; get; } // mã tỉnh hiện tại - 2 ký tự
        public string cURENTDISTRICTCODE { set; get; }// mã huyện hiện tại -- 3 ký tự
        public string cURENTCOMMUNECODE { set; get; } // mã xã thị trấn hiện tại -- 5 ký tự
        public string nAMEOFCONSERVATOR { set; get; } // họ tên người bảo hộ
        public string rELATIONWITHCONSERVATOR { set; get; } // quan hệ với người bảo hộ //số - 2 ký tự - TT36/2014/TT-BCA)
        public string iDOFCONSERVATOR { set; get; } // chứng minh thư, thẻ căn cước người bảo hộ
        public string pHONEOFCONSERVATOR { set; get; }// điện thoại người bảo hộ
        public string pROVINCIALBIRTHREGISTRATIONCODE { set; get; } // mã tỉnh đăng ký khai sinh // 3kys tự
    }
}
