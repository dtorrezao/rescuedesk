using System.Web;
using System.Web.Optimization;

namespace RescueDesk
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
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/BackOffice").Include(
                      "~/Content/animate.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/cs-skin-elastic.css",
                      "~/Content/flag-icon.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/normalize.css",
                      "~/Content/style.css",
                      "~/Content/rescuedesk.css",
                      "~/Content/lib/datatable/buttons.bootstrap.min.css",
                      "~/Content/lib/datatable/buttons.dataTables.min.css",
                      "~/Content/lib/datatable/dataTables.bootstrap.min.css",
                      "~/Content/themify-icons.css",
                      "~/Content/lib/chosen/chosen.min.css",
                      "~/Content/variables.css"));

            bundles.Add(new ScriptBundle("~/bundles/BackOffice").Include(
                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/popper.min.js",
                        "~/Scripts/plugins.js",
                        "~/Scripts/main.js",
                        "~/Scripts/chart-js/Chart.bundle.js",
                       "~/Scripts/data-table/datatables.min.js",
                       "~/Scripts/data-table/dataTables.bootstrap.min.js",
                       "~/Scripts/data-table/dataTables.buttons.min.js",
                       "~/Scripts/data-table/buttons.bootstrap.min.js",
                       "~/Scripts/data-table/jszip.min.js",
                       "~/Scripts/data-table/pdfmake.min.js",
                       "~/Scripts/data-table/vfs_fonts.js",
                       "~/Scripts/data-table/buttons.html5.min.js",
                       "~/Scripts/data-table/buttons.print.min.js",
                       "~/Scripts/data-table/buttons.colVis.min.js",
                       "~/Scripts/data-table/datatables-init.js",
                       "~/Scripts/chosen/chosen.jquery.min.js"
                        //"~/Scripts/dashboard.js",
                        //"~/Scripts/widgets.js"
                        ));
        }
    }
}
