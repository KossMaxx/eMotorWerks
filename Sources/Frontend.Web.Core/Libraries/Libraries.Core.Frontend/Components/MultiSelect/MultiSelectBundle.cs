using Libraries.Core.Backend.Common;

namespace Libraries.Core.Frontend.Components.MultiSelect
{
    public class MultiSelectBundle : ComponentBaseBundleConfig
    {
        public MultiSelectBundle() : base("MultiSelect")
        {
        }
        public override void RegisterBundles()
        {
            RegisterCss($"{BaseComponentName}", new[]
            {
                "~/Libraries/Libraries.Core.Frontend/Components/MultiSelect/css/multi-select.css",
            });
            RegisterJs($"{BaseComponentName}", new[]
            {
                "~/Libraries/Libraries.Core.Frontend/Components/MultiSelect/js/jquery.multi-select.js",
            });
        }
    }
}
