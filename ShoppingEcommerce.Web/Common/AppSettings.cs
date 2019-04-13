using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace ShoppingEcommerce.Web
{
    public class AppSettings
    {
        private AppSettings() { }
        private static string getAppSettings(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool ReleaseLanguageResourceKey = bool.Parse(getAppSettings("ReleaseLanguageResourceKey"));
        public static string ThemeDefault = getAppSettings("ThemeDefault");
        public static string Email = getAppSettings("Email");
        public static string loginUrl = getAppSettings("loginUrl");
        public static string EmailRecive = getAppSettings("EmailRecive");
        public static string Password = getAppSettings("Password");
        public static string Host = getAppSettings("Host");
        public static string SmtpClient = getAppSettings("SmtpClient");
        public static string Port = getAppSettings("Port");
        public static string EnalbleSSL = getAppSettings("EnalbleSSL");
        public static string DisplayName = getAppSettings("DisplayName");
        public static string MaxInvalidPasswordAttempts = getAppSettings("MaxInvalidPasswordAttempts");
        public static string ApplicationId = getAppSettings("ApplicationId");
        public static string AuthenticateMode = getAppSettings("AuthenticateMode");
        public static string ADDomain = getAppSettings("ADDomain");
        public static string TempFileDocuments = getAppSettings("TempFileDocuments");
        public static bool LoginDebugMode = bool.Parse(getAppSettings("LoginDebugMode"));
        public static string FolderScanFiles = getAppSettings("FolderScanFiles");
        public static string FileComments = getAppSettings("FileComments");
        public static string SharepointWebIntegrated = getAppSettings("SharepointWebIntegrated");
        public static string RijndaelSalt = getAppSettings("RijndaelSalt");
        public static string HomeUrl = getAppSettings("HomeUrl");
        public static string SurePortalBaseUrl = getAppSettings("SurePortalBaseUrl");


        public static void WriteSetting(string key, string value)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode("//appSettings");

            if (node == null)
                throw new InvalidOperationException("appSettings section not found in config file.");

            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    // add value for key
                    elem.SetAttribute("value", value);
                }
                else
                {
                    // key was not found so create the 'add' element 
                    // and set it's key/value attributes 
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }
        }

        public static void RemoveSetting(string key)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null)
                    throw new InvalidOperationException("appSettings section not found in config file.");
                else
                {
                    // remove 'add' element with coresponding key
                    node.RemoveChild(node.SelectSingleNode(string.Format("//add[@key='{0}']", key)));
                    doc.Save(getConfigFilePath());
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception(string.Format("The key {0} does not exist.", key), e);
            }
        }

        private static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        private static string getConfigFilePath()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }
        public static bool UpdateSetting(string key, string value)
        {
            try
            {
                Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                configuration.AppSettings.Settings[key].Value = value;
                configuration.Save();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
