using System;
using System.Windows.Forms;
using DevExpress.Skins;
using QLBV.DungChung;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading;
using QLBV.MoRong.SystemForm;
using QLBV.Utilities.Commons;
using QLBV.AutoMappers;
using AutoMapper;

namespace QLBV
{
    static class Program
    {
        public static ConnectData _connect;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _connect = new ConnectData();
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            //_connect.Connect();
            //if (_connect.isConnect)
            //{
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            log4net.Config.XmlConfigurator.Configure();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.BonusSkins).Assembly);
            Application.SetCompatibleTextRenderingDefault(false);

            AppConfig.MyMapper = new MapperConfiguration(configuration => 
            {
                ConfigMappings.CreateMappings(configuration);
            }).CreateMapper();

            Application.Run(new frmDangNhap());
            //Application.Run(new FrmTest());
        }
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            frmShowException frm = new frmShowException(t.Exception);
            frm.ShowDialog();
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            frmShowException frm = new frmShowException(ex);
            frm.ShowDialog();
        }
    }
}
