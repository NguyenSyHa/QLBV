using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.Class
{
    public class HSSK_PID
    {
        public thongtinBN THONG_TIN_BENH_NHAN { get; set; }

        public thongtintheBHYT THONG_TIN_THE_BHYT { get; set; }
    }

    public class thongtinBN
    {
        public string HO_TEN { get; set; }
        public string MA_GIOI_TINH { get; set; }
        public string NGAY_SINH { get; set; }
        public string MA_TINH_NOI_SINH { get; set; }
        public string MA_HUYEN_NOI_SINH { get; set; }
        public string MA_XA_NOI_SINH { get; set; }
        public string SO_CMND { get; set; }
        public string DIEN_THOAI_DD { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI_THUONG_TRU { get; set; }
        public string MA_TINH_THUONG_TRU { get; set; }
        public string MA_HUYEN_THUONG_TRU { get; set; }
        public string MA_XA_THUONG_TRU { get; set; }
        public string DIA_CHI_HIEN_TAI { get; set; }
        public string MA_TINH_HIEN_TAI { get; set; }
        public string MA_HUYEN_HIEN_TAI { get; set; }
        public string MA_XA_HIEN_TAI { get; set; }
        public string MA_NGHE_NGHIEP { get; set; }
        public string NOI_LAM_VIEC { get; set; }
        public string MA_DAN_TOC { get; set; }
        public string MA_QUOC_TICH { get; set; }
        public string MA_TON_GIAO { get; set; }
        public string MA_TRINH_DO_HOCVAN { get; set; }
        public string MA_QUANHE_NGUOI_BAO_HO { get; set; }
        public string HO_TEN_NGUOI_BAO_HO { get; set; }
        public string DIEN_THOAI_NGUOI_BAO_HO { get; set; }
        public string SO_CMND_NGUOI_BAO_HO { get; set; }
    }

    public class thongtintheBHYT
    {
        public string MA_THE { get; set; }
        public string MA_DKBD { get; set; }
        public string MA_KHU_VUC { get; set; }
        public string GT_THE_TU { get; set; }
        public string GT_THE_DEN { get; set; }
        public string DIA_CHI_BHYT { get; set; }
        public string GT_5NAM_LIENTUC { get; set; }
        public string MIEN_CUNG_CT { get; set; }
    }


    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class KQLogin
    {
        public string Code { get; set; }
        public HsskToken Data { get; set; }
        public string Message { get; set; }
    }
    public class HsskToken
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public int Refreshrefresh_expires_in { get; set; }
        public string Refresh_token { get; set; }
        public string Token_type { get; set; }
        public string Session_state { get; set; }
        public string Not_before_policy { get; set; }
        public string Scope { get; set; }
    }

    public class KQ_PID
    {
        public string status { set; get; }
        public string pId { set; get; }
        public string message { set; get; }

    }

    public class uploadResult
    {
        public string maGiaoDich { set; get; }
        public string message { set; get; }
        public string status { set; get; }
    }

}
