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


      bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
      bundles.Add(new StyleBundle("~/Contents/vendor/bootstrap/css").Include(
                      "~/Contents/vendor/bootstrap/css/bootstrap.min.css"));
      //bundles.Add(new StyleBundle("~/bundles/css").Include(
      //              "~/Contents/vendor/bootstrap/css/bootstrap.min.css",
      //              "~/Contents/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
      //              "~/Contents/fonts/iconic/css/material-design-iconic-font.min.css",
      //              "~/Contents/fonts/linearicons-v1.0.0/icon-font.min.css",
      //              "~/Contents/vendor/animate/animate.css",
      //              "~/Contents/vendor/css-hamburgers/hamburgers.min.css",
      //              "~/Contents/vendor/animsition/css/animsition.min.css",
      //              "~/Contents/vendor/select2/select2.min.css",
      //              "~/Contents/vendor/daterangepicker/daterangepicker.css",
      //              "~/Contents/vendor/slick/slick.css",
      //              "~/Contents/vendor/MagnificPopup/magnific-popup.css",
      //              "~/Contents/vendor/perfect-scrollbar/perfect-scrollbar.css",
      //              "~/Contents/css/util.css",
      //              "~/Contents/css/main.css"
      //              ));
    }
  }
}
