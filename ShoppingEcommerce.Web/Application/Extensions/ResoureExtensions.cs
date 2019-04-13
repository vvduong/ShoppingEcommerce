using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;


namespace ShoppingEcommerce.Web
{
  public static class ResoureExtensions
  {
    #region NEW IMPLEMENT

    /// <summary>
    /// Get resource value by key with specific culture or default
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="key"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static string Localize(this HtmlHelper htmlHelper, string key, CultureInfo cultureInfo = null)
    {
      if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        throw new ArgumentException(nameof(key));

      if (cultureInfo == null) cultureInfo = CultureInfo.CurrentUICulture;

      var area = htmlHelper.ViewContext.RouteData.DataTokens["area"];
      var controller = htmlHelper.ViewContext.RouteData.Values["controller"];
      var action = htmlHelper.ViewContext.RouteData.Values["action"];

      var resourceKey = area != null &&
                        !string.IsNullOrEmpty(area.ToString()) &&
                        !string.IsNullOrWhiteSpace(area.ToString())
          ? $"{area.ToString().ToTitleCase(cultureInfo)}." +
            $"{controller.ToString().ToTitleCase(cultureInfo)}." +
            $"{action.ToString().ToTitleCase(cultureInfo)}." +
            $"{key.ToCamelCase(cultureInfo)}"
          : $"_root.{controller.ToString().ToTitleCase(cultureInfo)}." +
            $"{action.ToString().ToTitleCase(cultureInfo)}." +
            $"{key.ToCamelCase(cultureInfo)}";

      return GetResourceValue(resourceKey, cultureInfo);
    }

    /// <summary>
    /// Get resource value by key and set default value (vi language if needed) with specific culture or default
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static string Localize(this HtmlHelper htmlHelper, string key, string value, CultureInfo cultureInfo = null)
    {
      if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        throw new ArgumentException(nameof(key));

      if (cultureInfo == null) cultureInfo = CultureInfo.CurrentUICulture;

      var area = htmlHelper.ViewContext.RouteData.DataTokens["area"];
      var controller = htmlHelper.ViewContext.RouteData.Values["controller"];
      var action = htmlHelper.ViewContext.RouteData.Values["action"];

      var resourceKey = area != null &&
                        !string.IsNullOrEmpty(area.ToString()) &&
                        !string.IsNullOrWhiteSpace(area.ToString())
          ? $"{area.ToString().ToTitleCase(cultureInfo)}." +
            $"{controller.ToString().ToTitleCase(cultureInfo)}." +
            $"{action.ToString().ToTitleCase(cultureInfo)}." +
            $"{key.ToCamelCase(cultureInfo)}"
          : $"_root.{controller.ToString().ToTitleCase(cultureInfo)}." +
            $"{action.ToString().ToTitleCase(cultureInfo)}." +
            $"{key.ToCamelCase(cultureInfo)}";

      return GetResourceValue(resourceKey, value, cultureInfo);
    }

    /// <summary>
    /// Get resource value by exact key (without any prefixes) with specific culture or default
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="key"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static string LocalizeExact(this HtmlHelper htmlHelper, string key, CultureInfo cultureInfo = null)
    {
      if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        throw new ArgumentException(nameof(key));

      if (cultureInfo == null) cultureInfo = CultureInfo.CurrentUICulture;

      var resourceKey = $"_common.{key.ToCamelCase(cultureInfo)}";

      return GetResourceValue(resourceKey, cultureInfo);
    }

