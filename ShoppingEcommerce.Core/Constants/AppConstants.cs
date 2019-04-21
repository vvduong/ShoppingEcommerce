using System.Collections.Generic;

namespace ShoppingEcommerce.Core.Constants
{
    public static class AppConstants
    {
        public const string SPUserName = "spadmin";
        public const string SPPassword = "Adm!nHPS!@#123";
        public const string SPWebURL = "http://demo2016.bioportal.vn";
        public const string SPAuthenticationService = "/VBTK/_vti_bin/authentication.asmx";
        public const string SPCopyService = "/VBTK/_vti_bin/copy.asmx";
        public const string SPDocumentList = "/VBTK/TempDocument/";

        public const int SaltSize = 24;
        public const string DefaultLanguage = "Language.DefaultLanguage";
        public const string EncryptionKey = "LacVietHPS!@#123"; // private key: dung de ma hoa va giai ma (must be 16 length)
        // Cookie names
        public const string LanguageIdCookieName = "LanguageCulture";

        // Cache names
        //TODO - Move to cache keys
        public const string LocalisationCacheName = "Localization-";
        public static string LanguageStrings = string.Concat(LocalisationCacheName, "LangStrings-");

        // View Bag / Temp Data Constants
        public const string MessageViewBagName = "Message";
        public const string DefaultCategoryViewBagName = "DefaultCategory";
        public const string GlobalClass = "GlobalClass";
        public const string CurrentAction = "CurrentAction";
        public const string CurrentController = "CurrentController";
        public const string MemberRegisterViewModel = "MemberRegisterViewModel";
        
        // Main admin role [This should never be changed]
        public const string AdminRoleName = "Administrator";

        // Main guest role [This should never be changed]
        // This is the role a non logged in user defaults to
        public const string GuestRoleName = "Guest";
        public const string UserRoleName = "User";

        //------------ End Permissions ----------

        // Paging options
        public const string PagingUrlFormat = "{0}?p={1}";


        //Mobile Check Name
        public const string IsMobileDevice = "IsMobileDevice";

    }
}
