using System.Web.Optimization;

namespace Depmon.Server.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainscripts")
                //.Include("~/Scripts/bootstrap.min.js")
                .Include("~/Public/scripts.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/headscripts")
                //.Include("~/Scripts/jquery-1.9.1.min.js")
                );
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-theme.min.css")
                );
        }
    }
}