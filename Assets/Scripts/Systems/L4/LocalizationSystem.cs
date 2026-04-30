using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.L4
{
    public class LocalizationSystem : GameSystem
    {
        private const string kDefaultLocale = "zh-CN";
        private const string kSavedLocaleKey = "L4.Localization.Locale";

        [Header("L4 Localization")]
        public LocalizationTableConfig LocalizationTable;
        public bool AutoLoadSavedLocale = true;
        public bool FallbackToEnglish = true;

        private readonly Dictionary<string, Dictionary<string, string>> _localeTables =
            new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        private string _currentLocale = kDefaultLocale;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            RebuildTables();

            string targetLocale = kDefaultLocale;
            if (LocalizationTable != null && !string.IsNullOrWhiteSpace(LocalizationTable.DefaultLocale))
            {
                targetLocale = LocalizationTable.DefaultLocale.Trim();
            }

            if (AutoLoadSavedLocale && PlayerPrefs.HasKey(kSavedLocaleKey))
            {
                string saved = PlayerPrefs.GetString(kSavedLocaleKey, targetLocale);
                if (!string.IsNullOrWhiteSpace(saved))
                {
                    targetLocale = saved.Trim();
                }
            }

            if (!SetLocale(targetLocale))
            {
                SetLocale(kDefaultLocale);
            }
        }

        public string GetCurrentLocale()
        {
            return _currentLocale;
        }

        public string[] GetSupportedLocales()
        {
            if (LocalizationTable != null && LocalizationTable.SupportedLocales != null && LocalizationTable.SupportedLocales.Length > 0)
            {
                var locales = new List<string>();
                for (int i = 0; i < LocalizationTable.SupportedLocales.Length; i++)
                {
                    string locale = LocalizationTable.SupportedLocales[i];
                    if (!string.IsNullOrWhiteSpace(locale))
                    {
                        locales.Add(locale.Trim());
                    }
                }

                return locales.ToArray();
            }

            return new[] { "zh-CN", "zh-TW", "en-US" };
        }

        public bool SetLocale(string localeCode)
        {
            if (string.IsNullOrWhiteSpace(localeCode))
            {
                return false;
            }

            string normalized = localeCode.Trim();
            if (!IsSupportedLocale(normalized))
            {
                return false;
            }

            _currentLocale = normalized;
            PlayerPrefs.SetString(kSavedLocaleKey, _currentLocale);
            PlayerPrefs.Save();

            Events.NotificationAdded("L4", $"Locale switched: {_currentLocale}", FeedbackSeverity.Info);
            Events.ActionLogAdded("L4", $"Localization locale updated to {_currentLocale}", FeedbackSeverity.Info);
            return true;
        }

        public string Translate(string key, string fallback = "")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return fallback ?? string.Empty;
            }

            if (_localeTables.TryGetValue(_currentLocale, out Dictionary<string, string> table)
                && table.TryGetValue(key, out string localized)
                && !string.IsNullOrWhiteSpace(localized))
            {
                return localized;
            }

            if (FallbackToEnglish
                && _localeTables.TryGetValue("en-US", out Dictionary<string, string> enTable)
                && enTable.TryGetValue(key, out string english)
                && !string.IsNullOrWhiteSpace(english))
            {
                return english;
            }

            return string.IsNullOrWhiteSpace(fallback) ? key : fallback;
        }

        public bool TryTranslate(string key, out string localized)
        {
            localized = Translate(key, string.Empty);
            return !string.IsNullOrWhiteSpace(localized);
        }

        public string FormatNumber(double value)
        {
            CultureInfo culture = ResolveCulture(_currentLocale);
            return value.ToString("N0", culture);
        }

        public string FormatCurrency(double value, string currencyCode = "USD")
        {
            CultureInfo culture = ResolveCulture(_currentLocale);
            string symbol = ResolveCurrencySymbol(currencyCode);
            return string.Format(culture, "{0}{1:N0}", symbol, value);
        }

        public string FormatDate(DateTime date)
        {
            CultureInfo culture = ResolveCulture(_currentLocale);
            return date.ToString("d", culture);
        }

        public void RebuildTables()
        {
            _localeTables.Clear();
            string[] locales = GetSupportedLocales();
            for (int i = 0; i < locales.Length; i++)
            {
                if (!_localeTables.ContainsKey(locales[i]))
                {
                    _localeTables.Add(locales[i], new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
                }
            }

            if (LocalizationTable == null || LocalizationTable.Entries == null)
            {
                return;
            }

            for (int i = 0; i < LocalizationTable.Entries.Length; i++)
            {
                LocalizedTextEntry entry = LocalizationTable.Entries[i];
                if (entry == null || string.IsNullOrWhiteSpace(entry.Key))
                {
                    continue;
                }

                for (int j = 0; j < locales.Length; j++)
                {
                    string locale = locales[j];
                    if (!_localeTables.TryGetValue(locale, out Dictionary<string, string> localeMap))
                    {
                        continue;
                    }

                    string value = LocalizationTable.ResolveText(entry, locale);
                    if (!localeMap.ContainsKey(entry.Key))
                    {
                        localeMap.Add(entry.Key, value ?? string.Empty);
                    }
                    else
                    {
                        localeMap[entry.Key] = value ?? string.Empty;
                    }
                }
            }
        }

        private bool IsSupportedLocale(string localeCode)
        {
            string[] supported = GetSupportedLocales();
            for (int i = 0; i < supported.Length; i++)
            {
                if (string.Equals(supported[i], localeCode, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static CultureInfo ResolveCulture(string locale)
        {
            try
            {
                return CultureInfo.GetCultureInfo(locale);
            }
            catch
            {
                return CultureInfo.InvariantCulture;
            }
        }

        private static string ResolveCurrencySymbol(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                return "$";
            }

            switch (currencyCode.Trim().ToUpperInvariant())
            {
                case "CNY":
                    return "\u00A5";
                case "EUR":
                    return "\u20AC";
                case "RUB":
                    return "\u20BD";
                case "IRR":
                    return "IRR ";
                default:
                    return "$";
            }
        }
    }
}
