using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;
using Scissors.Modules.ModuleName;

namespace Scissors.Modules.ModuleName.Win
{
    public class ModuleNameWindowsFormsModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore() => new ModuleTypeList(new[]
        {
            typeof(SystemModule),
            typeof(SystemWindowsFormsModule),
            typeof(ModuleNameModule)
        });
    }
}
