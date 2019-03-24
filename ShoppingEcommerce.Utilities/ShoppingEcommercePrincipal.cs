using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LacViet.ShoppingEcommerce.Utilities
{

    interface IShoppingEcommercePrincipal : IPrincipal
    {
        Guid ID { get; set; }
        string LoginName { get; set; }
        string LanguageCulture { get; set; }
        bool IsAdmin { get; set; }
        Guid ApplicationID { get; set; }
        string ServerName { get; set; }
        string DBName { get; set; }
        string LoginDB { get; set; }
        string Password { get; set; }
        List<string> MembershipRole { get; set; }
    }
    public class ShoppingEcommercePrincipal : IShoppingEcommercePrincipal
    {
        #region - DECLARE -
        #endregion
        #region - PROPERTY -
        public Guid ID { get; set; }
        public string LoginName { get; set; }
        public IIdentity Identity { get; set; }
        public string LanguageCulture { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ApplicationID { get; set; }
        public List<string> MembershipRole { get; set; }
        private string serverName;
        public string ServerName
        {
            get
            {
                return EncryptionUtils.DecryptText(serverName);
            }
            set { serverName = value; }
        }
        private string dBName;
        public string DBName
        {
            get
            {
                return EncryptionUtils.DecryptText(dBName);
            }
            set { dBName = value; }
        }
        private string loginDB;
        public string LoginDB
        {
            get
            {
                return EncryptionUtils.DecryptText(loginDB);
            }
            set { loginDB = value; }
        }
        private string password;
        public string Password
        {
            get
            {
                return EncryptionUtils.DecryptText(password);
            }
            set { password = value; }
        }
        public bool IsInRole(string role)
        {

            return MembershipRole.Where(name=>name.ToString().ToLower()== role).Count()>0;
            //return this.MembershipUser != null &&this.MembershipUser.MembershipRoles != null && role != null && this.MembershipUser.MembershipRoles.Where(p=>p.RoleName.Equals(role,StringComparison.CurrentCultureIgnoreCase)).Count()>0;
        }
        #endregion
        public ShoppingEcommercePrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }
    }
    public class ShoppingEcommercePrincipalSerializeModel
    {
        public Guid ID { get; set; }
        public string LoginName { get; set; }
        public string LanguageCulture { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsAdmin { get; set; }

        public Guid ApplicationID { get; set; }
        public string ServerName { get; set; }
        public string DBName { get; set; }
        public string LoginDB { get; set; }
        public string Password { get; set; }
        public List<string> MembershipRole { get; set; }
    }
}
