using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace Scissors.Modules.ModuleName
{
    public class ModuleNameModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore() => new ModuleTypeList(new[]
        {
            typeof(SystemModule)
        });
    }
}
