using System.Web;
using System.Web.Optimization;

namespace SIREON
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/vendors/js/core.js",    
                      "~/Content/vendors/apexcharts/apexcharts.min.js",
                      "~/Content/vendors/chartjs/Chart.min.js",
                      "~/Content/js/charts/chartjs.addon.js",   
                      "~/Content/js/template.js", 
                      "~/Content/js/dashboard.js" ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/vendors/iconfonts/mdi/css/materialdesignicons.css",
                      "~/Content/css/shared/style.css",
                      "~/Content/css/demo_1/style.css",
                      "~/Content/images/favicon.ico"));
        }
    }
}
