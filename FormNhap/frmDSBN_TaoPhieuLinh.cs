using DevExpress.Data;
using QLBV.Class;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frmDSBN_TaoPhieuLinh : Form
    {
        public delegate void DelegateBN(List<BenhNhanADO> data);
        private DelegateBN delegateBn;
        List<BenhNhanADO> listBenhNhan = new List<BenhNhanADO>();

        public frmDSBN_TaoPhieuLinh(List<BenhNhanADO> _data, DelegateBN _delegateBn)
        {
            InitializeComponent();
            try
            {
                this.delegateBn = _delegateBn;
                listBenhNhan = _data;
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }

        }

        private void frmDSBN_TaoPhieuLinh_Load(object sender, EventArgs e)
        {
            try
            {
                gridControlDSBN.BeginUpdate();
                gridControlDSBN.DataSource = listBenhNhan;
                gridControlDSBN.EndUpdate();
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void gridViewDSBN_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
                {
                    var row = (BenhNhanADO)gridViewDSBN.GetRow(gridViewDSBN.GetRowHandle(e.ListSourceRowIndex));
                    if (row != null)
                    {
                        if (e.Column.FieldName == "STT")
                        {
                            e.Value = e.ListSourceRowIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var bnAdo = (List<BenhNhanADO>)gridControlDSBN.DataSource;
                if (bnAdo != null && bnAdo.Count > 0)
                {
                    if (!bnAdo.Exists(o => o.Check))
                    {
                        MessageBox.Show("Bạn chưa chọn bệnh nhân", "Thông báo");
                        return;
                    }
                    List<BenhNhanADO> list = new List<BenhNhanADO>();
                    foreach (var item in bnAdo)
                    {
                        if (item.Check)
                            list.Add(item);
                    }
                    if (delegateBn != null)
                        delegateBn(list);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void linklblSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (var item in listBenhNhan)
            {
                item.Check = true;
            }
            gridControlDSBN.BeginUpdate();
            gridControlDSBN.DataSource = listBenhNhan;
            gridControlDSBN.EndUpdate();
        }

        private void linklblUnselectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (var item in listBenhNhan)
            {
                item.Check = false;
            }
            gridControlDSBN.BeginUpdate();
            gridControlDSBN.DataSource = listBenhNhan;
            gridControlDSBN.EndUpdate();
        }
    }
}
