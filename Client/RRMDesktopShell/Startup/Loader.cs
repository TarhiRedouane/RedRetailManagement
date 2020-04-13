using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RRMDesktopShell.Startup
{
    public  class Loader
    {
        private static Assembly[] _assemblies =>
            AppDomain.CurrentDomain.GetAssemblies()
                                   .Where(ass => ass.FullName.StartsWith("RRM"))
                                   .ToArray();

        public static Assembly[] CreateAssemblies(Assembly assembly)
        {
            var listOfAsemblies = new List<Assembly>(_assemblies);
            listOfAsemblies.Add(assembly);
            return listOfAsemblies.ToArray();
        }
    }
}
