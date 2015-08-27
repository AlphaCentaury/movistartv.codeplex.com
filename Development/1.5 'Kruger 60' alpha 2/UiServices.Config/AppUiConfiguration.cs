// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.Win32;
using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.UiServices.Configuration.Cache;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Configuration.Properties;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public class AppUiConfiguration
    {
        private IDictionary<Guid, IConfigurationItem> Items;

        #region Static methods

        public static AppUiConfiguration Current
        {
            get;
            private set;
        } // Current

        public static AppUiConfiguration CreateForUserConfig(UserConfig userConfig)
        {
            AppUiConfiguration config;

            config = new AppUiConfiguration();
            config.User = userConfig;
            config.Items = new Dictionary<Guid, IConfigurationItem>();

            return config;
        } // CreateForUserConfig

        public static InitializationResult Load(string overrideBasePath, Action<string> displayProgress)
        {
            AppUiConfiguration config;
            InitializationResult result;

            if (displayProgress != null) displayProgress(Properties.Texts.LoadProgress_Start);
            config = new AppUiConfiguration();
            result = config.LoadBasicConfiguration(overrideBasePath);
            if (result.IsError) return result;

            if (displayProgress != null) displayProgress(Properties.Texts.LoadProgress_ContentProvider);
            result = config.LoadContentProviderData();
            if (result.IsError) return result;

            if (displayProgress != null) displayProgress(Properties.Texts.LoadProgress_UserConfig);
            result = config.LoadUserConfiguration();
            if (result.IsError) return result;

            config.ProcessXmlConfigurationItems();

            Current = config;

            return InitializationResult.Ok;
        } // Load

        public static AppUiConfiguration LoadRegistryAppConfiguration(out InitializationResult initializationResult)
        {
            AppUiConfiguration config;

            config = new AppUiConfiguration();

            initializationResult = config.LoadRegistrySettings(null);
            if (initializationResult.IsError) return null;

            return config;
        } // LoadRegistryAppConfiguration

        protected static IList<string> GetUiCultures()
        {
            var culture = CultureInfo.CurrentUICulture;
            var tempList = new List<string>();

            while (culture.Name != "")
            {
                tempList.Add(culture.Name.ToLowerInvariant());
                culture = culture.Parent;
            } // while
            tempList.Add("<default>");

            var cultureList = new List<string>(tempList.Count);
            cultureList.AddRange(tempList);

            return cultureList.AsReadOnly();
        } // GetUiCultures

        #endregion

        public AppUiConfiguration()
        {
            Folders = new AppUiConfigurationFolders();
        } // constructor

        public AppUiConfigurationFolders Folders
        {
            get;
            protected set;
        } // Folders

        public IList<string> Cultures
        {
            get;
            protected set;
        } // Cultures

        public IDictionary<string, string> DescriptionServiceTypes
        {
            get;
            protected set;
        } // FriendlyNamesServiceTypes

        public bool DisplayPreferredOrFirst
        {
            get;
            protected set;
        } // DisplayPreferredOrFirst

        public CacheManager Cache
        {
            get;
            protected set;
        } // Cache

        public ProviderLogoMappings ProviderLogoMappings
        {
            get;
            protected set;
        } // ProviderLogoMappings

        public ServiceLogoMappings ServiceLogoMappings
        {
            get;
            protected set;
        } // ServiceLogoMappings

        public UiContentProvider ContentProvider
        {
            get;
            protected set;
        } // ContentProvider

        public string AnalyticsClientId
        {
            get;
            protected set;
        } // AnalyticsClientId

        public UserConfig User
        {
            get;
            protected set;
        } // User

        public IConfigurationItem this[Guid guid]
        {
            get { return Items[guid]; } // get
            set { Items[guid] = value; } // set
        } // this[Guid]

        public void SetConfiguration(IConfigurationItem item)
        {
            this[item.ConfigurationId] = item;
        } // SetConfiguration

        #region Public methods

        public void Save(string xmlFilePath)
        {
            foreach (var pair in Items)
            {
                User.Configuration = new XmlConfigurationItem();
                User.Configuration.XmlData = new List<XmlElement>(Items.Count);
                User.Configuration.XmlData.Add(XmlConfigurationItem.GetXmlElement(pair.Value));
            } // foreach

            XmlSerialization.Serialize(xmlFilePath, Encoding.UTF8, User);

            User.Configuration = null;
        } // Save

        #endregion

        #region Basic app configuration

        protected InitializationResult LoadBasicConfiguration(string overrideBasePath)
        {
            InitializationResult initResult;

            initResult = LoadRegistrySettings(overrideBasePath);
            if (initResult.IsError) return initResult;

            // Cultures
            Cultures = GetUiCultures();

            // Record tasks
            Folders.RecordTasks = Path.Combine(Folders.Base, "RecordTasks");

            // Cache
            Folders.Cache = Path.Combine(Folders.Base, "Cache");

            // Logos
            Folders.Logos = Path.Combine(Folders.Base, "Logos");

            // TODO: load from somewhere in a culture-aware way
            var descriptionServiceType = new Dictionary<string, string>();
            descriptionServiceType.Add("1", "SD TV");
            descriptionServiceType.Add("2", "Radio (MPEG-1)");
            descriptionServiceType.Add("3", "Teletext");
            descriptionServiceType.Add("6", "Mosaic");
            descriptionServiceType.Add("10", "Radio (AAC)");
            descriptionServiceType.Add("11", "Mosaic (AAC)");
            descriptionServiceType.Add("12", "Data");
            descriptionServiceType.Add("16", "DVB MHP");
            descriptionServiceType.Add("17", "HD TV (MPEG-2)");
            descriptionServiceType.Add("22", "SD TV (AVC)");
            descriptionServiceType.Add("25", "HD TV (AVC)");
            DescriptionServiceTypes = descriptionServiceType;

            // TODO: load from user config
            DisplayPreferredOrFirst = true;

            // Validate application configuration
            initResult = Validate();
            if (!initResult.IsOk) return initResult;

            // Initialize managers and providers
            if (!Directory.Exists(Folders.RecordTasks))
            {
                Directory.CreateDirectory(Folders.RecordTasks);
            } // if

            Cache = new CacheManager(Folders.Cache);

            ProviderLogoMappings = new ProviderLogoMappings(
                Path.Combine(Folders.Logos, Properties.InvariantTexts.FileLogoProviderMappings));

            ServiceLogoMappings = new ServiceLogoMappings(
                Path.Combine(Folders.Logos, Properties.InvariantTexts.FileLogoDomainMappings),
                Path.Combine(Folders.Logos, Properties.InvariantTexts.FileLogoServiceMappings));

            return InitializationResult.Ok;
        } // LoadBasicConfiguration

        #endregion

        #region Registry settings

        protected InitializationResult LoadRegistrySettings(string overrideBasePath)
        {
            try
            {
                var result = LoadRegistrySettingsInternal(overrideBasePath);
                if (result != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Texts.AppConfigRegistryCaption,
                        Message = string.Format(Texts.AppConfigRegistryText, result)
                    };
                }
                else
                {
                    return InitializationResult.Ok;
                } // if-else
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.AppConfigRegistryCaption,
                    Message = string.Format(Texts.AppConfigRegistryText, ex.Message),
                    InnerException = ex
                };
            } // try-catch
        } // LoadRegistrySettings

        private string LoadRegistrySettingsInternal(string overrideBasePath)
        {
            string fullKeyPath;

            using (var hkcu = Registry.CurrentUser)
            {
                fullKeyPath = InvariantTexts.RegistryKey_Root;
                using (var root = hkcu.OpenSubKey(InvariantTexts.RegistryKey_Root))
                {
                    if (root == null) return string.Format(Texts.AppConfigRegistryMissingKey, fullKeyPath);

                    var isInstalled = root.GetValue(InvariantTexts.RegistryValue_Installed);
                    if (isInstalled == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Installed);

                    var clientId = root.GetValue(InvariantTexts.RegistryValue_Analytics_ClientId) as string;
                    AnalyticsClientId = clientId;
                    if (string.IsNullOrEmpty(clientId))
                    {
                        AnalyticsClientId = Guid.NewGuid().ToString("D").ToUpperInvariant();
                        using (var writableRoot = hkcu.OpenSubKey(InvariantTexts.RegistryKey_Root, true))
                        {
                            writableRoot.SetValue(InvariantTexts.RegistryValue_Analytics_ClientId, AnalyticsClientId);
                        } // using writableRoot
                    } // if

                    fullKeyPath = InvariantTexts.RegistryKey_Root + "\\" + InvariantTexts.RegistryKey_Folders;
                    using (var folders = root.OpenSubKey(InvariantTexts.RegistryKey_Folders))
                    {
                        if (folders == null) return string.Format(Texts.AppConfigRegistryMissingKey, fullKeyPath);

                        var baseFolder = folders.GetValue(InvariantTexts.RegistryValue_Folders_Base);
                        if (baseFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folders_Base);

                        Folders.Base = overrideBasePath ?? baseFolder as string;

#if DEBUG
                        //var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                        //var installFolder = Path.GetDirectoryName(location);
                        string installFolder = null;
#else
                        var installFolder = folders.GetValue(InvariantTexts.RegistryValue_Folders_Install);
                        if (installFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folders_Install);
#endif
                        Folders.Install = installFolder as string;
                    } // using folders
                } // using root
            } // using hkcu

            return null;
        } // LoadRegistrySettingsInternal

        #endregion

        protected InitializationResult Validate()
        {
            InitializationResult result;

            result = new InitializationResult();
            result.Caption = Properties.Texts.LoadConfigValidationCaption;

            if (!Directory.Exists(Folders.Base))
            {
                result.Message = string.Format(Properties.Texts.AppConfigValidationBasePath, Folders.Base);
                return result;
            } // if

            if (!Directory.Exists(Folders.Logos))
            {
                result.Message = string.Format(Properties.Texts.AppConfigValidationLogosPath, Folders.Logos);
                return result;
            } // if

            result.IsOk = true;
            return result;
        } // Validate

        #region Content provider

        protected InitializationResult LoadContentProviderData()
        {
            var xmlPath = Path.Combine(Folders.Base, "movistartv-config.xml");

            try
            {
                var xmlContentProvider = ContentProviderData.Load(xmlPath);

                var validationResult = xmlContentProvider.Validate();
                if (validationResult != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Properties.Texts.LoadContentProviderDataValidationCaption,
                        Message = string.Format(Properties.Texts.LoadContentProviderDataValidation, xmlPath, validationResult),
                    };
                } // if

                ContentProvider = UiContentProvider.FromXmlConfiguration(xmlContentProvider, Cultures);
                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadContentProviderDataExceptionCaption,
                    Message = string.Format(Properties.Texts.LoadContentProviderDataValidation, xmlPath, Properties.Texts.LoadContentProviderDataValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadContentProviderData

        #endregion

        #region User configuration

        protected InitializationResult LoadUserConfiguration()
        {
            var xmlPath = Path.Combine(Folders.Base, "user-config.xml");

            try
            {
                // load
                User = XmlSerialization.Deserialize<UserConfig>(xmlPath, true);

                // validate
                var validationError = User.Validate();
                if (validationError != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Properties.Texts.LoadUserConfigValidationCaption,
                        Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, xmlPath, validationError),
                    };
                } // if

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadUserConfigExceptionCaption,
                    Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, xmlPath, Properties.Texts.LoadConfigUserConfigValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadUserConfiguration

        protected void ProcessXmlConfigurationItems()
        {
            Items = new Dictionary<Guid, IConfigurationItem>(User.Configuration.XmlData.Count);
            foreach (var item in User.Configuration.XmlData)
            {
                var xAttr = item.Attributes["configurationId"];
                if (xAttr != null)
                {
                    var id = new Guid(xAttr.Value);
                    // TODO: ProcessXmlConfigurationItems
                    Items[id] = null;
                } // if
            } // foreach
        } // ProcessXmlConfigurationItems

        #endregion
    } // class AppUiConfiguration
} // namespace
