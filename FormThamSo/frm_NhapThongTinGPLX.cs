using QLBV.DataCommunication;
using QLBV.DataCommunication.Models;
using QLBV.DungChung;
using QLBV.Models.Business.DataCommunication;
using QLBV.Providers.Business.Datacommunication;
using QLBV.Providers.Dictionaries.CanBo;
using QLBV.Signature.Models;
using QLBV.Utilities.Commons;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_NhapThongTinGPLX : Form
    {
        private readonly int _mbn;
        private readonly int _idkb;
        private ConnectData connect;
        private readonly DataCommunicationProvider _datacommunicationProvider;
        QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);

        private StaffProvider _staffProvider;
        public StaffProvider StaffProvider
        {
            get
            {
                if (_staffProvider == null)
                    _staffProvider = new StaffProvider();

                return _staffProvider;
            }
        }

        public frm_NhapThongTinGPLX(int mabn, int idkb)
        {
            InitializeComponent();

            _datacommunicationProvider = new DataCommunicationProvider();
            _mbn = mabn;
            _idkb = idkb;
        }
        private void btnGui_Click(object sender, EventArgs e)
        {
            DataCommunicationModel dataCommunication = _datacommunicationProvider.GetGPLX(_mbn);

            var bn = DaTaContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mbn);
            dataCommunication.IDBENHVIEN = DungChung.Bien.MaBV;
            dataCommunication.BENHVIEN = DungChung.Bien.TenCQ;

            #region check thông tin thiếu

            List<string> strErros = new List<string>();

            if (string.IsNullOrEmpty(dataCommunication.SOCMND_PASSPORT))
            {
                string erro = "Số chứng minh thư";
                txt_edit_socmt.Enabled = true;
                strErros.Add(erro);
            }
            else
                txt_edit_socmt.Enabled = false;

            if (string.IsNullOrEmpty(dataCommunication.NOICAP))
            {
                string erro = "Nơi cấp";
                txt_edit_noicapcmt.Enabled = true;
                strErros.Add(erro);
            }
            else
                txt_edit_noicapcmt.Enabled = false;

            if (string.IsNullOrEmpty(dataCommunication.NGAYTHANGNAMCAPCMND) || string.IsNullOrWhiteSpace(dataCommunication.NGAYTHANGNAMCAPCMND))
            {
                string erro = "Ngày tháng cấp CMT";
                dt_edit_ngaycapcmt.Enabled = true;
                strErros.Add(erro);
            }
            else
                dt_edit_ngaycapcmt.Enabled = false;

            if (string.IsNullOrEmpty(bn.NgaySinh) || string.IsNullOrWhiteSpace(bn.NgaySinh))
            {
                string erro = "Ngày sinh";
                txt_edit_NgaySinh.Enabled = true;
                strErros.Add(erro);
            }
            else
                txt_edit_NgaySinh.Enabled = false;

            if (string.IsNullOrEmpty(bn.ThangSinh) || string.IsNullOrWhiteSpace(bn.ThangSinh))
            {
                string erro = "Tháng sinh";
                txt_edit_ThangSinh.Enabled = true;
                strErros.Add(erro);
            }
            else
                txt_edit_ThangSinh.Enabled = false;

            if (strErros.Count > 0)
            {
                this.ClientSize = new System.Drawing.Size(451, 298);
                MessageBox.Show(string.Format("Bệnh nhân thiếu một số thông tin {0}. Xin vui lòng kiểm tra lại!", string.Join(", ", strErros)));
                return;
            }

            //if (dataCommunication.SOCMND_PASSPORT == null || dataCommunication.NOICAP == null || string.IsNullOrEmpty(dataCommunication.NGAYTHANGNAMCAPCMND.Trim()) || bn.First().NgaySinh == "  " || bn.First().ThangSinh == "  ")
            //{
            //    MessageBox.Show("Thông tin không hợp lệ");
            //    //txt_edit_socmt.Enabled = true;
            //    //txt_edit_noicapcmt.Enabled = true;
            //    //dt_edit_ngaycapcmt.Enabled = true;
            //    //txt_edit_NgaySinh.Enabled = true;
            //    //txt_edit_ThangSinh.Enabled = true;
            //    return;
            //}
            #endregion
            var dataCommunicationXml = AppConfig.MyMapper.Map<DataCommunicationXmlModel>(dataCommunication);

            var xml = XMLHelper.SerializeObject(dataCommunicationXml, "root");
            CreatePath.Path(AppDomain.CurrentDomain.BaseDirectory + "Xmls"); // Tạo thư mục xml để chứa file ký
            var xmlName = bn.MaBNhan + "_" + Helpers.RemoveDiacritics(bn.TenBNhan) + "_" + dataCommunicationXml.Id;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Xmls\" + $"{xmlName}.xml";

            // Xuất file xml
            File.WriteAllText(filePath, xml);

            // Danh sach benh vien ky so bang USB
            var hospitalCodes = new List<string>()
            {
                "30010",
                "30004"
            };

            var isSignature = true;

            string username = DungChung.Bien.xmlFilePath_LIS[10];
            string pass = DungChung.Bien.xmlFilePath_LIS[11];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Chưa nhập tên đăng nhập hoặc mật khẩu");
                return;
            }

            // Các bênh viện ký số bằng USB
            if (hospitalCodes.Any(a => a.Equals(DungChung.Bien.MaBV)))
                isSignature = Helpers.SignXmlFile(filePath);
            else // Ký số của misa
            {
                var staff = StaffProvider.GetStaffByCode(DungChung.Bien.MaCB);

                var login = new LoginModel()
                {
                    Username = staff.Email,
                    Password = Security.Decrypt(staff.MKChuKySo),
                    PhoneNumber = staff.SoDT,
                    FileName = filePath,
                    Id = Guid.NewGuid().ToString(),
                    FirstName = staff.TenCB,
                    XmlId = dataCommunicationXml.Id,
                };

                if (staff.ChuKySo != null
                    && staff.ChuKySo.Length > 0)
                {
                    login.Image = new ImageModel()
                    {
                        Img = staff.ChuKySo,
                        SignatureImage = staff.ChuKySo != null ? Convert.ToBase64String(staff.ChuKySo) : null
                    };
                }

                isSignature = Task.Run(async () => await Signature.Signature.SignMisa(login)).Result;
            }

            if (isSignature)
            {
                var xmlToBytes = File.ReadAllBytes(filePath);
                var xmlToBase64String = Convert.ToBase64String(xmlToBytes);
                dataCommunication.SIGNDATA = xmlToBase64String;

                Task.Run(async () => await DataCommunication.DataCommunication.SyncDataCommunication(dataCommunication, username, pass, _idkb));
            }
        }

        private void frm_NhapThongTinGPLX_Load(object sender, EventArgs e)
        {
            connect = Program._connect;
            this.ClientSize = new System.Drawing.Size(518, 191);
            var bngplx = DaTaContext.GPLXes.Where(p => p.IDKB == _idkb).ToList();
            var idbnkb = (from bnkb in DaTaContext.BNKBs.Where(p => p.IDKB == _idkb)
                          join bn in DaTaContext.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                          join ttbx in DaTaContext.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                          select new
                          {
                              bn.NgaySinh,
                              bn.ThangSinh,
                              ttbx.SoKSinh,
                              ttbx.NgayCapCMT,
                              ttbx.NoiCapCMT
                          }).ToList();
            var canbo =(from cb in DaTaContext.CanBoes
                        join ad in DaTaContext.ADMINs on cb.MaCB equals ad.MaCB
                        where cb.Status == 1
                        select new { cb.TenCB ,cb.MaCB}).Distinct().ToList();
            var giamdoc = DaTaContext.CanBoes.Where(p => p.TenCB == DungChung.Bien.GiamDoc).Select(p => p.MaCB).FirstOrDefault();
            lupCanBo.Properties.DataSource = canbo;
            if (bngplx.Count() > 0)
            {
                if (bngplx.First().Status == true)
                {
                    cbx_MaTuy.Enabled = false;
                    cbx_KetLuan.Enabled = false;
                    cbx_DVNongDoCon.Enabled = false;
                    cbx_HangGPLX.Enabled = false;
                    lupCanBo.Enabled = false;
                    txt_NongDoCon.Enabled = false;
                    txt_TinhTrangBenh.Enabled = false;
                    txt_edit_NgaySinh.Enabled = false;
                    txt_edit_noicapcmt.Enabled = false;
                    txt_edit_socmt.Enabled = false;
                    txt_edit_ThangSinh.Enabled = false;
                    dt_edit_ngaycapcmt.Enabled = false;
                    btnGui.Enabled = false;
                    btnLuu.Enabled = false;
                    label8.Enabled = false;
                }
                if (idbnkb.Count() > 0)
                {
                    txt_edit_NgaySinh.Text = idbnkb.First().NgaySinh;
                    txt_edit_ThangSinh.Text = idbnkb.First().ThangSinh;
                    txt_edit_socmt.Text = idbnkb.First().SoKSinh;
                    txt_edit_noicapcmt.Text = idbnkb.First().NoiCapCMT;
                    txt_NongDoCon.Text = Convert.ToString(bngplx.First().NongDoCon);
                    txt_TinhTrangBenh.Text = bngplx.First().TinhTrangBenh;
                    if (idbnkb.First().NgayCapCMT != null)
                    {
                        dt_edit_ngaycapcmt.DateTime = idbnkb.First().NgayCapCMT.Value;
                    }
                }
                #region Hạng bằng lái
                if (bngplx.First().HangBangLai == "A1")
                {
                    cbx_HangGPLX.SelectedIndex = 0;
                }
                else if(bngplx.First().HangBangLai == "A2")
                {
                    cbx_HangGPLX.SelectedIndex = 1;
                }
                else if (bngplx.First().HangBangLai == "A3")
                {
                    cbx_HangGPLX.SelectedIndex = 2;
                }
                else if (bngplx.First().HangBangLai == "A4")
                {
                    cbx_HangGPLX.SelectedIndex = 3;
                }
                else if (bngplx.First().HangBangLai == "B1")
                {
                    cbx_HangGPLX.SelectedIndex = 4;
                }
                else if (bngplx.First().HangBangLai == "B2")
                {
                    cbx_HangGPLX.SelectedIndex = 5;
                }
                else if (bngplx.First().HangBangLai == "C")
                {
                    cbx_HangGPLX.SelectedIndex = 6;
                }
                else if (bngplx.First().HangBangLai == "D")
                {
                    cbx_HangGPLX.SelectedIndex = 7;
                }
                else if (bngplx.First().HangBangLai == "E")
                {
                    cbx_HangGPLX.SelectedIndex = 8;
                }
                else if (bngplx.First().HangBangLai == "F")
                {
                    cbx_HangGPLX.SelectedIndex = 9;
                }
                else if (bngplx.First().HangBangLai == "FC")
                {
                    cbx_HangGPLX.SelectedIndex = 10;
                }
                else if (bngplx.First().HangBangLai == "FD")
                {
                    cbx_HangGPLX.SelectedIndex = 11;
                }
                else if (bngplx.First().HangBangLai == "FE")
                {
                    cbx_HangGPLX.SelectedIndex = 12;
                }
                else
                {
                    cbx_HangGPLX.SelectedIndex = -1;
                }
                #endregion
                #region Ma tuý
                if (bngplx.First().MaTuy == "0")
                {
                    cbx_MaTuy.SelectedIndex = 0;
                }
                else if (bngplx.First().MaTuy == "1")
                {
                    cbx_MaTuy.SelectedIndex = 1;
                }
                else
                {
                    cbx_MaTuy.SelectedIndex = -1;
                }
                #endregion
                #region Nồng độ cồn
                if (bngplx.First().DonViNDC == "0")
                {
                    cbx_DVNongDoCon.SelectedIndex = 0;
                }
                else if (bngplx.First().DonViNDC == "1")
                {
                    cbx_DVNongDoCon.SelectedIndex = 1;
                }
                else
                {
                    cbx_DVNongDoCon.SelectedIndex = -1;
                }
                #endregion
                if (!string.IsNullOrEmpty(bngplx.First().MaCB))
                {
                    lupCanBo.EditValue = bngplx.First().MaCB;
                }
                else
                {
                    lupCanBo.EditValue = giamdoc;
                }
                cbx_KetLuan.Text = bngplx.First().KetLuan;
                txt_SoGiayKsK.Text = bngplx.First().SoGiayKSK;
            }
            else
            {
                lupCanBo.EditValue = giamdoc;
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //thêm thông tin gửi GPLX
        private void button1_Click(object sender, EventArgs e)
        {
            var gplx = DaTaContext.GPLXes.Where(p => p.IDKB == _idkb).ToList();
            if (gplx.Count() <= 0)
            {
                string strSQL = "sp_DanhSoGPLX";
                string[] strpara = new string[] { "@IDKB", "@MaKCB", "@nam" };
                object[] oValue = new object[] { _idkb, DungChung.Bien.MaBV, DateTime.Now.Date };
                SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.Date };

                connect.Connect();
                DataTable dtTble = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
            }
            var idbnkb = DaTaContext.BNKBs.Where(p => p.IDKB == _idkb).ToList();
            if (idbnkb.Count() > 0)
            {
                if (cbx_MaTuy.SelectedIndex == -1 || cbx_DVNongDoCon.SelectedIndex == -1 || cbx_KetLuan.SelectedIndex == -1 || cbx_HangGPLX.SelectedIndex == -1 || txt_NongDoCon.Text == "")
                {
                    if (cbx_MaTuy.SelectedIndex == -1)
                    {
                        MessageBox.Show("Chưa nhập test ma túy!!");
                    }
                    if (cbx_DVNongDoCon.SelectedIndex == -1)
                    {
                        MessageBox.Show("Chưa nhập đơn vị nồng độ cồn!!");
                    }
                    if (cbx_KetLuan.SelectedIndex == -1)
                    {
                        MessageBox.Show("Chưa nhập kết luận!!");
                    }
                    if (cbx_HangGPLX.SelectedIndex == -1)
                    {
                        MessageBox.Show("Chưa nhập hạng bằng lái!!");
                    }
                    if (txt_NongDoCon.Text == "")
                    {
                        MessageBox.Show("Chưa nhập nồng độ cồn!!");
                    }
                    return;
                }
                GPLX gplxs = DaTaContext.GPLXes.Single(p => p.IDKB == _idkb);
                if (cbx_MaTuy.SelectedIndex == 0)
                {
                    gplxs.MaTuy = "0";
                }
                else if (cbx_MaTuy.SelectedIndex == 1)
                {
                    gplxs.MaTuy = "1";
                }
                else
                {
                    gplxs.MaTuy = "";
                }
                if (cbx_DVNongDoCon.SelectedIndex == 0)
                {
                    gplxs.DonViNDC = "0";
                }
                else if (cbx_DVNongDoCon.SelectedIndex == 1)
                {
                    gplxs.DonViNDC = "1";
                }
                //gplxs.Status = true;
                gplxs.KetLuan = cbx_KetLuan.Text;
                string mcb = Convert.ToString(lupCanBo.EditValue);
                if (!string.IsNullOrEmpty(mcb))
                {
                    gplxs.MaCB = mcb;
                }
                else
                {
                    MessageBox.Show("Chưa chọn cán bộ");
                    return;
                }
                gplxs.HangBangLai = cbx_HangGPLX.Text;
                gplxs.NongDoCon = txt_NongDoCon.Text;
                gplxs.TinhTrangBenh = txt_TinhTrangBenh.Text;
                DaTaContext.SaveChanges();
            }
            if (DaTaContext.SaveChanges() >= 0)
            {
                MessageBox.Show("Thêm thông tin thành công");
                frm_NhapThongTinGPLX_Load(null, null);
            }
            else
            {
                MessageBox.Show("Thêm thông tin Thất bại");
                return;
            }
        }

        //thêm thông tin còn thiếu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            txt_edit_NgaySinh.Enabled = false;
            txt_edit_ThangSinh.Enabled = false;
            txt_edit_socmt.Enabled = false;
            txt_edit_noicapcmt.Enabled = false;
            dt_edit_ngaycapcmt.Enabled = false;
            int ngaysinh = 0;
            int thangsinh = 0;
            if (!string.IsNullOrEmpty(txt_edit_NgaySinh.Text) && !string.IsNullOrEmpty(txt_edit_ThangSinh.Text))
            {
                 ngaysinh = Convert.ToInt32(txt_edit_NgaySinh.Text);
                 thangsinh = Convert.ToInt32(txt_edit_ThangSinh.Text);
            }
            else
            {
                MessageBox.Show("Chưa nhập ngày sinh, tháng sinh");
                return;
            }
            BenhNhan bn = DaTaContext.BenhNhans.Single(p => p.MaBNhan == _mbn);
            TTboXung ttbx = DaTaContext.TTboXungs.Single(p => p.MaBNhan == _mbn);
            bn.NgaySinh = ngaysinh.ToString("D2");
            bn.ThangSinh = thangsinh.ToString("D2");
            ttbx.SoKSinh = txt_edit_socmt.Text;
            ttbx.NgayCapCMT = dt_edit_ngaycapcmt.DateTime;
            ttbx.NoiCapCMT = txt_edit_noicapcmt.Text;
            DaTaContext.SaveChanges();

            if (DaTaContext.SaveChanges() >= 0)
            {
                MessageBox.Show("Thêm thông tin thành công");
                frm_NhapThongTinGPLX_Load(null, null);
            }
            else
                MessageBox.Show("Thêm thông tin thất bại");
        }

        private void lb_ThuLai_Click(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(518, 191);
            txt_edit_NgaySinh.Enabled = false;
            txt_edit_ThangSinh.Enabled = false;
            txt_edit_socmt.Enabled = false;
            txt_edit_noicapcmt.Enabled = false;
            dt_edit_ngaycapcmt.Enabled = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(518, 290);

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txt_edit_NgaySinh.Enabled = true;
            txt_edit_ThangSinh.Enabled = true;
            txt_edit_socmt.Enabled = true;
            txt_edit_noicapcmt.Enabled = true;
            dt_edit_ngaycapcmt.Enabled = true;
        }

        private void lupCanBo_EditValueChanged(object sender, EventArgs e)
        {
        }
    }
}
