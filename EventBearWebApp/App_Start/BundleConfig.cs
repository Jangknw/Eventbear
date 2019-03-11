using System.Web;
using System.Web.Optimization;

namespace EventBearWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/all.min",
                      "~/Content/navcss.css",
                      "~/Content/search1.css",
                      "~/Content/search2.css",
                      "~/Content/Home.css",
                      "~/Content/Booking.css",
                      "~/Content/AddLo.css",
                      "~/Content/StyleSheet2.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/AddLo").Include(
                    "~/Scripts/DropDownListUtility/DropDownListUtility.js",
                    "~/Scripts/AddLo/AddLo.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/Search").Include(
                   "~/Scripts/DropDownListUtility/DropDownListUtility.js",
                   "~/Scripts/Search/Search.js"
           ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/Home").Include(
                  "~/Scripts/DropDownListUtility/DropDownListUtility.js",
                  "~/Scripts/Home/HomeIndex.js"
          ));
        }
    }
}
