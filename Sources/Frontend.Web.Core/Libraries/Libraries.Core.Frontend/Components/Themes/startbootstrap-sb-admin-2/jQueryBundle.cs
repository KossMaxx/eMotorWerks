using Libraries.Core.Backend.Common;

namespace Libraries.Core.Frontend.Components.Themes.StartBootStrapSbAdmin2.jQuery
{
    public class jQueryBundle : ComponentBaseBundleConfig
    {
        public jQueryBundle() : base("jquery")
        {
        }
        public override void RegisterBundles()
        {
            RegisterJs("Themes/StartBootStrapSbAdmin2/jquery", new[]
            {
                $"~/Themes/Themes.Core/startbootstrap-sb-admin-2/vendor/jquery/jquery.min.js",
            });
        }
    }
}
