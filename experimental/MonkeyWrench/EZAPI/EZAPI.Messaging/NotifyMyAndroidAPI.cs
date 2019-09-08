using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Configuration;

namespace EZAPI.Messaging
{
    public enum NMANotificationPriority : sbyte
    {
        VeryLow = -2,
        Moderate = -1,
        Normal = 0,
        High = 1,
        Emergency = 2
    }

    public class NMAClient
    {
        private const string CLIENT_CONFIG_SECTION_NAME = "nmaClient";

        private const string EX_MSG_CLIENT_CONFIG_SECTION_NOT_FOUND =
            "Notify My Android client configuration section [{0}] not found; unable to proceed.";

        private const string POST_NOTIFICATION_BASE_METHOD =
            "notify?apikey={0}&application={1}&description={2}&event={3}&priority={4}";

        private const string POST_NOTIFICATION_PROVIDER_PARAMETER = "&developerkey={0}";
        private const string REQUEST_CONTENT_TYPE = "application/x-www-form-urlencoded";
        private const string REQUEST_METHOD_TYPE = "POST";

        private NMAClientConfiguration _clientCfg;

        public NMAClient() : this(null) { }

        public NMAClient(NMAClientConfiguration clientCfg_)
        {
            if (clientCfg_ == null)
            {
                var cfgSection = ConfigurationManager.GetSection(CLIENT_CONFIG_SECTION_NAME);

                if (cfgSection == null || !(cfgSection is NMAClientConfiguration))
                    throw new InvalidOperationException(String.Format(EX_MSG_CLIENT_CONFIG_SECTION_NOT_FOUND, CLIENT_CONFIG_SECTION_NAME));

                clientCfg_ = (cfgSection as NMAClientConfiguration);
            }

            _clientCfg = clientCfg_;
            _clientCfg.Validate();
        }

        public void PostNotification(NMANotification notification_)
        {
            notification_.Validate();

            var updateRequest =
                HttpWebRequest.Create(BuildNotificationRequestUrl(notification_)) as HttpWebRequest;

            updateRequest.ContentLength = 0;
            updateRequest.ContentType = REQUEST_CONTENT_TYPE;
            updateRequest.Method = REQUEST_METHOD_TYPE;

            var postResponse = default(WebResponse);

            try
            {
                postResponse = updateRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
            finally
            {
                if (postResponse != null)
                    postResponse.Close();
            }
        }

        private string BuildNotificationRequestUrl(NMANotification notification_)
        {
            if (!(_clientCfg.BaseUrl.EndsWith("/"))) _clientCfg.BaseUrl += "/";

            var nmaUrlSb = new StringBuilder(_clientCfg.BaseUrl);

            nmaUrlSb.AppendFormat(
                POST_NOTIFICATION_BASE_METHOD,
                HttpUtility.UrlEncode(_clientCfg.ApiKeychain),
                HttpUtility.UrlEncode(_clientCfg.ApplicationName),
                HttpUtility.UrlEncode(notification_.Description),
                HttpUtility.UrlEncode(notification_.Event),
                ((sbyte)(notification_.Priority)));

            if (!String.IsNullOrEmpty(_clientCfg.ProviderKey))
                nmaUrlSb.AppendFormat(
                    POST_NOTIFICATION_PROVIDER_PARAMETER,
                    HttpUtility.UrlEncode(_clientCfg.ProviderKey));

            return nmaUrlSb.ToString();
        }
    } // class

    public class NMAClientConfiguration : ConfigurationSection
    {
        private const int API_KEYCHAIN_MAX_LENGTH = 244;
        private const int APPLICATION_NAME_MAX_LENGTH = 256;

        private const string API_KEYCHAIN_CFG_PROP_NAME = "apiKeychain";
        private const string APPLICATION_NAME_CFG_PROP_NAME = "applicationName";
        private const string BASE_URL_CFG_PROP_NAME = "baseUrl";
        private const string DEFAULT_BASE_URL = @"https://nma.usk.bz/publicapi/";

        private const string EX_MSG_API_KEYCHAIN_EXCEEDS_MAX_LENGTH =
            "Provided NMA API keychain exceeds the maximum allowed length [{0}]; unable to proceed.";

        private const string EX_MSG_API_KEYCHAIN_NOT_PROVIDED =
            "NMA API keychain not provided; unable to proceed.";

        private const string EX_MSG_APPLICATION_NAME_EXCEEDS_MAX_LENGTH =
            "Provided NMA application name exceeds the maximum allowed length [{0}]; unable to proceed.";

        private const string EX_MSG_APPLICATION_NAME_NOT_PROVIDED =
            "NMA application name not provided; unable to proceed.";

