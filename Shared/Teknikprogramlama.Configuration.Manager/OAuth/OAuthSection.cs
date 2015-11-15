using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Teknikprogramlama.Configuration.Manager.OAuth
{
    public class OAuthSection : ConfigurationSection
    {
        private static string sectionName = "OAuth";

        #region Public Methods

        /// <summary>
        /// Get this configuration set from the application's default config file
        /// </summary>
        /// <param name="sectionName"></param>
        public static OAuthSection Open()
        {
            System.Reflection.Assembly assy =
                System.Reflection.Assembly.GetEntryAssembly();
            return Open(assy.Location);
        }

        ///<summary>
        ///Get this configuration set from a specific config file
        ///</summary>
        protected static OAuthSection Open(string path)
        {
            if ((object)_instance == null)
            {
                if (path.EndsWith(".config",
                    StringComparison.InvariantCultureIgnoreCase))
                    _spath = path.Remove(path.Length - 7);
                else
                    _spath = path;
                //Configuration config = ConfigurationManager.OpenExeConfiguration(spath);
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = $@"{path}.config";
                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

                if (config.Sections[sectionName] == null || config.Sections[sectionName] is System.Configuration.DefaultSection)
                {
                    _instance = new OAuthSection().Copy(sectionName);
                    config.Sections.Remove(sectionName);
                    config.Sections.Add(sectionName, _instance);
                    config.Save(ConfigurationSaveMode.Modified);
                }
                else
                    _instance =
                        (OAuthSection)config.Sections[sectionName];
            }
            return _instance;
        }

        ///<summary>
        ///Create a full copy of the current properties
        ///</summary>
        public OAuthSection Copy(string sectionName)
        {
            OAuthSection copy = new OAuthSection();
            string xml = SerializeSection(this,
                sectionName, ConfigurationSaveMode.Full);
            System.Xml.XmlReader rdr =
                new System.Xml.XmlTextReader(new System.IO.StringReader(xml));
            copy.DeserializeSection(rdr);
            return copy;
        }

        ///<summary>
        ///Save the current property values to the config file
        ///</summary>
        public void Save(string sectionName, ConfigurationSaveMode method = ConfigurationSaveMode.Modified)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(_spath);
            OAuthSection section = config.Sections[sectionName] as OAuthSection;
            if (section == null)
            {
                config.Save(method);
                return;
            }

            section.LockItem = true;
            foreach (ConfigurationProperty prop in section.Properties)
            {
                string name = prop.Name;
                section.SetPropertyValue(section.Properties[name], this[name], false);
            }
            config.Save(method);
        }

        #endregion Public Methods

        #region Helper Methods
        private ConfigurationPropertyAttribute GetConfigNameAttr(out string name, [CallerMemberName] string s = null)
        {
            var self = this.GetType();
            //var cm = MethodBase.GetCurrentMethod(); // Returns GetConfigNameAttr. [TODO] HATALI
            StackTrace stackTrace = new StackTrace();

            //var cm = stackTrace.GetFrame(1).GetMethod(); [1]
            //var cm = new StackFrame(1).GetMethod(); // [2] Alternatif ve daha hızlı
            //var cmName = cm.Name;                   // [2]
            var cmName = s;                           // [3] En kolay ve hızlısı

            var f = cmName?.Replace("get_", null).Replace("set_", null);

            var current = self.GetProperty(f);
            var attr = current.
                GetCustomAttributes(true).
                OfType<ConfigurationPropertyAttribute>()
                .FirstOrDefault();

            name = attr?.Name;
            return attr;
        }
        #endregion

        #region Properties

        public static OAuthSection Default
        {
            get { return DefaultInstance; }
        }

        internal static OAuthSection DefaultInstance => DefaultCopy;

        #endregion Properties

        #region Fields
        private static string _spath;
        private static OAuthSection _instance = null;

        protected static OAuthSection DefaultCopy { get; } = new OAuthSection();

        static OAuthSection()
        {
        }

        #endregion Fields

        #region Elements

        [ConfigurationProperty(name: "Github")]
        public Github Github
        {
            get { return (Github) this["Github"]; }
            set { this["Github"] = value; }
        }

        [ConfigurationProperty(name: "Twitter")]
        public Twitter Twitter => (Twitter)this["Twitter"];

        [ConfigurationProperty(name: "Google")]
        public Google Google => (Google)this["Google"];

        [ConfigurationProperty(name: "Instagram")]
        public Instagram Instagram => (Instagram)this["Instagram"];

        [ConfigurationProperty(name: "Facebook")]
        public Facebook Facebook
        {
            get { return (Facebook) this["Facebook"]; }
            set { this["Facebook"] = value; }
        }

        #endregion
    }
}