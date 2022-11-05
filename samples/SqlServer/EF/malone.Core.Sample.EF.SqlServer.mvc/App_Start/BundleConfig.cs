using System.Web.Optimization;

namespace malone.Core.Sample.EF.SqlServer.mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/libs/jquery/jquery-{version}.js",
                        "~/Content/js/jquery.Message.js",
                        "~/Content/js/url-helper.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/libs/jquery.validate/jquery.validate*",
                        "~/libs/jquery.validate/jquery.validate.unobstrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/libs/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/libs/bootstrap/js/bootstrap.js",
                      "~/libs/others/js/bootstrap-datepicker.js",
                      "~/libs/others/js/bootstrap-datepicker.es.min.js",
                      "~/libs/others/js/bootstrap-switch.js",
                      "~/libs/others/js/jquery.datepicker.js",
                      "~/libs/others/js/jquery.switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/ListController").Include(
                      "~/Content/js/controller-list.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/libs/bootstrap/css/bootstrap.css",
                      "~/libs/others/css/bootstrap-datepicker3.css",
                      "~/libs/others/css/bootstrap-switch.css",
                      "~/Content/css/site.css"));
        }
    }
}
