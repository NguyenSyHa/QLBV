using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QLBV.BaoCao;
using DevExpress.XtraPrinting.Native;
using QLBV.Utilities.Commons;
using QLBV.Signature.Models;
using QLBV.Utilities.Helppers;

namespace QLBV
{
    public partial class frmIn : DevExpress.XtraEditors.XtraForm
    {
        public XtraReport _xtraReport;
        private SaveFileDialog save;

        public frmIn()
        {
            InitializeComponent();
        }

        public frmIn(XtraReport xtraReport) : this()
        {
            _xtraReport = xtraReport;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objArr">Mảng 2 chiều truyền vào Object[,] </param>
        /// <param name="arrColNameFomat">Mảng 2 chiều tên cột và kiểu fomat của cột (String [,] arrcolName = = new String[] {{"Tên cột 1","Tên cột 2",...},{"Fomat cột 1", "Fomat cột 2",...}})</param>
        /// <param name="arrWith">Mảng int xác định độ rộng của các cột theo thứ tự  int[] arrWidth = new int[] {10,40,...} (hoặc checkAutoWith = true ==> int[] arrWidth = new int[]{})</param>
        /// <param name="sheetName">Tên Sheet trong file Excel xuất ra</param>
        /// <param name="filePath">Đường dẫn xuất file</param>
        /// <param name="checkAutofomat">Auto Fomat độ rộng cột hoặc không</param>
        public frmIn(Object[,] objArr, string[] arrColNameFomat, int[] arrWith, string sheetName, string filePath, bool checkAutofomat, string name)
        {
            InitializeComponent();

            _objArr2 = objArr;
            _arrColNameFomat = arrColNameFomat;
            _arrWith = arrWith;
            _filePath = filePath;
            _sheetName = sheetName;
            _checkAutofomat = checkAutofomat;
            this._name = name;
        }
        #region các biến dùng cho xuất Excel
        private Object[,] _objArr2;// mảng 2 chiều
        private string[] _arrColNameFomat;// tên cột và fomat
        private int[] _arrWith;// độ rộng cột
        private string _sheetName;// tên sheet
        private string _filePath;// tên đường dẫn
        private bool _checkAutofomat;// fomat độ rộng cột theo AutoFomat
        private string _name;
        #endregion các biến dùng cho xuất Excel

        private void frmIn_Load(object sender, EventArgs e)
        {
            bool _in = true;
            _in = (DungChung.Ham.checkQuyen(this.Name)[4]);

            printPreview_In.Enabled = _in;
            printPreview_InChon.Enabled = _in;
            printPreview_Xuat.Enabled = _in;
            if (prcIN.PrintingSystem != null)
            {
                prcIN.PrintingSystem.ShowMarginsWarning = false;

                // Hide Parameters Panel before preview
                prcIN.PrintingSystem.ExecCommand(PrintingSystemCommand.Parameters, new object[] { false });
            }
        }

        private void frmIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
                MemoryManagement.FlushMemory();
            }
            if (e.KeyCode == Keys.F5)
            {

            }
            if (e.KeyCode == Keys.F6)
                this.printPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find;
        }

