using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_CheckIn : DevExpress.XtraEditors.XtraForm
    {
        public frm_CheckIn()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhNhan> _lBenhNhan = new List<BenhNhan>();
        private void TimKiem()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            int id_DoiTuongKham = -1;
            var dtuong = _data.DTBNs.Where(p => p.HTTT == 1).Select(p => p.IDDTBN).ToList();
            if (dtuong.Count > 0)
                id_DoiTuongKham = dtuong.First();
            _lBenhNhan = _data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.MaKCB==DungChung.Bien.MaBV && p.NNhap <= ngayden && p.IDDTBN == id_DoiTuongKham && (rad_ExPort.SelectedIndex==0?p.Export==false: p.Export==true)).ToList();
            grcDSBN.DataSource = null;
            grcDSBN.DataSource = _lBenhNhan;
        }
        private void btn_TimKiem_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_ExPort.SelectedIndex == 1)
                btn_ExPort.Text = "Xóa dữ liệu";
            else
                btn_ExPort.Text = "Gửi dữ liệu";
            TimKiem();
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            for (int i = 0; i < grvDSBN.RowCount; i++)
            {
                grvDSBN.SetRowCellValue(i,colChon, true);

            }
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            for (int i = 0; i < grvDSBN.RowCount; i++)
            {
                grvDSBN.SetRowCellValue(i, colChon, false);

            }
        }

        private void rad_ExPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            hyp_HuyChon_OpenLink(null,null);
        }
        string user = "", pass = "";
        public void _getvalue_US(string u, string p)
        {
            this.user = u;
            this.pass = p;

        }
        public static bool export_CheckIn(QLBV_Database.QLBVEntities _data, int _mabn, bool _delete, string user, string pass,int loai)
        {
            bool b = false;
            var bn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            foreach (var item in bn)
            {
                if (_delete)
                {
                    DungChung.cls_KetNoi_BYT cls = new DungChung.cls_KetNoi_BYT();

                    b = !cls.createCheckInFile(_data, _mabn, DungChung.Bien.xmlFilePath_LIS[3], 2, true, user, pass,2);
                }
                else
                {
                    DungChung.cls_KetNoi_BYT cls = new DungChung.cls_KetNoi_BYT();

                    b = cls.createCheckInFile(_data, _mabn, DungChung.Bien.xmlFilePath_LIS[3], 0, true, user, pass,2);
                }
                item.Export = b;
                _data.SaveChanges();
            }
            return true;
        }
    
        private void btn_ExPort_Click(object sender, EventArgs e)
        {
            ChucNang.CheckUser frm_c = new ChucNang.CheckUser("Nhập tài khoản do BYT cấp!");
            frm_c.ok = new ChucNang.CheckUser._getdata(_getvalue_US);
            frm_c.ShowDialog();
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool delete = false;
            if (rad_ExPort.SelectedIndex == 1)
                delete = true;
            if (string.IsNullOrEmpty(user))
                return;
            for (int i = 0; i < grvDSBN.RowCount; i++)
            {
                if (grvDSBN.GetRowCellValue(i, colChon) != null && grvDSBN.GetRowCellValue(i, colChon).ToString().ToLower() == "true")
                {
                    int mbn=Convert.ToInt32(grvDSBN.GetRowCellValue(i,colMaBNhan));
                    export_CheckIn(_data, mbn,delete,user,pass,2);
                }

            }
            MessageBox.Show(DungChung.cls_KetNoi_BYT._error[0].ToString() + DungChung.cls_KetNoi_BYT._error[1].ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChucNang.CheckUser frm_c = new ChucNang.CheckUser("Nhập tài khoản do BYT cấp!");
            frm_c.ok = new ChucNang.CheckUser._getdata(_getvalue_US);
            frm_c.ShowDialog();
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool delete = false;
            if (rad_ExPort.SelectedIndex == 1)
                delete = true;
            if (string.IsNullOrEmpty(user))
                return;
            for (int i = 0; i < grvDSBN.RowCount; i++)
            {
                if (grvDSBN.GetRowCellValue(i, colChon) != null && grvDSBN.GetRowCellValue(i, colChon).ToString().ToLower() == "true")
                {
                    int mbn = Convert.ToInt32(grvDSBN.GetRowCellValue(i, colMaBNhan));
                    export_CheckIn(_data, mbn, delete, user, pass, 1);
                }

            }
            MessageBox.Show(DungChung.cls_KetNoi_BYT._error[0].ToString() + DungChung.cls_KetNoi_BYT._error[1].ToString());
        }
    }
}