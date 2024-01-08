using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static QLBV.FormThamSo.frm_kqcls;

namespace QLBV.FormThamSo
{
    public partial class Frm_ChonKQXN : DevExpress.XtraEditors.XtraForm
    {

        QLBVEntities _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;

        int mabn = 0;
        List<Status_CD> _lstatus_cd = new List<Status_CD>();

        public Frm_ChonKQXN(int mabn)
        {
            InitializeComponent();
            this.mabn = mabn;
        }

        private void Frm_ChonKQXN_Load(object sender, EventArgs e)
        {
            _lstatus_cd.Add(new Status_CD { Ten = "Chưa làm", Status = 0 });
            _lstatus_cd.Add(new Status_CD { Ten = "Đã làm", Status = 1 });
            _lstatus_cd.Add(new Status_CD { Ten = "Đã tiếp nhận", Status = 2 });
            _lstatus_cd.Add(new Status_CD { Ten = "Hủy", Status = -1 });

            lupStatus.DataSource = _lstatus_cd;

            var rs = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == mabn)
                      join bn in _dataContext.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                      join cd in _dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                      join clsct in _dataContext.CLScts on cd.IDCD equals clsct.IDCD
                      join dv in _dataContext.DichVus on cd.MaDV equals dv.MaDV
                      join dvct in _dataContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                      join tn in _dataContext.TieuNhomDVs.Where(p => p.IDNhom == 1) on dv.IdTieuNhom equals tn.IdTieuNhom
                      group new { cls, clsct, cd, dv, dvct, tn } by new { clsct.Id, tn.TenRG, dvct.TenDVct, dvct.TSBT, clsct.KetQua, clsct.Status, cls.NgayThang, cls.NgayTH, bn.GTinh, dvct.TSnTu, dvct.TSnDen, dvct.TSnuTu, dvct.TSnuDen, dvct.DonVi } into kq
                      select new
                      {
                          kq.Key.Id,
                          TenRG = kq.Key.TenRG,
                          TenXN = kq.Key.TenDVct,
                          TSBT = kq.Key.TSBT,
                          KetQua = kq.Key.KetQua,
                          Status = kq.Key.Status,
                          NgayThang = kq.Key.NgayThang,
                          NgayTH = kq.Key.NgayTH,
                          TSTu = kq.Key.GTinh == 1 ? kq.Key.TSnTu : kq.Key.TSnuTu,
                          TSDen = kq.Key.GTinh == 1 ? kq.Key.TSnDen : kq.Key.TSnuDen,
                          DonVi = kq.Key.DonVi
                      }).ToList();

            grcCLS.DataSource = rs;
            grvCLS.SelectAll();
        }

        public List<int> GetKQXN()
        {
            List<int> list = new List<int>();
            var selectedRow = grvCLS.GetSelectedRows();

            for (int i = 0; i < grvCLS.SelectedRowsCount; i++)
            {
                if (selectedRow[i] >= 0)
                {
                    if (grvCLS.GetRowCellValue(selectedRow[i], colID) != null)
                    {
                        list.Add(Convert.ToInt32(grvCLS.GetRowCellValue(selectedRow[i], colID)));
                    }
                    
                }
            }

            return list;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}