        private void frmIn_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void bar_XuatExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (QLBV_Library.QLBV_Ham.xuatExcelArr(_objArr2, _arrColNameFomat, _arrWith, _sheetName, _filePath, _checkAutofomat))
                MessageBox.Show("Xuất thành công");
            else
                MessageBox.Show("Lỗi!");
        }

        private void printPreviewBarItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //for (int i = Application.OpenForms.Count - 1; i >= 1; i--)
            //{
            //    if (Application.OpenForms[i].Name != "Menu")
            //    {
                    //if (Application.OpenForms[i].Name == "frm_Check_moi" || Application.OpenForms[i].Name == "frmPhieulinh")
                    //{
                    //Application.OpenForms[i].Dispose();
                    //MemoryManagement.FlushMemory();
                    //}
                //}

                //Application.OpenForms["frm_Check_moi"];
                //Application.OpenForms["frmPhieulinh"].Close();
            //}


        }

        private void btnExportToDoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportToDOC("doc", DocumentFormat.Doc);
        }

        private void btnExportToDocX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ExportToDOC(string extension, DocumentFormat df)
        {
            if (_xtraReport == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Environment.CurrentDirectory + "\\" + _xtraReport.ExportOptions.PrintPreview.DefaultFileName + "." + extension;
            sfd.Filter = String.Format("{0} File|*.{0}", extension);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (RichEditDocumentServer docServer = new RichEditDocumentServer())
                using (MemoryStream ms = new MemoryStream())
                {
                    _xtraReport.ExportToRtf(ms, new RtfExportOptions() { ExportMode = RtfExportMode.SingleFile });

                    ms.Position = 0;
                    docServer.LoadDocument(ms, DocumentFormat.Rtf);
                    docServer.SaveDocument(sfd.FileName, df);
                }
                if (MessageBox.Show("Bạn muốn mở file vừa xuất?", extension + " export", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(sfd.FileName);
                }
            }
        }

        private void BtnSign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string savePath = AppDomain.CurrentDomain.BaseDirectory + @"Documents";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var chuky = _data.CanBoes.FirstOrDefault(p => p.MaCB == DungChung.Bien.MaCB);

            Image imgSign = null;

            if (chuky != null
                && chuky.ChuKySo != null)
                imgSign = Image.FromStream(new MemoryStream(chuky.ChuKySo));

            //Cho chọn đường dẫn lưu
            save = new SaveFileDialog()
            {
                Filter = "|*.pdf",
                RestoreDirectory = true,
                InitialDirectory = savePath,
                FileName = _xtraReport.Name
            };

            if (_xtraReport != null && chuky != null
                && save.ShowDialog() == DialogResult.OK)
            {
                this.prcIN.PrintingSystem = _xtraReport.PrintingSystem;
                _xtraReport.ExportToPdf(save.FileName);

                // Sau thông tin này thay bằng tài khaonr đăng nhập
                var login = new LoginModel()
                {
                    Username = chuky.Email,
                    //Username = @"duynguyen.19x@gmail.com",
                    Password = Security.Decrypt(chuky.MKChuKySo),
                    PhoneNumber = chuky.SoDT,
                    FileName = save.FileName,
                    Id = Guid.NewGuid().ToString(),
                    FirstName = chuky.TenCB,
                    LastName = "Hoàng Văn",
                    Image = AddSignatureImageToFile(_xtraReport, chuky.ChuKySo, save.FileName),
                };
                if (chuky.Email == null)
                {
                    MessageBox.Show("Không tìm thấy Email chữ ký số !");
                    return;
                }
                if (chuky.MKChuKySo == null)
                {
                    MessageBox.Show("Không tìm thấy MK chữ ký số !");
                    return;
                }
                Task.Run(async () => await Signature.Signature.SignMisa(login)).Wait();
            }
        }

        private ImageModel AddSignatureImageToFile(XtraReport report, byte[] img, string fileName)
        {
            ImageModel image = new ImageModel();

            if (report is repDonThuoc_TT18)
            {
                foreach (Page page in report.Pages)
                {
                    NestedBrickIterator iterator = new NestedBrickIterator(page.InnerBricks);
                    while (iterator.MoveNext())
                    {
                        VisualBrick brick = iterator.CurrentBrick as VisualBrick;
                        if (brick != null
                            && "Signature".Equals(brick.Value)
                            && brick.BrickOwner is XRPictureBox pictureBox)
                        {
                            //GraphicsUnit.Document

                            //float documentToInch = (1f / 300f);
                            //float inchToPoint = 72f;
                            //float pointToPixel = (1f / 0.75f);

                            //image.PositionX = brick.Location.X;
                            //image.PositionY = brick.Location.Y;
                            //image.Height = pictureBox.HeightF;
                            //image.Width = pictureBox.WidthF;

                            //image.PositionX = 393;
                            //image.PositionY = 279;
                            //image.Height = 60;
                            //image.Width = 200;
                        }
                    }
                }

                image.SignatureImage = img != null ? Convert.ToBase64String(img) : null;
            }

            return image;
        }

        private void frmIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void frmIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.Close();
            MemoryManagement.FlushMemory();
        }
    }
}