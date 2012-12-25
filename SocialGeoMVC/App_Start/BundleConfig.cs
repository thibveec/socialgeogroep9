﻿using System.Web;
using System.Web.Optimization;

namespace SocialGeoMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                        "~/Scripts/foundation.min.js",
                        "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/foundation/backoffice").Include(
                        "~/Scripts/foundation.min.js",
                        "~/Scripts/backoffice.js",
                        "~/Scripts/chosen.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/foundation/backoffice/login").Include(
                        "~/Scripts/foundation.min.js",
                        "~/Scripts/backofficelogin.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                        "~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/normalize.css",
                "~/Content/foundation.min.css",
                "~/Content/app.css"));

            bundles.Add(new StyleBundle("~/Content/backoffice/login/css").Include(
               "~/Content/normalize.css",
               "~/Content/foundation.min.css",
               "~/Content/backofficelogin.css"));

            bundles.Add(new StyleBundle("~/Content/backoffice/css").Include(
              "~/Content/normalize.css",
              "~/Content/foundation.min.css",
              "~/Content/backoffice.css",
              "~/Content/chosen.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}