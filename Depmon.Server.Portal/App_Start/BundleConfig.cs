using System.Web.Optimization;
using System.Web.Optimization.React;

namespace Depmon.Server.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new JsxBundle("~/bundles/main")
                .IncludeDirectory("~/Scripts/app", "*.jsx", true));

            bundles.Add(new StyleBundle("~/bundles/styles"));

            bundles.Add(new ScriptBundle("~/bundles/headscripts"));

            bundles.Add(new ScriptBundle("~/bundles/footscripts"));
        }
    }
}