using System;
using System.Security.Principal;
using ShoppingEcommerce.Extensions;
using ShoppingEcommerce.Utilities;
using Unity.Attributes;

namespace ShoppingEcommerce.Interactors
{
    public class IdentityInteractor
    {
        [Dependency] protected IIdentity Identity { get; set; }

        public string IdentityName => Identity
            .When(identity => !(identity is null) && identity.IsAuthenticated)
            .Map(identity => identity.Name)
            .Map(FindSharepointIdentityName)
            .Reduce(string.Empty);

        /// <summary>
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        private static string FindSharepointIdentityName(string identityName)
        {
            if (!identityName.Contains("#") || !identityName.Contains("|"))
            {
                if (identityName.Contains("\\"))
                {
                    return identityName;
                }

                return ApplicationConfiguration.AdDomain + "\\" + identityName;
            }

            var index = identityName.IndexOf("|", StringComparison.Ordinal) + 1;
            if (index < 0)
            {
                return identityName;
            }

            var userName = identityName.Substring(index, identityName.Length - index);

            return userName.Contains("|") ? userName.Replace("|", "\\") : userName;
        }
    }
}