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
    public partial class frm_NhapBenhNhanLao : Form
    {
        int _MaBNhan = 0;
        // int _luu = 0; //trạng thái: 0- thêm mới; 1- sửa
        public frm_NhapBenhNhanLao()
        {
            InitializeComponent();
        }
        public frm_NhapBenhNhanLao(int MaBNhan)
        {
            InitializeComponent();
            this._MaBNhan = MaBNhan;
            // this._luu = luu;
        }
        List<Obj> _ListDtuong = new List<Obj>();
        private void frm_NhapBenhNhanLao_Load(object sender, EventArgs e)
        {
            List<Obj> lTinhTrangH = new List<Obj>();
            lTinhTrangH.Add(new Obj { Value = 0, Name = "Không xác định" });
            lTinhTrangH.Add(new Obj { Value = 1, Name = "1" });
            lTinhTrangH.Add(new Obj { Value = 2, Name = "2" });
            lTinhTrangH.Add(new Obj { Value = 3, Name = "3" });
            lupTinhTrangH.Properties.DataSource = lTinhTrangH;
            lupTinhTrangH.Properties.DisplayMember = "Name";
            lupTinhTrangH.Properties.ValueMember = "Value";
            lupTinhTrangH.EditValue = 0;

            List<Obj> listchandoan = new List<Obj>();
            listchandoan.Add(new Obj { Value = 0, Name = "" });
            listchandoan.Add(new Obj { Value = 1, Name = "Lao" });
            listchandoan.Add(new Obj { Value = 2, Name = "Lao đa kháng" });
            listchandoan.Add(new Obj { Value = 3, Name = "Lao siêu kháng" });
            listchandoan.Add(new Obj { Value = 4, Name = "Mycobacteria không lao (NTM)" });
            lupChanDoan.Properties.DataSource = listchandoan;
            lupChanDoan.Properties.DisplayMember = "Name";
            lupChanDoan.Properties.ValueMember = "Value";
            lupChanDoan.EditValue = lupChanDoan.Properties.GetKeyValueByDisplayText("");

            rdTienSuDtriLao.SelectedIndex = 2;
            //_ListDtuong.Add(new Obj { LoaiChanDoan = 0, Value = 0, Name = "---Chọn đối tượng---" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 1, Value = 0, Name = "" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 1, Value = 1, Name = "1. Nhóm H nghi lao" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 1, Value = 2, Name = "2. Trẻ em nghi lao" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 1, Value = 3, Name = "3. Nghi lao phổi (lao phổi hoặc lao phổi AFB âm)" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 1, Value = 4, Name = "4. Nghi lao ngoài phổi" });

            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 0, Name = "" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 1, Name = "1. Thất bại Phác đồ II" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 2, Name = "2. Nghi lao/BN lao tiếp xúc với BN lao kháng thuốc" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 3, Name = "3. Thất bại Phác đồ I" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 4, Name = "4. Không âm hóa sau 2 hoặc 3 tháng điều trị PĐ I hoặc PĐ II" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 5, Name = "5. Tái phát PĐ I hoặc PĐ II" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 6, Name = "6. Điều trị lại sau bỏ trị PĐ I và/ hoặc PĐ II" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 7, Name = "7. Bệnh nhân lao/ H mới" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 8, Name = "8. Khác: người nghi lao có tiền sử điều trị thuốc lao trên 1 tháng" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 2, Value = 9, Name = "9. Bệnh nhân lao mới" });

            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 0, Name = "" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 1, Name = "1. Nghi thất bại PĐ IV" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 2, Name = "2. Thất bại PĐ IV" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 3, Name = "3. Tiền sử điều trị thuốc lao hàng 2 trên 1 tháng" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 4, Name = "4. Nghi lao/BN lao tiếp xúc với BN lao siêu/ tiền siêu kháng thuốc" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 5, Name = "5. Kháng Rifampicin" });
            _ListDtuong.Add(new Obj { LoaiChanDoan = 3, Value = 6, Name = "6. Khác" });
            lupChanDoan_EditValueChanged(sender, e);

            // load thông tin bn lao
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            TTboXung bn = data.TTboXungs.Where(p => p.MaBNhan == _MaBNhan).FirstOrDefault();
            if (bn != null)
            {
                txtSo_eTBM.Text = bn.So_eTBM;
                if (bn.ThangTheoDoi != null)
                    txtThangThoiDoi.Text = bn.ThangTheoDoi.ToString();

                if (bn.ChanDoanLao != null && bn.ChanDoanLao > 0)
                    lupChanDoan.EditValue = bn.ChanDoanLao.Value;

                if (bn.DTuongLao != null && bn.DTuongLao > 0)
                    lupDoiTuong.EditValue = bn.DTuongLao.Value;

                if (bn.TinhTrangH != null && bn.TinhTrangH > 0)
                    lupTinhTrangH.EditValue = bn.TinhTrangH.Value;

                if (bn.TienSuDTri != null)
                    rdTienSuDtriLao.SelectedIndex = bn.TienSuDTri.Value;
                txtDtuongLaoKhac.Text = bn.DTuongLaoKhac;
            }

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOK_Click(object sender, EventArgs e)
        {
            int dtuong = 0;
            if (lupDoiTuong.EditValue != null)
                dtuong = Convert.ToInt32(lupDoiTuong.EditValue);
            if (kt())
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                #region thêm mới
                TTboXung bn = data.TTboXungs.Where(p => p.MaBNhan == _MaBNhan).FirstOrDefault();
                if (bn == null)
                {
                    bn = new TTboXung();
                    bn.MaBNhan = _MaBNhan;
                    bn.So_eTBM = txtSo_eTBM.Text;
                    bn.ThangTheoDoi = Convert.ToInt32(txtThangThoiDoi.Text);
                    bn.TinhTrangH = Convert.ToInt32(lupTinhTrangH.EditValue);
                    bn.ChanDoanLao = Convert.ToInt32(lupChanDoan.EditValue);
                    if(rdTienSuDtriLao.SelectedIndex == 0 || rdTienSuDtriLao.SelectedIndex == 1)
                    bn.TienSuDTri = rdTienSuDtriLao.SelectedIndex;
                    bn.DTuongLao = dtuong;
                    bn.DTuongLaoKhac = txtDtuongLaoKhac.Text;
                    data.TTboXungs.Add(bn);
                    if (data.SaveChanges() >= 0)
                        MessageBox.Show("Cập nhật thành công");

                }
                #endregion

                else
                {
                    #region Sửa
                    bn.So_eTBM = txtSo_eTBM.Text;
                    bn.ThangTheoDoi = Convert.ToInt32(txtThangThoiDoi.Text);
                    bn.TinhTrangH = Convert.ToInt32(lupTinhTrangH.EditValue);
                    bn.ChanDoanLao = Convert.ToInt32(lupChanDoan.EditValue);
                    if (rdTienSuDtriLao.SelectedIndex == 0 || rdTienSuDtriLao.SelectedIndex == 1)
                        bn.TienSuDTri = rdTienSuDtriLao.SelectedIndex;
                    else
                        bn.TienSuDTri = null;
                    bn.DTuongLao = dtuong;
                    bn.DTuongLaoKhac = txtDtuongLaoKhac.Text;
                    if (data.SaveChanges() >= 0)
                        MessageBox.Show("Cập nhật thành công");
                    #endregion
                }
            }
        }

        private bool kt()
        {
            if (String.IsNullOrEmpty(lblErr.Text))
                return true;
            else
                return false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // class đối tượng hoặc Phân loại đối tượng chẩn đoán
        public class Obj
        {
            public int LoaiChanDoan { set; get; }//1: Lao; 2: Đa kháng; 3: Siêu kháng; 4: NTM (chỉ áp dụng với đối tượng chẩn đoán lao)
            public int Value { set; get; }
            public string Name { set; get; }
            public string GhiChu { set; get; }  //đối với đối tượng chẩn đoán là khác

        }

        private void lupChanDoan_EditValueChanged(object sender, EventArgs e)
        {
            int dtuong = Convert.ToInt32(lupChanDoan.EditValue);
            var q = _ListDtuong.Where(p => p.LoaiChanDoan == dtuong || p.LoaiChanDoan == 0).OrderBy(p => p.LoaiChanDoan).ThenBy(p => p.Value).ToList();
            lupDoiTuong.Properties.DataSource = q;
            lupDoiTuong.Properties.DisplayMember = "Name";
            lupDoiTuong.Properties.ValueMember = "Value";
            // lupDoiTuong.EditValue = lupDoiTuong.Properties.GetKeyValueByDisplayText("---Chọn đối tượng---");
            lupDoiTuong.EditValue = 1;

        }

        private void txtThangThoiDoi_EditValueChanged(object sender, EventArgs e)
        {
            int ot;
            if (!Int32.TryParse(txtThangThoiDoi.Text, out ot))
                lblErr.Text = "Số tháng theo dõi phải là số nguyên dương";
            else
                lblErr.Text = "";
        }

        private void txtThangThoiDoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void lupDoiTuong_EditValueChanged(object sender, EventArgs e)
        {
            txtDtuongLaoKhac.Enabled = false;
            if (lupChanDoan.EditValue != null && lupDoiTuong.EditValue != null)
            {
                if ((Convert.ToInt32(lupChanDoan.EditValue) == 1 && Convert.ToInt32(lupDoiTuong.EditValue) == 4) || (Convert.ToInt32(lupChanDoan.EditValue) == 3 && Convert.ToInt32(lupDoiTuong.EditValue) == 6))
                    txtDtuongLaoKhac.Enabled = true;
            }

        }


    }
}
