using System.Web;
using System.Web.Optimization;

namespace SchoolManagementSystems.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            ScriptBundle thirdPartyScripts = new ScriptBundle("~/Scripts/bundledscript");
            thirdPartyScripts.Include("~/Scripts/jquery-{version}.js",
                                "~/Scripts/jquery.simple-dtpicker.js",
                        "~/Scripts/jquery.tablesorter.js",
                        "~/Scripts/jquery.tablesorter.pager.js",
                        "~/Scripts/ReloadPagination.js",
                        "~/Scripts/Permission.js");

            bundles.Add(thirdPartyScripts);

            ScriptBundle layoutscript = new ScriptBundle("~/Scripts/layoutscript");
            layoutscript.Include("~/Theme/plugins/jQuery/jquery-3.1.1.min.js",
                                "~/Theme/bootstrap/js/bootstrap.min.js",
                        "~/Theme/dist/js/adminlte.min.js",
                        "~/Scripts/angular.js",
                        "~/AngularCode/ViewProfileController.js"
                       );

            bundles.Add(layoutscript);
            ScriptBundle layoutstylesheet = new ScriptBundle("~/Scripts/layoutstylesheet");
            layoutstylesheet.Include("~/Theme/bootstrap/css/bootstrap.min.css",
                                "~/Theme/dist/css/skins/skin-blue.min.css",
                        "~/Theme/dist/css/AdminLTE.min.css"
                      );

            bundles.Add(layoutstylesheet);





            bundles.Add(new StyleBundle("~/CSS/bundledcss").Include(
                      "~/CSS/bootstrap.css",
                        "~/CSS/style.css",
                        "~/Scripts/jquery.simple-dtpicker.css",
                        "~/CSS/custom.css"));

        

            //BundleTable.EnableOptimizations = true;
        }
    }
}