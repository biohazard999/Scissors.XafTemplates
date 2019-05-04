using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;
using Scissors.ModuleNameApp;

namespace Scissors.ModuleNameApp.Win
{
    public class ModuleNameAppWindowsFormsModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore() => new ModuleTypeList(new[]
        {
            typeof(SystemModule),
            typeof(SystemWindowsFormsModule),
            typeof(ModuleNameAppModule)
        });
    }
}
