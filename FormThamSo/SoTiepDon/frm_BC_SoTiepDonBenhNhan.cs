using DevExpress.XtraEditors;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo.SoTiepDon
{
    public partial class frm_BC_SoTiepDonBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public string TEN = "";
        public int MaKPkb = 0;
        int Luu = 1;
        public frm_BC_SoTiepDonBenhNhan()
        {
            InitializeComponent();
            this.Load += (e, r) =>
            {

            };
        }
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);

        private void cboDTuong_EditValueChanged(object sender, EventArgs e)
        {
            //if (!formLoad)
            //    TimKiem();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.repBC_SoTiepDon rep = new BaoCao.repBC_SoTiepDon();

            _dttu = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtDenNgay.DateTime);

            int _makp = 0;
            if (lupMaKP.EditValue != null)
            {

                _makp = Convert.ToInt32(lupMaKP.EditValue);
            }

            int iddtbn = 0;

            if (cboDTuong.EditValue != null)
            {
                iddtbn = Convert.ToInt32(cboDTuong.EditValue);
            }

            var dsTiepDon = (from bnhan in data.BenhNhans
                             where bnhan.NNhap >= _dttu && bnhan.NNhap <= _dtden
                             where iddtbn == 99 ? true : bnhan.IDDTBN == iddtbn
                             where _makp == 0 ? true : bnhan.MaKP == _makp
                             join kphong in data.KPhongs on bnhan.MaKP equals kphong.MaKP
                             join canbo in data.CanBoes on bnhan.MaCB equals canbo.MaCB
                             join ttbs in data.TTboXungs on bnhan.MaBNhan equals ttbs.MaBNhan
                             join bnkb in data.BNKBs on bnhan.MaBNhan equals bnkb.MaBNhan into g
                             select new
                             {
                                 MaBNhan = data.BenhNhans.Where(s => s.MaBNhan == bnhan.MaBNhan).Select(s => s.MaBNhan).Distinct().FirstOrDefault(),
                                 bnhan.TenBNhan,
                                 Ngaysinh = bnhan.NgaySinh + "/" + bnhan.ThangSinh + "/" + bnhan.NamSinh,
                                 GTinh = bnhan.GTinh == 1 ? "Nam" : "Nữ",
                                 bnhan.DChi,
                                 bnhan.DTuong,
                                 bnhan.IDDTBN,
                                 bnhan.SThe,
                                 CMT = ttbs.SoKSinh,
                                 DienThoai = ttbs.DThoai + "; " + ttbs.DThoaiNT,
                                 CDNoiGT = data.BenhViens.Where(s => s.MaBV == bnhan.MaBV).Select(x => x.TenBV).FirstOrDefault(),
                                 TenKP = bnhan.Status == 0 ? data.KPhongs.Where(s => s.MaKP == bnhan.MaKP).Select(x => x.TenKP).FirstOrDefault() : data.KPhongs.Where(s => s.MaKP == g.FirstOrDefault().MaKP).Select(x => x.TenKP).FirstOrDefault(),
                                 TenCB = bnhan.Status == 0 ? data.CanBoes.Where(s => s.MaCB == bnhan.MaCB).Select(s => s.TenCB).FirstOrDefault() : data.CanBoes.Where(s => s.MaCB == g.FirstOrDefault().MaCB).Select(s => s.TenCB).Take(1).FirstOrDefault(),
                                 bnhan.NNhap
                             })
                             .OrderByDescending(p => p.NNhap)
                             .ToList();

            if (dsTiepDon.Count > 0)
            {
                rep.DataSource = dsTiepDon;
                int SoTT = 1;
                foreach (var item in dsTiepDon)
                {
                    rep.SoTT.Value = SoTT;
                    rep.MaBNhan.Value = item.MaBNhan;
                    rep.TenBNhan.Value = item.TenBNhan;
                    rep.NgaySinh.Value = item.Ngaysinh;
                    rep.GTinh.Value = item.GTinh;
                    rep.DChi.Value = item.DChi;
                    rep.DTuong.Value = item.DTuong;
                    rep.SThe.Value = item.SThe;
                    rep.CMT.Value = item.CMT;
                    rep.DienThoai.Value = item.DienThoai;
                    rep.CDNoiGT.Value = item.CDNoiGT;
                    rep.TenKP.Value = item.TenKP;
                    rep.TenCB.Value = item.TenCB;
                    rep.NNhap.Value = item.NNhap;
                    SoTT++;
                }
                rep.DataBinding();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa tiếp đón bệnh nhân có đội tượng này tại phòng khoa này trong khoảng thời gian này");
            }
        }

        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            //if (!formLoad)
            //    TimKiem();
        }
        
        private void frm_BC_SoTiepDonBenhNhan_Load(object sender, EventArgs e)
        {
            formLoad = true;
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.DateTime = System.DateTime.Now;
            dtDenNgay.DateTime = System.DateTime.Now;
            List<listKP> Kphong = (from kp in DaTaContext.KPhongs.Where(p => p.Status == 1) select new listKP { MaKP = kp.MaKP, TenKP = kp.TenKP }).OrderBy(p => p.TenKP).ToList();
            List<DTBN> _ldtbn = DaTaContext.DTBNs.OrderBy(p => p.DTBN1).ToList();
            _ldtbn.Add(new DTBN { IDDTBN = 99, DTBN1 = " Tất cả" });
            cboDTuong.Properties.DataSource = null;
            cboDTuong.Properties.DataSource = _ldtbn.OrderBy(p => p.DTBN1);

            if (Kphong.Count > 0)
            {
                Kphong.Insert(0, new listKP { MaKP = 0, TenKP = " Tất cả" });
                lupMaKP.Properties.DataSource = Kphong.OrderBy(p => p.TenKP).ToList();
            }
        }
        bool formLoad = false;
        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!formLoad)
            //    TimKiem();
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!formLoad)
            //    TimKiem();
        }
    }

    public class listKP
    {
        public int makp;
        public string tenkp;
        public int MaKP
        {
            set { makp = value; }
            get { return makp; }
        }
        public string TenKP
        {
            set { tenkp = value; }
            get { return tenkp; }

        }

        public string PLoai { get; set; }
    }
}