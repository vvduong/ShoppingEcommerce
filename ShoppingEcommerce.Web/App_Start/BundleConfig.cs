using System.Web;
using System.Web.Optimization;

namespace ShoppingEcommerce.Web
{
  public class BundleConfig
  {
    // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
      //            "~/Scripts/jquery-{version}.js"));

      //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
      //            "~/Scripts/jquery.validate*"));

      //// Use the development version of Modernizr to develop with and learn from. Then, when you're
      //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
      //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
      //            "~/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.validate*"));


      //bundles.Add(new StyleBundle("~/Content/css").Include(
      //                "~/Content/bootstrap.min.css",
      //                "~/Content/bootstrap.min.css",
      //                "~/Content/font-awesome.min.css",
      //                "~/Content/material-design-iconic-font.min.css",
      //                "~/Content/icon-font.min.css",
      //                "~/Content/animate.css",
      //                "~/Content/hamburgers.min.css",
      //                "~/Content/animsition.min.css",
      //                "~/Content/select2.min.css",
      //                "~/Content/daterangepicker.css",
      //                "~/Content/slick.css",
      //                "~/Content/magnific-popup.css",
      //                "~/Content/perfect-scrollbar.css",
      //                "~/Content/util.css",
      //                "~/Content/main.css"
      //                ));
      //bundles.Add(new StyleBundle("~/Content/vendor/bootstrap/css").Include(
      //                "~/Content/vendor/bootstrap/css/bootstrap.min.css"));
      bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                    "~/Content/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                    "~/Content/fonts/iconic/css/material-design-iconic-font.min.css",
                    "~/Content/fonts/linearicons-v1.0.0/icon-font.min.css",
                    "~/Content/vendor/animate/animate.css",
                    "~/Content/vendor/css-hamburgers/hamburgers.min.css",
                    "~/Content/vendor/animsition/css/animsition.min.css",
                    "~/Content/vendor/select2/select2.min.css",
                    "~/Content/vendor/daterangepicker/daterangepicker.css",
                    "~/Content/vendor/slick/slick.css",
                    "~/Content/vendor/MagnificPopup/magnific-popup.css",
                    "~/Content/vendor/perfect-scrollbar/perfect-scrollbar.css",
                    "~/Content/css/util.css",
                    "~/Content/css/main.css"
                    ));
    }
  }
}
