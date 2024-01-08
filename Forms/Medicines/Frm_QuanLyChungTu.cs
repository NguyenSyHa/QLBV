using DevExpress.XtraEditors;
using QLBV.Providers.Business.Medicines;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.Forms.Medicines
{
    public partial class Frm_QuanLyChungTu : DevExpress.XtraEditors.XtraForm
    {
        private readonly QLBVEntities _dataContext;
        private readonly ChungTusProvider _chungTusProvider;
        private readonly bool isNhapDuoc;

        public Frm_QuanLyChungTu(QLBVEntities dataContext, bool isNhapDuoc)
        {
            InitializeComponent();
            _dataContext = dataContext;
            _chungTusProvider = new ChungTusProvider(dataContext);
            this.isNhapDuoc = isNhapDuoc;
        }

        private void Frm_QuanLyChungTu_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;

            lupMaCB.DataSource = _dataContext.CanBoes.ToList();

            lupKhoNhan.Properties.DataSource = _chungTusProvider.GetListKhoaPhong(DungChung.Bien.MaCB, 0);

            lupKhoNhan.EditValue = DungChung.Bien.MaKP;
        }

        public void Search()
        {
            DateTime tungay = dtTuNgay.DateTime;
            DateTime denngay = dtDenNgay.DateTime;

            int maKNhan = 0;
            if(lupKhoNhan.EditValue != null)
                maKNhan = Convert.ToInt32(lupKhoNhan.EditValue);

            grcDaNhan.DataSource = _chungTusProvider.GetCTsDaNhan(dtTuNgay.DateTime, dtDenNgay.DateTime, maKNhan, isNhapDuoc);
            grcYCXoa.DataSource = _chungTusProvider.GetCTsYCXoa(dtTuNgay.DateTime, dtDenNgay.DateTime, maKNhan, isNhapDuoc);
        }

        private void btnYCXoa_Click(object sender, EventArgs e)
        {
            string soCT = "";
            if (grvDaNhan.GetFocusedRowCellValue(colSoCT) != null && grvDaNhan.GetFocusedRowCellValue(colSoCT).ToString() != "")
                soCT = grvDaNhan.GetFocusedRowCellValue(colSoCT).ToString();

            int maKNhan = lupKhoNhan.EditValue != null ? Convert.ToInt32(lupKhoNhan.EditValue) : 0;

            _chungTusProvider.SetMaKPYeuCau(soCT, maKNhan);

            Search();
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            string soCT = "";
            if (grvDaNhan.GetFocusedRowCellValue(colSoCT) != null && grvDaNhan.GetFocusedRowCellValue(colSoCT).ToString() != "")
                soCT = grvDaNhan.GetFocusedRowCellValue(colSoCT).ToString();

            int maKNhan = lupKhoNhan.EditValue != null ? Convert.ToInt32(lupKhoNhan.EditValue) : 0;

            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa chứng từ số " + soCT + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rs == DialogResult.Yes)
            {
                if (_chungTusProvider.IsDuThuoc(soCT))
                {
                    _chungTusProvider.DeleteChungTuBySoCT(soCT);
                    _chungTusProvider.UpdateStockInMedicineList(soCT);

                }
                else
                {
                    MessageBox.Show("Chứng từ có thuốc/vật tư không đủ số lượng tồn, vui lòng kiểm tra lại", "Thông báo!");
                }
            }

            Search();
        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void lupKhoNhan_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void btnHuyYeuCau_Click(object sender, EventArgs e)
        {
            string soCT = "";
            if (grvDaNhan.GetFocusedRowCellValue(colSoCT) != null && grvDaNhan.GetFocusedRowCellValue(colSoCT).ToString() != "")
                soCT = grvDaNhan.GetFocusedRowCellValue(colSoCT).ToString();

            int? maKNhan = null;

            _chungTusProvider.SetMaKPYeuCau(soCT, maKNhan);

            Search();
        }

        private void grvDaNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string status = "";
            if (grvDaNhan.GetFocusedRowCellDisplayText(colTrangThai) != null && grvDaNhan.GetFocusedRowCellDisplayText(colTrangThai) != "")
                status = grvDaNhan.GetFocusedRowCellDisplayText(colTrangThai);

            if (status == "Chưa yêu cầu")
            {
                btnYCXoa.Enabled = true;
                btnHuyYeuCau.Enabled = false;
            }
            else if (status == "Đã yêu cầu")
            {
                btnYCXoa.Enabled = false;
                btnHuyYeuCau.Enabled = true;
            }
            else
            {
                btnYCXoa.Enabled = false;
                btnHuyYeuCau.Enabled = false;
            }
        }
    }
}
