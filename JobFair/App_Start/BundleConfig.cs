using System.Web;
using System.Web.Optimization;

namespace JobFair
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery-3.6.0.min.js",
                        "~/Assets/js/link.js",
                        "~/Assets/js/cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/js/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/all.min.css"));

            // User assets
            bundles.Add(new ScriptBundle("~/bundles/user_js").Include(
                    "~/Assets/js/user_scripts.js"));


            bundles.Add(new StyleBundle("~/Content/user_css").Include(
                      "~/Assets/css/user_styles.css"));

            // Admin site assets
            bundles.Add(new StyleBundle("~/Content/admin_css").Include(
                        "~/Assets/css/admin_styles.css"));
        }
    }
}
