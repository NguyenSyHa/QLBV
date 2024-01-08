using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace QLBV.Admin
{
    public partial class frm_CopyQuyen : Form
    {
        Action reload;
        string tenDN;
        public frm_CopyQuyen(string _tenDN, Action _reload = null)
        {
            InitializeComponent();
            tenDN = _tenDN;
            reload = _reload;
        }

        private void frm_CopyQuyen_Load(object sender, EventArgs e)
        {
            loadAdmin();
        }

        private void loadAdmin()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenDNSearch = txtTenDangNhap.Text;
            var q = (from a in _data.ADMINs
                     join cb in _data.CanBoes on a.MaCB equals cb.MaCB
                     join kp in _data.KPhongs on cb.MaKP equals kp.MaKP
                     select new TTDN { ID = a.ID, MaCB = a.MaCB, TenDN = a.TenDN, TenGoi = a.TenGoi, TenKP = kp.TenKP }
                    ).Where(p => tenDNSearch == "" || p.TenDN.Contains(tenDNSearch) || p.TenGoi.Contains(tenDNSearch)).OrderBy(p => p.TenDN).ThenBy(p => p.TenGoi).ToList();
            grc_Admin.DataSource = q;
        }

        public class TTDN
        {
            public int ID { get; set; }
            public string MaCB { get; set; }
            public string TenDN { get; set; }
            public string TenGoi { get; set; }
            public string TenKP { get; set; }
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            loadAdmin();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var pemissions = dataContext.Permissions.Where(o => o.TenDN == tenDN).ToList();
            if (pemissions != null && pemissions.Count > 0)
            {
                var check = grv_Admin.GetSelectedRows();
                if (check.Count() <= 0)
                {
                    MessageBox.Show("Chưa chọn tài khoản đích");
                    return;
                }

                bool success = false;

                foreach (var item in check)
                {
                    var row = (TTDN)grv_Admin.GetRow(item);
                    if (row != null)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                var deletes = dataContext.Permissions.Where(o => o.TenDN == row.TenDN).ToList();
                                foreach (var x in deletes)
                                {
                                    var delete = dataContext.Permissions.FirstOrDefault(o => o.ID == x.ID && o.TenDN == x.TenDN);
                                    if (delete != null)
                                    {
                                        dataContext.Permissions.Remove(delete);
                                        dataContext.SaveChanges();
                                    }
                                }
                                scope.Complete();
                            }
                            catch (Exception ex)
                            {
                                scope.Dispose();
                                throw ex;
                            }

                        }

                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                foreach (var per in pemissions)
                                {
                                    Permission copy = new Permission();
                                    LibraryStore.Mapper.DataObjectMapper.Map<Permission>(copy, per);
                                    copy.TenDN = row.TenDN;
                                    copy.ID = per.ID;
                                    dataContext.Permissions.Add(copy);
                                    dataContext.SaveChanges();
                                }
                                scope.Complete();
                            }
                            catch (Exception ex)
                            {
                                scope.Dispose();
                                throw ex;
                            }
                        }
                    }
                }
                MessageBox.Show("Copy quyền thành công");
                if (reload != null)
                    reload();
                this.Close();
            }
            else
            {
                MessageBox.Show(string.Format("Tài khoản :{0} chưa được thiết lập quyền", tenDN));
            }
        }
    }
}
