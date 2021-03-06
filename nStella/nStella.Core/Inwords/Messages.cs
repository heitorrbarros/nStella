﻿using nStella.Core.Inwords.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace nStella.Core.Inwords
{
    public sealed class Messages
    {
        private static readonly string BUNDLE_NAME = "nStella.Core.Inwords.Resources.Messages";
        public static readonly CultureInfo LOCALE_PT_BR = new CultureInfo("pt-BR");
        private static readonly IDictionary<string, ResourceManager> RESOURCE_BUNDLES;

        static Messages()
        {
            Dictionary<string, ResourceManager> resourcesByLocale = new Dictionary<string, ResourceManager>(2);            
            resourcesByLocale.Add(LOCALE_PT_BR.Name, new ResourceManager(typeof(messages_pt_BR)));
            resourcesByLocale.Add(CultureInfo.CreateSpecificCulture("en").Name, new ResourceManager(typeof(messages_en)));

            RESOURCE_BUNDLES = new ReadOnlyDictionary<string, ResourceManager>(resourcesByLocale);
        }
        private Messages()
        {
        }

        public static string GetString(string key)
        {
            return RESOURCE_BUNDLES[key].GetString(key);
        }

        public static string GetString(string key, CultureInfo cultureInfo)
        {
            ResourceManager resourceManager = RESOURCE_BUNDLES[cultureInfo.Name];

            if (resourceManager == null)
            {
                throw new NotSupportedException("Não é possivel converter números para o idioma " + cultureInfo.DisplayName);
            }

            string result = resourceManager.GetString(key, cultureInfo);

            if (result == null)
                throw new MissingManifestResourceException();   
                 
            return result;
        }
    }
}