    /// <summary>
    /// Get resource value by exact key (without any prefixes) and set default value (vi language if needed) with specific culture or default
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static string LocalizeExact(this HtmlHelper htmlHelper, string key, string value, CultureInfo cultureInfo = null)
    {
      if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        throw new ArgumentException(nameof(key));

      if (cultureInfo == null) cultureInfo = CultureInfo.CurrentUICulture;

      var resourceKey = $"_common.{key.ToCamelCase(cultureInfo)}";

      return GetResourceValue(resourceKey, value, cultureInfo);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    private static string GetResourceValue(string key, CultureInfo cultureInfo)
    {
      //return ServiceFactory.Get<ILocalizationService>().GetResourceValue(key, cultureInfo.Name);
      return string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    private static string GetResourceValue(string key, string value, CultureInfo cultureInfo)
    {
      //return ServiceFactory.Get<ILocalizationService>().GetResourceValue(key, value, cultureInfo.Name);
      return string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    private static string ToTitleCase(this string value, CultureInfo cultureInfo)
    {
      return string.IsNullOrWhiteSpace(value) ? value : cultureInfo.TextInfo.ToTitleCase(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static string ToCamelCase(this string value, CultureInfo cultureInfo)
    {
      if (string.IsNullOrWhiteSpace(value)) return value;
      if (value.Length == 1) return value.ToLower(cultureInfo);
      return char.ToLower(value[0], cultureInfo) + value.Substring(1);
    }

    #endregion

    #region Multilanguage

    /// <summary>
    ///     Gets the specific language text from the language key
    /// </summary>
    /// <param name="helper"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Lang(string key)

    {
      var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"].ToString();
      var area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];

      if (string.IsNullOrEmpty(key)) throw new Exception("Key not null or empty!");

      var beginKey = area != null
          ? string.Concat(area.ToString(), ".", controller, ".", key)
          : string.Concat(controller, ".", key);

      return GetResourceString(beginKey);
    }

    public static string ResourceValue(string key)
    {
      if (string.IsNullOrEmpty(key)) throw new Exception("Key not null or empty!");

      return GetResourceString(key);
    }

    #region Form Control Title

    public static string LangTitle(this HtmlHelper helper)
    {
      var controller = helper.ViewContext.RouteData.Values["Controller"];
      var action = helper.ViewContext.RouteData.Values["action"];
      var area = helper.ViewContext.RouteData.DataTokens["area"];

      var beginKey = area != null
          ? string.Concat(area.ToString(), ".", controller, ".", action, ".", SystemConstants.Title)
          : string.Concat(controller.ToString(), ".", action, ".", SystemConstants.Title);

      return GetResourceString(beginKey);
    }

    public static string LangTitle(this HtmlHelper helper, string key)
    {
      var controller = helper.ViewContext.RouteData.Values["Controller"];
      var action = helper.ViewContext.RouteData.Values["action"];
      var area = helper.ViewContext.RouteData.DataTokens["area"];

      if (string.IsNullOrEmpty(key)) throw new Exception("Key not null or empty!");

      var beginKey = area != null
          ? string.Concat(area.ToString(), ".", controller, ".", action, ".", key)
          : string.Concat(controller.ToString(), ".", action, ".", key);

      return GetResourceString(beginKey);
    }

    #endregion

    #region Module

    public static string LangTitleModule(this HtmlHelper helper)
    {
      var controller = helper.ViewContext.RouteData.Values["Controller"];
      var area = helper.ViewContext.RouteData.DataTokens["area"];

      var beginKey = area != null
          ? string.Concat(area.ToString(), ".", controller, ".", SystemConstants.Title)
          : string.Concat(controller.ToString(), ".", SystemConstants.Title);

      return GetResourceString(beginKey);
    }

    public static string LangTitleModule(this HtmlHelper helper, string key)
    {
      var controller = helper.ViewContext.RouteData.Values["Controller"];
      var area = helper.ViewContext.RouteData.DataTokens["area"];

      var beginKey = area != null
          ? string.Concat(area.ToString(), ".", controller, ".", key)
          : string.Concat(controller.ToString(), ".", key);

      return GetResourceString(beginKey);
    }

    #endregion

    public static string Lang(this HtmlHelper helper, string key)
    {
      var controller = helper.ViewContext.RouteData.Values["Controller"];
      var area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];

      if (string.IsNullOrEmpty(key)) throw new Exception("Key not null or empty!");

      var beginKey = area != null
          ? string.Concat(area.ToString(), ".", controller, ".", key)
          : string.Concat(controller.ToString(), ".", key);
      return GetResourceString(beginKey);
    }

    private static string GetResourceString(string key)
    {
      //var localizationService = ServiceFactory.Get<ILocalizationService>();

      //return StringUtils.getResourceValueKey(!AppSettings.ReleaseLanguageResourceKey
      //    ? localizationService.GetResourceString(key, false)
      //    : localizationService.GetResourceString(key));

      return string.Empty;
    }

    #endregion
  }
}
