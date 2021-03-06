using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Xpo;

namespace Scissors.ModuleName.Win
{
    public class ModuleNameWindowsFormsApplication : WinApplication
    {
        public ModuleNameWindowsFormsApplication()
        {
            CustomizeLanguagesList += ModuleNameWindowsFormsApplication_CustomizeLanguagesList;
            DatabaseVersionMismatch += ModuleNameWindowsFormsApplication_DatabaseVersionMismatch;
            ExecuteStartupLogicBeforeClosingLogonWindow = true;
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, true), false));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private void ModuleNameWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e)
        {
            var userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
            {
                e.Languages.Add(userLanguageName);
            }
        }

        private void ModuleNameWindowsFormsApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
        {
#if (UseEasyTest)
//-:cnd:noEmit
#if EASYTEST
                e.Updater.Update();
                e.Handled = true;
                return;
#endif
//+:cnd:noEmit
#endif
//-:cnd:noEmit
#if DEBUG
//+:cnd:noEmit
            e.Updater.Update();
            e.Handled = true;
//-:cnd:noEmit
#endif
//+:cnd:noEmit
//-:cnd:noEmit
#if RELEASE
//+:cnd:noEmit
            var message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
            {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
//-:cnd:noEmit
#endif
//+:cnd:noEmit
        }
    }
}