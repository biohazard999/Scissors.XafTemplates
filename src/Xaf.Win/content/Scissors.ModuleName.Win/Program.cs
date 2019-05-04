using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using Scissors.ModuleName.Win.Extentions;
using Scissors.ModuleName.App;
using Scissors.ModuleName.App.Win;

namespace Scissors.ModuleName.Win
{
    class Program
    {
        [STAThread]
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

            DevExpress.ExpressApp.Model.ModelXmlReader.UseXmlReader = true;
            DevExpress.ExpressApp.Model.Core.ModelNode.UseDefaultValuesCache = true;
            DevExpress.ExpressApp.Core.ControllersManager.UseParallelBatchControllerCreation = true;
            DevExpress.ExpressApp.ApplicationModulesManager.UseParallelTypesCollector = true;
            DevExpress.ExpressApp.ApplicationModulesManager.UseStaticCache = true;
            DevExpress.Persistent.Base.ReflectionHelper.UseAssemblyResolutionCache = true;
        }

        protected virtual WinApplication CreateApplication()
        {
            var app = new ModuleNameWindowsFormsApplication();
            using (app.Initialize())
            {
                app.ApplicationName = "Scissors.ModuleName";
                app.OptimizedControllersCreation = true;
                app.RunSetupInNewThread = true;

                app.UseOldTemplates = false;
                app.UseLightStyle = true;

                app.LinkNewObjectToParentImmediately = true;
                app.CheckCompatibilityType = CheckCompatibilityType.ModuleInfo;
                app.DatabaseUpdateMode
                    = System.Diagnostics.Debugger.IsAttached
                    ? DatabaseUpdateMode.UpdateDatabaseAlways
                    : DatabaseUpdateMode.UpdateOldDatabase;

                app.ConnectionString = GetConnectionString();

                app.Modules.AddRange(new ModuleBase[]
                {
                    new SystemModule(),
                    new SystemWindowsFormsModule(),
                    new ModuleNameAppModule(),
                    new ModuleNameAppWindowsFormsModule(),
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
#if (UseEasyTest)
//-:cnd:noEmit
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null)
            {
                return ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
//+:cnd:noEmit
#endif
#endif
            InMemoryDataStoreProvider.Register();
            return InMemoryDataStoreProvider.ConnectionString;
        }
    }
}