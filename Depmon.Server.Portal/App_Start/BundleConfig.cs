using System.Web.Optimization;
using System.Web.Optimization.React;

namespace Depmon.Server.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new JsxBundle("~/bundles/main").IncludeDirectory("~/JSX", "*.jsx", true));

            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-theme.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/headscripts")
                .Include("~/Scripts/react/react-0.13.1.min.js")
                .Include("~/Scripts/react-bootstrap/react-bootstrap.min.js")
                .Include("~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/footscripts")
                .Include("~/Scripts/bootstrap.min.js"));

        }
    }
}