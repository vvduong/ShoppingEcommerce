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
      bundles.Add(new ScriptBundle("~/Content/js").Include(
                     "~/Content/vendor/jquery/jquery-3.2.1.min.js",
                     "~/Content/vendor/animsition/js/animsition.min.js",
                     "~/Content/vendor/bootstrap/js/popper.js",
                     "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                     "~/Content/vendor/select2/select2.min.js",
                     "~/Content/vendor/daterangepicker/moment.min.js",
                     "~/Content/vendor/daterangepicker/daterangepicker.js",
                     "~/Content/vendor/slick/slick.min.js",
                     "~/Content/js/slick-custom.js",
                     "~/Content/vendor/parallax100/parallax100.js",
                     "~/Content/vendor/MagnificPopup/jquery.magnific-popup.min.js",
                     "~/Content/vendor/isotope/isotope.pkgd.min.js",
                     "~/Content/vendor/sweetalert/sweetalert.min.js",
                     "~/Content/vendor/perfect-scrollbar/perfect-scrollbar.min.js",
                     "~/Content/js/main.js"
                     ));

    }
  }
}
