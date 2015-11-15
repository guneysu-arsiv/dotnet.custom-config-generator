using System;
using System.Configuration;
using Teknikprogramlama.Configuration.Manager.Abstract;

namespace Teknikprogramlama.Configuration.Manager.OAuth
{
    public class Github : OAuthElement
    {
        public Github()
        {
            this.AuthorizationUrl = new Uri(@"https://github.com/login/oauth/authorize");
        }

        [ConfigurationProperty(name: "scope", DefaultValue = "Scope")]
        public string Scope
        {
            get { return (string) this["scope"]; }
            set { this["scope"] = value; }
        }

        [ConfigurationProperty(name: "state", DefaultValue = "State")]
        public string State
        {
            get { return (string)this["state"]; }
            set { this["state"] = value; }
        }

        [ConfigurationProperty(name: "code", DefaultValue = "Code")]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }
    }
}