using System.Web.Optimization;

namespace Html.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            StyleBundle mainBundle = new StyleBundle("~/Content/css");
            mainBundle.Include("~/Content/site.css");
            mainBundle.Include("~/Content/Home/RootElement.css");
            mainBundle.Include("~/Content/Home/FixedHeader.css");
            mainBundle.Include("~/Content/Home/BannerPanel.css");
            mainBundle.Include("~/Content/Home/BannerSwitchPanel.css");
            mainBundle.Include("~/Content/Home/StepsPanel.css");
            mainBundle.Include("~/Content/Home/NewsPanel.css");
            mainBundle.Include("~/Content/Home/Footer.css");
            mainBundle.Include("~/Content/Home/WikiLinksList.css");
            mainBundle.Include("~/Content/Home/VinCheckPanel.css");
            mainBundle.Include("~/Content/TopicDetails/TopicDetails.css");
            mainBundle.Include("~/Content/Management/NewsManagement.css");
            bundles.Add(mainBundle);

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