        private const string EX_MSG_BASE_URL_IS_INVALID =
            "Provided NMA API base URL is invalid; unable to proceed.";

        private const string EX_MSG_BASE_URL_NOT_PROVIDED =
            "NMA API base URL not provided; unable to proceed.";

        private const string DEVELOPER_KEY_CFG_PROP_NAME = "developerKey";

        public void Validate()
        {
            if (String.IsNullOrEmpty(ApiKeychain))
                throw new InvalidOperationException(EX_MSG_API_KEYCHAIN_NOT_PROVIDED);

            if (ApiKeychain.Length > API_KEYCHAIN_MAX_LENGTH)
                throw new InvalidOperationException(String.Format(EX_MSG_API_KEYCHAIN_EXCEEDS_MAX_LENGTH, API_KEYCHAIN_MAX_LENGTH));

            if (String.IsNullOrEmpty(ApplicationName))
                throw new InvalidOperationException(EX_MSG_APPLICATION_NAME_NOT_PROVIDED);

            if (ApplicationName.Length > APPLICATION_NAME_MAX_LENGTH)
                throw new InvalidOperationException(String.Format(EX_MSG_APPLICATION_NAME_EXCEEDS_MAX_LENGTH, APPLICATION_NAME_MAX_LENGTH));

            if (String.IsNullOrEmpty(BaseUrl))
                throw new InvalidOperationException(EX_MSG_BASE_URL_NOT_PROVIDED);

            var tempUri = default(Uri);

            if (!(Uri.TryCreate(BaseUrl, UriKind.Absolute, out tempUri)))
                throw new InvalidOperationException(EX_MSG_BASE_URL_IS_INVALID);
        }

        [ConfigurationProperty(API_KEYCHAIN_CFG_PROP_NAME, IsRequired = true)]
        public string ApiKeychain
        {
            get { return this[API_KEYCHAIN_CFG_PROP_NAME] as string; }
            set { this[API_KEYCHAIN_CFG_PROP_NAME] = value; }
        }

        [ConfigurationProperty(APPLICATION_NAME_CFG_PROP_NAME, IsRequired = true)]
        public string ApplicationName
        {
            get { return this[APPLICATION_NAME_CFG_PROP_NAME] as string; }
            set { this[APPLICATION_NAME_CFG_PROP_NAME] = value; }
        }

        [ConfigurationProperty(BASE_URL_CFG_PROP_NAME, DefaultValue = DEFAULT_BASE_URL)]
        public string BaseUrl
        {
            get { return this[BASE_URL_CFG_PROP_NAME] as string; }
            set { this[BASE_URL_CFG_PROP_NAME] = value; }
        }

        [ConfigurationProperty(DEVELOPER_KEY_CFG_PROP_NAME)]
        public string ProviderKey
        {
            get { return this[DEVELOPER_KEY_CFG_PROP_NAME] as string; }
            set { this[DEVELOPER_KEY_CFG_PROP_NAME] = value; }
        }
    } // class

    public struct NMANotification
    {
        private const int DESCRIPTION_MAX_LENGTH = 10000;
        private const int EVENT_MAX_LENGTH = 1000;

        private const string EX_MSG_DESCRIPTION_EXCEEDS_MAX_LENGTH =
            "Provided notification description exceeds the maximum allowed length [{0}]; unable to proceed.";

        private const string EX_MSG_DESCRIPTION_NOT_PROVIDED =
            "Notification description not provided; unable to proceed.";

        private const string EX_MSG_EVENT_EXCEEDS_MAX_LENGTH =
            "Provided notification event exceeds the maximum allowed length [{0}]; unable to proceed.";

        private const string EX_MSG_EVENT_NOT_PROVIDED =
            "Notification event not provided; unable to proceed.";

        public void Validate()
        {
            if (String.IsNullOrEmpty(Description))
                throw new InvalidOperationException(EX_MSG_DESCRIPTION_NOT_PROVIDED);

            if (Description.Length > DESCRIPTION_MAX_LENGTH)
                throw new InvalidOperationException(String.Format(EX_MSG_DESCRIPTION_EXCEEDS_MAX_LENGTH, DESCRIPTION_MAX_LENGTH));

            if (String.IsNullOrEmpty(Event))
                throw new InvalidOperationException(EX_MSG_EVENT_NOT_PROVIDED);

            if (Event.Length > EVENT_MAX_LENGTH)
                throw new InvalidOperationException(String.Format(EX_MSG_EVENT_EXCEEDS_MAX_LENGTH, EVENT_MAX_LENGTH));
        }

        public string Event { get; set; }
        public string Description { get; set; }

        public NMANotificationPriority Priority { get; set; }
    } // struct

} // namespace
