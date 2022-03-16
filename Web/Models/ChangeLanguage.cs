using Microsoft.Extensions.Localization;
using Services;
using System.Reflection;

namespace Web.Models
{
    public class ChangeLanguage
    {

       
        private readonly IStringLocalizer _localizer;

        public ChangeLanguage( IStringLocalizerFactory factory)
        {
          
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }


        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }

    }
}
