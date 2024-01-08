using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using QLBV.DungChung;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using QLBV.Class;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

namespace QLBV.ChucNang
{
    public partial class frm_GuiHSSK : Form
    {
        public frm_GuiHSSK()
        {
            InitializeComponent();
            //GridLocalizer.Active = new MyGridLocalizer();

        }

        List<BenhNhanADO> listSelecteds;
        List<BenhNhanADO> listAll;

        private void frm_GuiHSSK_Load(object sender, EventArgs e)
        {
            //if (DungChung.Bien.MaBV != "24009")
            //{
            //    colXXemHSBN.Visible = false;
            //    gridColumn20.Visible = false;
            //}
           
            listSelecteds = new List<BenhNhanADO>();
            txtSearch.ResetText();
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            LoadDataToForm();
            gridColumn12.Image = imageList1.Images[0];
            cbbDoiTuong.Text = cbbDoiTuong.Items[0].ToString();
        }

        private void LoadDataToForm()
        {
            var tuNgay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            var denNgay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            var index = radioGroupDoiTuong.SelectedIndex;
            var dTuong = cbbDoiTuong.SelectedIndex;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            listAll = (from bn in dataContext.BenhNhans.Where(o => o.MaKCB == DungChung.Bien.MaBV && (dTuong == 0 ? true : (dTuong == 1 ? o.DTuong == "BHYT" : (dTuong == 2 ? o.DTuong == "Dịch vụ" : o.DTuong == "KSK"))))
                        .Where(o => (index == 0 ? true : (index == 1 ? o.NoiTru == 1 : (index == 2 ? (o.NoiTru != 1 && o.DTNT == false) : (o.DTNT == true && o.NoiTru != 1)))))
                       join vp in dataContext.VienPhis.Where(o => o.NgayTT >= tuNgay && o.NgayTT <= denNgay) on bn.MaBNhan equals vp.MaBNhan
                       join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                       join ttbx in dataContext.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                       select new BenhNhanADO { MaBNhan = bn.MaBNhan, DChi = bn.DChi, TenBNhan = bn.TenBNhan, DTuong = bn.DTuong, Tuoi = bn.Tuoi, Check = false, GTinh = bn.GTinh, NgaySinh = bn.NgaySinh, ThangSinh = bn.ThangSinh, NamSinh = bn.NamSinh, MaGD_HSSK = rv.maGiaoDichHSSK, Error = ttbx.MoTa_HSSK }).ToList();
            listAll.ForEach(x => { if (listSelecteds.Exists(o => o.MaBNhan == x.MaBNhan)) x.Check = true; x.Is_Send = !string.IsNullOrWhiteSpace(x.MaGD_HSSK); });
            gridControlSearch.BeginUpdate();
            gridControlSearch.DataSource = listAll;
            gridControlSearch.EndUpdate();
        }


        #region Service
        private KQLogin getToken(LoginModel request)
        {
            var httpClient = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri("https://api-hssk.kcb.vn/api/v1/resource/authentication/login");

            // Tạo StringContent
            //string jsoncontent = "{\"username\": \"lienthong_vietyen\", \"password\": \"Bvvy@2468\"}";
            string jsoncontent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            httpRequestMessage.Content = httpContent;

            var response = httpClient.SendAsync(httpRequestMessage);
            var responseContent = response.Result.Content.ReadAsStringAsync();


            if (response.Result.IsSuccessStatusCode)
            {
                KQLogin plv = response.Result.Content.ReadAsAsync<KQLogin>().Result;
                return plv;
            }
            return new KQLogin();
        }

