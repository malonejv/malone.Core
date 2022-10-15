using System.Web.Optimization;

namespace malone.Core.Sample.AdoNet.SqlServer.mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.Message.js",
                        "~/Scripts/url-helper.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobstrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-datepicker.es.min.js",
                      "~/Scripts/bootstrap-switch.js",
                      "~/Scripts/jquery.datepicker.js",
                      "~/Scripts/jquery.switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/ListController").Include(
                      "~/Scripts/controller-list.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/bootstrap-switch.css",
                      "~/Content/site.css"));
        }
    }
}
