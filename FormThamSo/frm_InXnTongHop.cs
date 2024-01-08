using System;
using QLBV_Database;
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
    public partial class frm_InXnTongHop : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<string> maDVcts;
        List<int> _idcls = new List<int>();
        int idcd;
        public frm_InXnTongHop(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        public frm_InXnTongHop(int mabn, List<int> id)
        {
            InitializeComponent();
            _mabn = mabn;
            _idcls = id;
        }
        int _mabn = 0;
        QLBV_Database.QLBVEntities _data;
        List<InXNTongHop> listData;
        List<InXNTongHop> q2;
        private void frm_InXnTongHop_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _ltn = _data.TieuNhomDVs.ToList();
            listData = (from cls in _data.CLS.Where(p => p.MaBNhan == _mabn)
                        join cd in _data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                        join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                        select new InXNTongHop { IdTieuNhom = (dv.IdTieuNhom ?? 0), NgayThang = cls.NgayThang, TenKP = kp.TenKP, MaMay = cd.MaMay, IdCLS = cls.IdCLS, IdCD = cd.IDCD }).Distinct().ToList();
            var q11 = (from qq in listData
                       select new { qq.IdTieuNhom, qq.NgayThang, qq.TenKP, qq.MaMay, qq.IdCLS }
                         ).Distinct().ToList();
            q2 = (from q in _ltn.Where(p => p.IDNhom == 1)
                  join b in q11 on q.IdTieuNhom equals b.IdTieuNhom
                  select new InXNTongHop { IdCLS = b.IdCLS, TenTN = q.TenTN, IdTieuNhom = q.IdTieuNhom, NgayThang = b.NgayThang, TenKP = b.TenKP, MaMay = b.MaMay }).ToList();
            gridControlTN.DataSource = q2;
            if (DungChung.Bien.MaBV == "24272")
            {
                rdo_TrongNgoaiDM.Visible = true;
                rdo_TrongNgoaiDM.SelectedIndex = 2;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DungChung.Bien.check = 1;
            List<InXNTongHop> _ltn = new List<InXNTongHop>();
            var selectRows = gridViewTN.GetSelectedRows();
            string tenKP = "";
            string maMay = "";
            int trongDM = 0;
            foreach (var item in selectRows)
            {
                var row = (InXNTongHop)gridViewTN.GetRow(item);
                if (row != null)
                {
                    foreach (var subItem in q2)
                    {
                        if (row.IdCLS == subItem.IdCLS)
                        {
                            tenKP = row.TenKP;
                            maMay = row.MaMay;
                            _ltn.Add(subItem);
                        }
                    }
                }
            }
            if (_ltn.Count() > 0)
            {
                if (DungChung.Bien.MaBV == "24272")
                {
                    trongDM = rdo_TrongNgoaiDM.SelectedIndex;
                    CLS.InPhieu._inPhieuXNTongHop_24272(_mabn, trongDM, _ltn, false, tenKP, maMay);
                }
                else
                {
                    CLS.InPhieu._inPhieuXNTongHop(_mabn, _ltn, false, tenKP, maMay,0,_idcls);
                }
            }
        }
        public void InPhieuTongHop3004(int mabn, int idcls, string TenKP = "", string MaMay = "")
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<InXNTongHop> _ltn = new List<InXNTongHop>();
            _ltn = (from cd in _data.ChiDinhs.Where(p => p.IdCLS == idcls)
                    join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                    join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                    join tndv in _data.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                    join tn in _data.DichVucts on clsct.MaDVct equals tn.MaDVct
                    select new InXNTongHop
                    {
                        IdTieuNhom = tndv.IdTieuNhom,
                        TenTN = tndv.TenTN,
                        MaMay = cd.MaMay ?? "",
                        IdCLS = cd.IdCLS ?? 0,
                        IdCD = cd.IDCD,
                    }).ToList();
            if (_ltn.Count() > 0)
            {
                var kps = (from cls in _data.CLS.Where(p => p.IdCLS == idcls)
                           join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                           select new { kp.TenKP }).ToList();

                if (DungChung.Bien.MaBV == "24012" 
                    || DungChung.Bien.MaBV == "27194")
                    MaMay = _ltn.First().MaMay;
                if (DungChung.Bien.MaBV == "24012")
                    CLS.InPhieu._inPhieuXNTongHop24012(_mabn, _ltn, false, TenKP = kps.FirstOrDefault().TenKP ?? "", MaMay, idcls);
                else
                    CLS.InPhieu._inPhieuXNTongHop(_mabn, _ltn, false, TenKP = kps.FirstOrDefault().TenKP ?? "", MaMay, idcls, _idcls);
            }
        }
        public class InXNTongHop
        {
            public int IdTieuNhom { get; set; }
            public string TenTN { get; set; }
            public DateTime? NgayThang { get; set; }
            public string TenKP { get; set; }
            public string MaMay { get; set; }
            public int IdCLS { get; set; }
            public int IdCD { get; set; }
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            gridViewTN.SelectAll();
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            gridViewTN.ClearSelection();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}