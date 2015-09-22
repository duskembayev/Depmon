using System.Web.Optimization;

namespace Depmon.Server.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/main")
                .Include("~/Scripts/bundle.js"));
        }
    }
}