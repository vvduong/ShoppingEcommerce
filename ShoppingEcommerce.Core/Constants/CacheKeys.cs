namespace ShoppingEcommerce.Core.Constants
{
    public static class CacheKeys
    {
        public const string Domain = "ThisDomain";
        public static class PrivateMessage
        {
            public const string StartsWith = "PrivateMessage.";
        }

        public static class Member
        {
            public const string StartsWith = "Member.";
        }
        public static class User
        {
            public const string StartsWith = "User.";
        }
        public static class Role
        {
            public const string StartsWith = "Role.";
        }
        public static class UserRole
        {
            public const string StartsWith = "UserRole.";
        }
        public static class RolePermission
        {
            public const string StartsWith = "RolePermission.";
        }
        public static class Permission
        {
            public const string StartsWith = "Permission.";
        }

        public static class Language
        {
            public const string StartsWith = "Language.";
        }

        public static class Settings
        {
            public const string StartsWith = "Settings.";
            public static string Main = string.Concat(StartsWith, "mainsettings");
        }
        public static class Navigation
        {
            public const string StartsWith = "Navigation.";
        }
        public static class Module
        {
            public const string StartsWith = "Module.";
        }
    }
}
