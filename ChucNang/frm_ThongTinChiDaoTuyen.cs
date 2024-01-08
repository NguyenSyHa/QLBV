using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;


namespace QLBV.ChucNang
{
    public partial class frm_ThongTinChiDaoTuyen : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public List<ChiDaoTuyenADO> listChiDaoTuyenADO = new List<ChiDaoTuyenADO>();
        public List<ChiDaoTuyen> listChiDaoTuyen = new List<ChiDaoTuyen>();

        public frm_ThongTinChiDaoTuyen()
        {
            InitializeComponent();
        }

        private void frm_ThongTinChiDaoTuyen_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void LoadChiDaoTuyen()
        {
            listChiDaoTuyen = (from cdt in dataContext.ChiDaoTuyens
                               select cdt).ToList();
            if (listChiDaoTuyen != null && listChiDaoTuyen.Count > 0)
                listChiDaoTuyenADO = (from cdt in listChiDaoTuyen
                                      select new ChiDaoTuyenADO(cdt)).ToList();
            cboChiDaoTuyen.Properties.DataSource = listChiDaoTuyenADO;
        }

        public class ChiDaoTuyenADO : ChiDaoTuyen
        {
            public string TextChiDaoTuyen { get; set; }
            public ChiDaoTuyenADO(ChiDaoTuyen data)
            {
                LibraryStore.Mapper.DataObjectMapper.Map<ChiDaoTuyenADO>(this, data);
                this.TextChiDaoTuyen =
                    (this.SoLop != null ? "Số lớp đào tạo cho tuyến dưới: " + SoLop.ToString() + "; " : "")
                    + (this.SoNguoi != null ? "Số người tham dự: " + SoNguoi.ToString() + "; " : "")
                    + (this.SoLanKB != null ? "Số lần khám bệnh, khám sức khỏe định kỳ: " + SoLanKB.ToString() + "; " : "")
                    + (this.SoNgay != null ? "Số ngày khám: " + SoNgay.ToString() + "; " : "")
                    + (this.SoCanBo != null ? "Số cán bộ tham gia khám: " + SoCanBo.ToString() + "; " : "")
                    + (this.SoLanTT != null ? "Số lần tuyên truyền phòng chống dịch: " + SoLanTT.ToString() + "; " : "")
                    + (this.SoBuoi != null ? "Số buổi tham gia các chương trình Y tế quốc gia CSSKBĐ: " + SoBuoi.ToString() + "; " : "")
                    + (this.SoHDKhac != null ? "Các hoạt động khác: " + SoHDKhac.ToString() + "; " : "");
            }

        }

        private void grv_HDNghienCuu_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int row = e.RowHandle;

            if (e.Column.Name == "colXoa")
            {
                if (grv_HDNghienCuu.GetRowCellValue(row, colTenDeTai) != null && grv_HDNghienCuu.GetRowCellValue(row, colTenDeTai).ToString() != "")
                {
                    string tendetai = grv_HDNghienCuu.GetRowCellValue(row, colTenDeTai).ToString();
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đề tài " + tendetai + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        GridView view = sender as GridView;
                        view.DeleteRow(row);
                        //if (row >= 0)
                        //{
                        //    GridView view = sender as GridView;
                        //    view.DeleteRow(row);
                        //}
                        //else
                        //{
                        //    grv_HDNghienCuu.SetFocusedRowCellValue(colTenDeTai, "");
                        //    grv_HDNghienCuu.SetFocusedRowCellValue(colSLcapBo, "");
                        //    grv_HDNghienCuu.SetFocusedRowCellValue(colSLcapCoSo, "");
                        //    grv_HDNghienCuu.SetFocusedRowCellValue(colSLcapNN, "");
                        //}
                    }
                }
                else
                {
                    GridView view = sender as GridView;
                    view.DeleteRow(row);
                    //if (row >= 0)
                    //{
                    //    GridView view = sender as GridView;
                    //    view.DeleteRow(row);
                    //}
                    //else
                    //{                        
                    //    grv_HDNghienCuu.SetFocusedRowCellValue(colSLcapBo, "");
                    //    grv_HDNghienCuu.SetFocusedRowCellValue(colSLcapCoSo, "");
                    //    grv_HDNghienCuu.SetFocusedRowCellValue(colSLcapNN, "");
                    //}
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            RefreshData();

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            List<DeTai> list = new List<DeTai>();
            for (int i = 0; i < grv_HDNghienCuu.RowCount; i++)
            {
                if (grv_HDNghienCuu.GetRowCellValue(i, colTenDeTai) != null && grv_HDNghienCuu.GetRowCellValue(i, colTenDeTai).ToString() != "")
                {
                    DeTai moi = new DeTai();
                    moi.TenDeTai = grv_HDNghienCuu.GetRowCellValue(i, colTenDeTai).ToString();
                    if (grv_HDNghienCuu.GetRowCellValue(i, colSLcapNN) != null && grv_HDNghienCuu.GetRowCellValue(i, colSLcapNN).ToString() != "" && grv_HDNghienCuu.GetRowCellValue(i, colSLcapNN).ToString() != "0")
                        moi.SLNN = Convert.ToInt32(grv_HDNghienCuu.GetRowCellValue(i, colSLcapNN));
                    if (grv_HDNghienCuu.GetRowCellValue(i, colSLcapBo) != null && grv_HDNghienCuu.GetRowCellValue(i, colSLcapBo).ToString() != "" && grv_HDNghienCuu.GetRowCellValue(i, colSLcapBo).ToString() != "0")
                        moi.SLB = Convert.ToInt32(grv_HDNghienCuu.GetRowCellValue(i, colSLcapBo));
                    if (grv_HDNghienCuu.GetRowCellValue(i, colSLcapCoSo) != null && grv_HDNghienCuu.GetRowCellValue(i, colSLcapCoSo).ToString() != "" && grv_HDNghienCuu.GetRowCellValue(i, colSLcapCoSo).ToString() != "0")
                        moi.SLCS = Convert.ToInt32(grv_HDNghienCuu.GetRowCellValue(i, colSLcapCoSo));
                    list.Add(moi);
                }
            }

            frmIn frm = new frmIn();
            BaoCao.rep_ChiDaoTuyen rep = new BaoCao.rep_ChiDaoTuyen(list);
            rep.xrSubreport1.ReportSource = new QLBV.BaoCao.rep_sub_ChiDaoTuyen_DeTaiKH();
            rep.col1.Text = textEdit1.Text;
            rep.col2.Text = textEdit2.Text;
            rep.col31.Text = textEdit3.Text;
            rep.col32.Text = textEdit4.Text;
            rep.col33.Text = textEdit5.Text;
            rep.col4.Text = textEdit6.Text;
            rep.col5.Text = textEdit7.Text;
            rep.col6.Text = textEdit8.Text;
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void gridLookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
                cboChiDaoTuyen.EditValue = null;
        }

