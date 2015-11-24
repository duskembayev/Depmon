using System.Web.Optimization;

namespace Depmon.Server.Portal.React
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/public/assets/script.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/vendor")
                .Include("~/public/assets/vendor.js")
                );

            bundles.Add(new StyleBundle("~/bundles/style")
                .Include("~/public/assets/style.css")
                );
        }
    }
}
