using Libraries.Core.Backend.Common;
using Libraries.Core.Frontend.Components.Themes.StartBootStrapSbAdmin2.jQuery;
using Microsoft.Practices.Unity;

namespace Libraries.Core.Frontend.Components.Themes.StartBootStrapSbAdmin2
{
    public class TypeFabric:BaseTypeFabric
    {
        public override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<jQueryBundle, jQueryBundle>();
        }
    }
}
