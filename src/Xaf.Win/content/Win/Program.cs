using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Validation.Win;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;

namespace Scissors.Win
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run(args);
        }

        void Run(string[] args)
        {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            WindowsFormsSettings.LoadApplicationSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;

            InitializeTracing();
            InitGlobalStatics();

            using (var winApplication = CreateApplication())
            {
                try
                {
                    winApplication.Setup();
                    winApplication.Start();
                }
                catch (Exception e)
                {
                    winApplication.HandleException(e);
                }
            }
        }


        protected virtual void InitializeTracing()
        {
            if (Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder)
            {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
        }

        protected virtual void InitGlobalStatics()
        {
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
            DevExpress.ExpressApp.Utils.ImageLoader.Instance.UseSvgImages = true;
        }

        protected virtual WinApplication CreateApplication()
        {
            var app = new Empty191XafAppWindowsFormsApplication();
            using (app.Initialize())
            {
                app.ApplicationName = "Empty191XafApp",
                app.LinkNewObjectToParentImmediately = false,
                app.OptimizedControllersCreation = true,
                app.UseLightStyle = true,
			    app.ExecuteStartupLogicBeforeClosingLogonWindow = true,
                app.CheckCompatibilityType
                    = System.Diagnostics.Debugger.IsAttached
                    ? DatabaseUpdateMode.UpdateDatabaseAlways
                    : DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema,
                app.UseOldTemplates = false,
                app.Modules.AddRange(new[]
                {
                    new SystemModule(),
                    new SystemWindowsFormsModule(),
                    new Empty191XafAppModule(),
                    new Empty191XafAppWindowsFormsModule(),
                });
            }

            return app;
        }

        protected virtual string GetConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null)
            {
                return ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            InMemoryDataStoreProvider.Register();
            return InMemoryDataStoreProvider.ConnectionString;
        }
    }
}