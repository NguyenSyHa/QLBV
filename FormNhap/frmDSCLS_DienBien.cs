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
    public partial class frmDSCLS_DienBien : Form
    {
        public delegate void DelegateCLS(List<ClsADO> data);
        private DelegateCLS delegateCLS;
        List<ClsADO> listCLS = new List<ClsADO>();

        public frmDSCLS_DienBien(List<ClsADO> _data, DelegateCLS _delegateCLS)
        {
            InitializeComponent();
            try
            {
                this.delegateCLS = _delegateCLS;
                listCLS = _data;
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
                gridControlDSBN.DataSource = listCLS;
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
                    var row = (ClsADO)gridViewDSBN.GetRow(gridViewDSBN.GetRowHandle(e.ListSourceRowIndex));
                    if (row != null)
                    {
                        if (e.Column.FieldName == "STT")
                        {
                            e.Value = e.ListSourceRowIndex + 1;
                        }
                        else if (e.Column.FieldName == "NgayTH_Str")
                        {
                            if (row.NgayTH != null)
                                e.Value = row.NgayTH.Value.ToString("dd/MM/yyyy HH:mm");
                            else
                                e.Value = "";
                        }
                        else if (e.Column.FieldName == "NgayThang_Str")
                        {
                            if (row.NgayThang != null)
                                e.Value = row.NgayThang.Value.ToString("dd/MM/yyyy HH:mm");
                            else
                                e.Value = "";
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
                var bnAdo = (List<ClsADO>)gridControlDSBN.DataSource;
                if (bnAdo != null && bnAdo.Count > 0)
                {
                    if (!bnAdo.Exists(o => o.Check))
                    {
                        MessageBox.Show("Bạn chưa chọn CLS", "Thông báo");
                        return;
                    }
                    List<ClsADO> list = new List<ClsADO>();
                    foreach (var item in bnAdo)
                    {
                        if (item.Check)
                            list.Add(item);
                    }
                    if (delegateCLS != null)
                        delegateCLS(list);
                }
                this.Close();

            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void linkLabelSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (var item in listCLS)
            {
                item.Check = true;
            }
            gridControlDSBN.BeginUpdate();
            gridControlDSBN.DataSource = listCLS;
            gridControlDSBN.EndUpdate();
        }

        private void linkLabelUnSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (var item in listCLS)
            {
                item.Check = false;
            }
            gridControlDSBN.BeginUpdate();
            gridControlDSBN.DataSource = listCLS;
            gridControlDSBN.EndUpdate();
        }
    }
}
