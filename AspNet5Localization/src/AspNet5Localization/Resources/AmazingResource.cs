using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Localization.Resources
{
    /// <summary>
    /// This silly class is required due to the silly magic strings required so that the localization works.
    /// Don't feel like rewriting the IStringLocalizerFactory and/or IStringLocalizer class....
    /// https://github.com/aspnet/Localization/issues/150
    /// 
    /// Thanks to Joe Audette for this solution.
    /// https://github.com/joeaudette/experiments/tree/master/LocalizationWebSite/Resources
    /// 
    /// Resource files must be named with namespace...
    /// </summary>
    public class AmazingResource
    {
    }
}