        private void cboChiDaoTuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (cboChiDaoTuyen.EditValue == null)
            {
                cboChiDaoTuyen.Properties.Buttons[1].Visible = false;
                RefreshData();
            }
            else
            {
                cboChiDaoTuyen.Properties.Buttons[1].Visible = true;
                SetEnableButton(false);
                if (listChiDaoTuyen != null)
                {
                    var data = listChiDaoTuyen.FirstOrDefault(o => o.ID == LibraryStore.TypeConvert.Parse.ToInt32(cboChiDaoTuyen.EditValue.ToString()));
                    if (data != null)
                    {
                        textEdit1.Text = data.SoLop.ToString();
                        textEdit2.Text = data.SoNguoi.ToString();
                        textEdit3.Text = data.SoLanKB.ToString();
                        textEdit4.Text = data.SoNgay.ToString();
                        textEdit5.Text = data.SoCanBo.ToString();
                        textEdit6.Text = data.SoLanTT.ToString();
                        textEdit7.Text = data.SoBuoi.ToString();
                        textEdit8.Text = data.SoHDKhac.ToString();
                        var detai = dataContext.DeTais.Where(o => o.ChiDaoTuyenID == data.ID).ToList();
                        bindingSource1.DataSource = detai;
                    }
                    else
                    {
                        textEdit1.Text = "";
                        textEdit2.Text = "";
                        textEdit3.Text = "";
                        textEdit4.Text = "";
                        textEdit5.Text = "";
                        textEdit6.Text = "";
                        textEdit7.Text = "";
                        textEdit8.Text = "";
                        bindingSource1.DataSource = new List<DeTai>();
                    }
                }
            }
        }

        private void SetEnableButton(bool value)
        {
            btnAdd.Enabled = value;
            btnDelete.Enabled = !value;
            btnEdit.Enabled = !value;
            btnIn.Enabled = !value;
        }

        private void RefreshData()
        {
            try
            {
                LoadChiDaoTuyen();
                SetEnableButton(true);
                textEdit1.Text = "";
                textEdit2.Text = "";
                textEdit3.Text = "";
                textEdit4.Text = "";
                textEdit5.Text = "";
                textEdit6.Text = "";
                textEdit7.Text = "";
                textEdit8.Text = "";
                List<DeTai> listDetai = new List<DeTai>();
                bindingSource1.DataSource = listDetai;
                grc_HDNghienCuu.DataSource = bindingSource1;
                cboChiDaoTuyen.EditValue = null;
            }
            catch (Exception ex)
            {
                //Library.Logging.WriteLog.Error(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ChiDaoTuyen cdt = new ChiDaoTuyen();
            cdt.SoLop = LibraryStore.TypeConvert.Parse.ToInt32(textEdit1.Text);
            cdt.SoNguoi = LibraryStore.TypeConvert.Parse.ToInt32(textEdit2.Text);
            cdt.SoLanKB = LibraryStore.TypeConvert.Parse.ToInt32(textEdit3.Text);
            cdt.SoNgay = LibraryStore.TypeConvert.Parse.ToInt32(textEdit4.Text);
            cdt.SoCanBo = LibraryStore.TypeConvert.Parse.ToInt32(textEdit5.Text);
            cdt.SoLanTT = LibraryStore.TypeConvert.Parse.ToInt32(textEdit6.Text);
            cdt.SoBuoi = LibraryStore.TypeConvert.Parse.ToInt32(textEdit7.Text);
            cdt.SoHDKhac = LibraryStore.TypeConvert.Parse.ToInt32(textEdit8.Text);
            var addCDT = dataContext.ChiDaoTuyens.Add(cdt);
            dataContext.SaveChanges();
            var chiDaoTuyenID = addCDT.ID;
            if (bindingSource1.DataSource != null && chiDaoTuyenID > 0)
            {
                var data = (List<DeTai>)bindingSource1.DataSource;
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        item.ChiDaoTuyenID = chiDaoTuyenID;
                        dataContext.DeTais.Add(item);
                    }
                }
            }
            dataContext.SaveChanges();
            RefreshData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cboChiDaoTuyen.EditValue != null)
            {
                var id = LibraryStore.TypeConvert.Parse.ToInt32(cboChiDaoTuyen.EditValue.ToString());
                var editCDT = dataContext.ChiDaoTuyens.FirstOrDefault(o => o.ID == id);
                editCDT.SoLop = LibraryStore.TypeConvert.Parse.ToInt32(textEdit1.Text);
                editCDT.SoNguoi = LibraryStore.TypeConvert.Parse.ToInt32(textEdit2.Text);
                editCDT.SoLanKB = LibraryStore.TypeConvert.Parse.ToInt32(textEdit3.Text);
                editCDT.SoNgay = LibraryStore.TypeConvert.Parse.ToInt32(textEdit4.Text);
                editCDT.SoCanBo = LibraryStore.TypeConvert.Parse.ToInt32(textEdit5.Text);
                editCDT.SoLanTT = LibraryStore.TypeConvert.Parse.ToInt32(textEdit6.Text);
                editCDT.SoBuoi = LibraryStore.TypeConvert.Parse.ToInt32(textEdit7.Text);
                editCDT.SoHDKhac = LibraryStore.TypeConvert.Parse.ToInt32(textEdit8.Text);
                if (bindingSource1.DataSource != null && editCDT != null)
                {
                    var dtExist = dataContext.DeTais.Where(o => o.ChiDaoTuyenID == id).ToList();
                    var data = (List<DeTai>)bindingSource1.DataSource;
                    if (data != null && data.Count > 0)
                    {
                        var delete = dtExist.Where(o => !data.Where(q => q.ID != 0).Select(p => p.ID).Contains(o.ID)).ToList();
                        if (delete != null)
                        {
                            foreach (var de in delete)
                            {
                                dataContext.DeTais.Remove(de);
                            }
                        }

                        var add = data.Where(o => o.ID == 0).ToList();
                        if (add != null)
                        {
                            foreach (var ad in add)
                            {
                                ad.ChiDaoTuyenID = id;
                                dataContext.DeTais.Add(ad);
                            }
                        }

                        var edit = dtExist.Where(o => data.Where(q => q.ID != 0).Select(p => p.ID).Contains(o.ID)).ToList();
                        if (delete != null)
                        {
                            foreach (var ed in edit)
                            {
                                var d = dataContext.DeTais.FirstOrDefault(o => o.ID == ed.ID);
                                d.SLB = ed.SLB;
                                d.SLCS = ed.SLCS;
                                d.SLNN = ed.SLNN; d.TenDeTai = ed.TenDeTai;
                            }
                        }
                    }
                }
                dataContext.SaveChanges();
                RefreshData();
            }
            else
                return;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (cboChiDaoTuyen.EditValue != null)
                {
                    var id = LibraryStore.TypeConvert.Parse.ToInt32(cboChiDaoTuyen.EditValue.ToString());
                    var deleteCDT = dataContext.ChiDaoTuyens.FirstOrDefault(o => o.ID == id);
                    var remove = dataContext.ChiDaoTuyens.Remove(deleteCDT);
                    var detai = dataContext.DeTais.Where(o => o.ChiDaoTuyenID == remove.ID).ToList();
                    if (detai != null && detai.Count > 0)
                    {
                        foreach (var item in detai)
                        {
                            dataContext.DeTais.Remove(item);
                        }
                    }
                    dataContext.SaveChanges();
                    RefreshData();
                }
                else
                    return;
            }
        }
    }
}