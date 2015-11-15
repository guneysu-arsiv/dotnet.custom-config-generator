using System;
using System.Configuration;

namespace Teknikprogramlama.Configuration.Manager.OAuth
{
    public abstract class OAuthElement : ConfigurationElement
    {
        [ConfigurationProperty(name: "authorization_url")]
        public Uri AuthorizationUrl
        {
            get { return (Uri) this["authorization_url"]; }
            set { this["authorization_url"] = value; }
        }

        [ConfigurationProperty(name:"client_id", DefaultValue = "Client Id")]
        public string ClientId
        {
            get { return (string) this["client_id"]; }
            set { this["client_id"] = value; }
        }

        [ConfigurationProperty(name: "client_secret", DefaultValue = "Client Secret")]
        public string ClientSecret
        {
            get { return (string)this["client_secret"]; }
            set { this["client_secret"] = value; }
        }

        [ConfigurationProperty(name: "redirect_uri", DefaultValue = "Redirect Url")]
        public Uri RedirectUrl
        {
            get { return (Uri) this["client_secret"]; }
            set { this["client_secret"] = value; }
        }

        [ConfigurationProperty(name: "access_token", DefaultValue = "Access Token")]
        public string AccessToken
        {
            get { return (string) this["access_token"]; }
            set { this["access_token"] = value; }
        }
    }
}