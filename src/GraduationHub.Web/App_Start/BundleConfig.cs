﻿using System.Web.Optimization;

namespace GraduationHub.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libraries").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/underscore.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables-1.10.0/media/js/jquery.dataTables.js",
                "~/Scripts/DataTables-1.10.0/plugins/dataTables.bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/jasny-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/app/graduateHubApp.js")
                .IncludeDirectory("~/Scripts/app/utilities", "*.js")
                .IncludeDirectory("~/Scripts/app/controllers", "*.js")
                .IncludeDirectory("~/Scripts/app/services", "*.js")
                );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/DataTables-1.10.0/plugins/dataTables.bootstrap.css",
                "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}