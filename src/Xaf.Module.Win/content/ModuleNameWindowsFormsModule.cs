using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;

namespace Scissors.Modules.ModuleName.Win
{
    public class ModuleNameWindowsFormsModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore() => new ModuleTypeList(new[]
        {
            typeof(SystemModule),
            typeof(SystemWindowsFormsModule),
        });
    }
}
