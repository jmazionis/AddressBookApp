using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace AddressBook.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/contact-list")
                .Include("~/Scripts/Custom/AddressBookApp.Common.js")
                .Include("~/Scripts/Custom/contact-list.js"));
           

            bundles.Add(new ScriptBundle("~/bundles/dataTables")
                .Include("~/Scripts/DataTables/jquery.dataTables.js")
                .Include("~/Scripts/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/styles/dataTables")                    
                    .Include("~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/styles/contact-list")                    
                    .Include("~/Content/CustomCss/contact-list.css"));

            bundles.Add(new StyleBundle("~/styles/common")
                   .Include("~/Content/CustomCss/common.css"));

            bundles.Add(new StyleBundle("~/styles/bootstrap")
                    .Include("~/Content/Bootstrap/bootstrap.css")
                    .Include("~/Content/Bootstrap/bootstrap-theme.css"));
        }
    }
}
