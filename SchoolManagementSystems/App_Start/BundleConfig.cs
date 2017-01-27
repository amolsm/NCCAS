using System.Web;
using System.Web.Optimization;

namespace SchoolManagementSystems.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            ScriptBundle thirdPartyScripts = new ScriptBundle("~/Scripts/bundledscript");
            thirdPartyScripts.Include("~/Scripts/jquery-{version}.js",
                                "~/Scripts/jquery.simple-dtpicker.js",
                        "~/Scripts/jquery.tablesorter.js",
                        "~/Scripts/jquery.tablesorter.pager.js",
                        "~/Scripts/ReloadPagination.js",
                        "~/Scripts/Permission.js");

            bundles.Add(thirdPartyScripts);

          

          
            
            bundles.Add(new StyleBundle("~/CSS/bundledcss").Include(
                      "~/CSS/bootstrap.css",
                        "~/CSS/style.css",
                        "~/Scripts/jquery.simple-dtpicker.css",
                        "~/CSS/custom.css"));

        

            //BundleTable.EnableOptimizations = true;
        }
    }
}