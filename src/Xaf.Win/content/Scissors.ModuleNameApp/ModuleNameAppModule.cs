using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace Scissors.ModuleNameApp
{
    public class ModuleNameAppModule : ModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore() => new ModuleTypeList(new[]
        {
            typeof(SystemModule)
        });
    }
}