        private bool checkInfo(HSSK_PID request) // ham kiem tra thong tin benh nhan
        {
            if (string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.HO_TEN) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_GIOI_TINH) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.NGAY_SINH) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_TINH_NOI_SINH) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_HUYEN_NOI_SINH) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_XA_NOI_SINH) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_TINH_HIEN_TAI) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_HUYEN_HIEN_TAI) || string.IsNullOrEmpty(request.THONG_TIN_BENH_NHAN.MA_XA_HIEN_TAI))
            {
                return false;
            }
            return true;

        }

        private KQ_PID GetPID(string token, HSSK_PID bodyRequest)
        {
            var client = new RestClient("https://tructichhop-api.kcb.vn/hssk/dinhdanh");
            client.Timeout = 10000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            string json = JsonConvert.SerializeObject(bodyRequest);
            request.AddJsonBody(bodyRequest);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            string str3 = content;
            KQ_PID dinh = JsonConvert.DeserializeObject<KQ_PID>(str3);
            return dinh;
        }

        private HSSK_PID getTTBN(BenhNhan benhNhan, TTboXung ttBoSung)
        {
            HSSK_PID TTBN = new HSSK_PID();
            string gtthetu = "";
            string gttheden = "";
            string han5 = "";

            if (benhNhan.HanBHTu != null)
                gtthetu = DungChung.Ham.ngayBHYT(benhNhan.HanBHTu ?? DateTime.Now);
            if (benhNhan.HanBHTu != null)
                gttheden = DungChung.Ham.ngayBHYT(benhNhan.HanBHDen ?? DateTime.Now);
            if (benhNhan.NgayHM != null)
                han5 = DungChung.Ham.ngayBHYT(benhNhan.NgayHM ?? DateTime.Now);

            var TTBenhNhan = new thongtinBN()
            {
                HO_TEN = benhNhan.TenBNhan,
                MA_GIOI_TINH = benhNhan.GTinh == 1 ? "1" : "2",
                NGAY_SINH = benhNhan.NamSinh + (benhNhan.ThangSinh.Length == 1 ? ("0" + benhNhan.ThangSinh.Trim()) : benhNhan.ThangSinh.Trim()) + (benhNhan.NgaySinh.Trim().Length == 1 ? ("0" + benhNhan.NgaySinh.Trim()) : benhNhan.NgaySinh.Trim()),
                MA_TINH_NOI_SINH = ttBoSung.MaTinhKhaiSinh == null ? "" : ttBoSung.MaTinhKhaiSinh,
                MA_HUYEN_NOI_SINH = ttBoSung.MaHuyenKhaiSinh == null ? "" : ttBoSung.MaHuyenKhaiSinh,
                MA_XA_NOI_SINH = ttBoSung.MaXaKhaiSinh == null ? "" : ttBoSung.MaXaKhaiSinh,
                SO_CMND = ttBoSung.CMT == null ? "" : ttBoSung.CMT,
                DIEN_THOAI_DD = ttBoSung.DThoai == null ? "" : ttBoSung.DThoai,
                EMAIL = "",
                DIA_CHI_THUONG_TRU = "", // ai thaays thi bo sung vao
                MA_TINH_THUONG_TRU = "",
                MA_HUYEN_THUONG_TRU = "",
                MA_XA_THUONG_TRU = "",
                DIA_CHI_HIEN_TAI = benhNhan.DChi,
                MA_TINH_HIEN_TAI = ttBoSung.MaTinh == null ? "" : ttBoSung.MaTinh,
                MA_HUYEN_HIEN_TAI = ttBoSung.MaHuyen == null ? "" : ttBoSung.MaHuyen,
                MA_XA_HIEN_TAI = ttBoSung.MaXa == null ? "" : ttBoSung.MaXa,
                MA_NGHE_NGHIEP = "",
                NOI_LAM_VIEC = ttBoSung.NoiLV == null ? "" : ttBoSung.NoiLV,
                MA_DAN_TOC = ttBoSung.MaDT == null ? "" : ttBoSung.MaDT,
                MA_QUOC_TICH = "",
                MA_TON_GIAO = "",
                MA_TRINH_DO_HOCVAN = "",
                MA_QUANHE_NGUOI_BAO_HO = "",
                HO_TEN_NGUOI_BAO_HO = ttBoSung.NThan == null ? "" : ttBoSung.NThan,
                DIEN_THOAI_NGUOI_BAO_HO = ttBoSung.DThoaiNT == null ? "" : ttBoSung.DThoaiNT,
                SO_CMND_NGUOI_BAO_HO = "",
            };



            var thongtinBHYT = new thongtintheBHYT()
            {
                MA_THE = benhNhan.SThe == null ? "" : benhNhan.SThe,
                MA_DKBD = benhNhan.MaCS,
                MA_KHU_VUC = "",
                GT_THE_TU = gtthetu,
                GT_THE_DEN = gttheden,
                DIA_CHI_BHYT = "",
                GT_5NAM_LIENTUC = han5,
                MIEN_CUNG_CT = "",
            };


            TTBN.THONG_TIN_BENH_NHAN = TTBenhNhan;
            TTBN.THONG_TIN_THE_BHYT = thongtinBHYT;
            return TTBN;
        }

        private uploadResult uploadHSSK(string token, string filePath)
        {
            var client = new RestClient("https://tructichhop-api.kcb.vn/hssk/4210");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddFile("file", filePath);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                uploadResult kq = JsonConvert.DeserializeObject<uploadResult>(content);
                return kq;
            }
            return null;
        }


        #endregion

        private void btnSend_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[36]) || string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[37]))
            {
                MessageBox.Show("Bạn chưa nhập username hoặc password vào file thiết lập hệ thống !!!");
                return;
            }

            if (string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[34]) || string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[35]))
            {
                MessageBox.Show("Bạn chưa thiết lập đủ filePath !!!");
                return;
            }

            #region Duc viet lai
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            LienThongHSSK hssk = new LienThongHSSK();
            var dataSource = (List<BenhNhanADO>)gridControlChoose.DataSource;
            if (dataSource != null && dataSource.Count > 0)
            {
                //tt benh nhan ok thi dang nhap de lay token

                bool sttLogin = false;

                LoginModel loginModel = new LoginModel();
                loginModel.username = DungChung.Bien.xmlFilePath_LIS[36];
                loginModel.password = DungChung.Bien.xmlFilePath_LIS[37];

                KQLogin login = new KQLogin();
                login = getToken(loginModel);
                if (login.Code == "200")
                    sttLogin = true;

                if (!sttLogin)
                {
                    MessageBox.Show("Đăng nhập thất bại");
                    return;
                }


                List<BenhNhanADO> listMaBNhan = new List<BenhNhanADO>();
                List<string> listErrors = new List<string>();
                int CountSuccess = 0;
                int CountError = 0;
                string PersonID = "";

                //gửi từng bệnh nhân
                foreach (var item in dataSource)
                {
                    var benhNhan = dataContext.BenhNhans.Where(p => p.MaBNhan == item.MaBNhan).FirstOrDefault();
                    var ttBoSung = dataContext.TTboXungs.Where(p => p.MaBNhan == item.MaBNhan).FirstOrDefault();
                    var rv = dataContext.RaViens.Where(p => p.MaBNhan == item.MaBNhan).FirstOrDefault();
                    string PID = "";
                    HSSK_PID request = getTTBN(benhNhan, ttBoSung);
                    //kiem tra thong tin benh nhan
                    bool check = checkInfo(request);

                    if (check)
                    {
                        // lấy personid
                        if (!string.IsNullOrEmpty(ttBoSung.PID_HSSK))
                        {
                            PID = ttBoSung.PID_HSSK;
                        }
                        else
                        {
                            KQ_PID pid = new KQ_PID();
                            pid = GetPID(login.Data.Access_token, request);
                            if (!string.IsNullOrEmpty(pid.pId))
                            {
                                PID = pid.pId;
                                ttBoSung.PID_HSSK = PID;
                                dataContext.SaveChanges();
                            }
                        }

                        if (!string.IsNullOrEmpty(PID))
                        {
                            string guiFile = "";
                            DungChung.cls_KetNoi_BHXH clsBHXH = new DungChung.cls_KetNoi_BHXH();
                            guiFile = clsBHXH.XuatXML_4210_HSSK(dataContext, item.MaBNhan, DungChung.Bien.xmlFilePath_LIS[35], DungChung.Bien.xmlFilePath_LIS[34], benhNhan, rv, ttBoSung, PID);
                            if (!string.IsNullOrEmpty(guiFile))
                            {
                                uploadResult upload = new uploadResult();
                                upload = uploadHSSK(login.Data.Access_token, guiFile);

                                if (upload != null)
                                {
                                    rv.maGiaoDichHSSK = upload.maGiaoDich;
                                    if (dataContext.SaveChanges() > 0)
                                        CountSuccess++;
                                }
                                else
                                {
                                    ttBoSung.Status_HSSK = "0";
                                    ttBoSung.MoTa_HSSK = "Lỗi khi đẩy dữ liệu 4210";
                                    if (dataContext.SaveChanges() > 0)
                                        CountError++;
                                }
                            }
                        }
                        else
                        {
                            ttBoSung.Status_HSSK = "0";
                            ttBoSung.MoTa_HSSK = "Không tìm thấy PID";
                            if (dataContext.SaveChanges() > 0)
                                CountError++;
                        }
                    }
                    else
                    {
                        ttBoSung.Status_HSSK = "0";
                        ttBoSung.MoTa_HSSK = "Thông tin không đủ để lấy PID";
                        dataContext.SaveChanges();
                        CountError++;

                    }

                }
                //hssk.GetHoSoKhamChuaBenh
                gridControlSend.BeginUpdate();
                gridControlSend.DataSource = listMaBNhan;
                gridControlSend.EndUpdate();
                XtraMessageBox.Show(string.Format("Gửi thành công {0} bệnh nhân." + Environment.NewLine + "Gửi thất bại {1} bệnh nhân.", CountSuccess, CountError++));
                listSelecteds = listSelecteds.Where(o => !listMaBNhan.Select(p => p.MaBNhan).Contains(o.MaBNhan)).ToList();
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
                LoadDataToForm();
                txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
            #endregion


        }

        private void gridViewChoose_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetRow(gridViewChoose.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void gridViewSearch_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(gridViewSearch.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void gridViewSend_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSend.GetRow(gridViewSend.GetRowHandle(e.ListSourceRowIndex));
            if (row != null)
            {
                if (e.IsGetData && e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Object)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row.GTinh == 1 ? "Nam" : "Nữ";
                    else if (e.Column.FieldName == "Tuoi_Str")
                        e.Value = DungChung.Ham.CalculateAgeByDate(row.NgaySinh, row.ThangSinh, row.NamSinh);
                }
            }
        }

        private void repositoryItemButtonEdit_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetFocusedRow();
            if (row != null)
            {
                listSelecteds.Remove(row);
                if (listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan) != null)
                {
                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = false;
                }
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
                txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
        }
        private void repositoryItemButtonEdit_ViewBN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetFocusedRow();
            if (row != null)
            {
                FormNhap.frmHSBNNhapMoi frm = new FormNhap.frmHSBNNhapMoi(2, row.MaBNhan, 1);
                frm.ShowDialog();
            }
        }


        public class MyGridLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                switch (id)
                {
                    case GridStringId.FindControlFindButton:
                        return "Tìm kiếm";
                    case GridStringId.FindControlClearButton:
                        return "Xóa";
                    case GridStringId.FilterPanelCustomizeButton:
                        return "Lọc";
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }

        private void gridViewSearch_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {

        }

        public class BenhNhanADO : BenhNhan
        {
            public bool Check { get; set; }
            public string Error { get; set; }
            public bool Is_Send { get; set; }
            public string MaGD_HSSK { get; set; }
            public BenhNhanADO() { }
            public BenhNhanADO(BenhNhan data)
            {
                LibraryStore.Mapper.DataObjectMapper.Map<BenhNhanADO>(this, data);
            }
        }

        private void gridViewSearch_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(e.RowHandle);
            if (row != null && e.Column.FieldName == "Check")
            {
                ChooseRow(e, row);
                gridControlChoose.BeginUpdate();
                gridControlChoose.DataSource = listSelecteds;
                gridControlChoose.EndUpdate();
            }

        }

        private void ChooseRow(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e, BenhNhanADO row)
        {
            if (listSelecteds.Exists(o => o.MaBNhan == row.MaBNhan))
            {
                if ((bool)e.Value)
                {
                    row.Check = (bool)e.Value;
                    listSelecteds.Add(row);
                }
                else
                {
                    listSelecteds = listSelecteds.Where(o => o.MaBNhan != row.MaBNhan).ToList();
                }
            }
            else
            {
                row.Check = (bool)e.Value;
                listSelecteds.Add(row);
            }
            if (listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan) != null)
                listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = (bool)e.Value;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Condition();
            }
        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void GuiTest()
        {
            //lấy danh sách gửi test
            int i = 0;
            foreach (var item in listAll)
            {
                if ((item.PID == null || item.PID == "") && item.Tuoi > 0 && i < 500)
                {
                    listSelecteds.Add(item);
                }
                i++;
            }
            gridControlChoose.BeginUpdate();
            gridControlChoose.DataSource = listSelecteds;
            gridControlChoose.EndUpdate();
        }

        private void gridViewChoose_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Error")
                {
                    e.RepositoryItem = string.IsNullOrWhiteSpace(row.Error) ? repositoryItemButtonEdit_Error_Disable : repositoryItemButtonEdit_Error_Enable;
                }
            }
        }

        private void repositoryItemButtonEdit_Error_Enable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = (BenhNhanADO)gridViewChoose.GetFocusedRow();
            if (row != null)
            {
                frm_DS_HSSK_Error frm = new frm_DS_HSSK_Error(row);
                frm.ShowDialog();
            }
        }

        bool IsCheckAll = false;
        private void gridViewSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (hi.HitTest == GridHitTest.Column)
                {
                    if (hi.Column.FieldName == "Check" || hi.Column.FieldName == "View")
                    {
                        gridControlSearch.BeginUpdate();
                        gridControlChoose.BeginUpdate();
                        if (IsCheckAll)
                        {
                            hi.Column.Image = imageList1.Images[0];
                            var dataSource = (List<BenhNhanADO>)gridControlSearch.DataSource;
                            if (dataSource != null)
                            {
                                foreach (BenhNhanADO row in dataSource)
                                {
                                    if (row.Is_Send) break;
                                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = false;
                                    var checkChoose = listSelecteds.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                                    if (checkChoose != null)
                                    {
                                        listSelecteds = listSelecteds.Where(o => o.MaBNhan != row.MaBNhan).ToList();
                                    }
                                }
                                gridControlSearch.DataSource = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
                                IsCheckAll = false;
                            }
                        }
                        else
                        {
                            hi.Column.Image = imageList1.Images[1];
                            var dataSource = (List<BenhNhanADO>)gridControlSearch.DataSource;
                            if (dataSource != null)
                            {
                                foreach (BenhNhanADO row in dataSource)
                                {
                                    if (row.Is_Send) break;
                                    listAll.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Check = true;
                                    var checkChoose = listSelecteds.FirstOrDefault(o => o.MaBNhan == row.MaBNhan);
                                    if (checkChoose == null)
                                    {
                                        listSelecteds.Add(row);
                                    }
                                }
                                gridControlSearch.DataSource = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
                                IsCheckAll = true;
                            }
                        }
                        gridControlChoose.DataSource = listSelecteds;
                        gridControlSearch.EndUpdate();
                        gridControlChoose.EndUpdate();
                    }
                }
            }
        }

        private void gridControlSearch_Click(object sender, EventArgs e)
        {

        }

        private void chkDaGui_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDaGui.Checked)
                listAll.ForEach(o => { o.Check = false; });
            listSelecteds = listSelecteds.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
            gridControlChoose.BeginUpdate();
            gridControlChoose.DataSource = listSelecteds;
            gridControlChoose.EndUpdate();
            Search_Condition();
            btnSend.Enabled = !chkDaGui.Checked;
            btnCancel.Enabled = chkDaGui.Checked;
        }

        private void Search_Condition()
        {
            List<BenhNhanADO> search = new List<BenhNhanADO>();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                search = listAll.Where(o => (o.MaBNhan.ToString() == txtSearch.Text || o.TenBNhan.ToLower().Contains(txtSearch.Text.ToLower()) || o.DChi.ToLower().Contains(txtSearch.Text.ToLower())) && (o.Is_Send == chkDaGui.Checked)).ToList();
            }
            else
                search = listAll.Where(o => o.Is_Send == chkDaGui.Checked).ToList();
            gridControlSearch.BeginUpdate();
            gridControlSearch.DataSource = search;
            gridControlSearch.EndUpdate();
        }

        private void gridViewSearch_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (BenhNhanADO)gridViewSearch.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Check")
                {
                    e.RepositoryItem = repositoryItemCheckEdit_Check;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var dataSource = (List<BenhNhanADO>)gridControlChoose.DataSource;
            if (dataSource != null && dataSource.Count > 0)
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                foreach (var item in dataSource)
                {
                    var raVien = dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == item.MaBNhan);
                    if (raVien != null)
                    {
                        raVien.maGiaoDichHSSK = "";
                    }
                    var benhNhan = dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == item.MaBNhan);
                    if (benhNhan != null)
                    {
                        benhNhan.PID = "";
                    }
                }
                if (dataContext.SaveChanges() > 0)
                {
                    MessageBox.Show("Hủy thành công!");
                    listSelecteds = new List<BenhNhanADO>();
                    gridControlChoose.BeginUpdate();
                    gridControlChoose.DataSource = listSelecteds;
                    gridControlChoose.EndUpdate();
                    LoadDataToForm();
                    txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }

        }

        private void radioGroupDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void gridViewSearch_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            int count = 0;
            if (gridControlSearch.DataSource != null)
            {
                count = ((List<BenhNhanADO>)gridControlSearch.DataSource).Count;
            }
            e.Appearance.DrawString(e.Cache, string.Format("Tổng: {0}", count), e.Bounds);
            e.Handled = true;
        }

        private void gridViewChoose_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            int count = 0;
            if (gridControlChoose.DataSource != null)
            {
                count = ((List<BenhNhanADO>)gridControlChoose.DataSource).Count;
            }
            e.Appearance.DrawString(e.Cache, string.Format("Tổng: {0}", count), e.Bounds);
            e.Handled = true;
        }

        private void gridViewSend_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            int count = 0;
            if (gridControlSend.DataSource != null)
            {
                count = ((List<BenhNhanADO>)gridControlSend.DataSource).Count;
            }
            e.Appearance.DrawString(e.Cache, string.Format("Tổng: {0}", count), e.Bounds);
            e.Handled = true;
        }

        private void gridViewSearch_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (gridViewSearch.GetRowCellValue(e.RowHandle, colLoi) != null && gridViewSearch.GetRowCellValue(e.RowHandle, colLoi).ToString() != "")
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void cbbDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToForm();
            txtSearch_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
    }
}
