using System.Configuration;

namespace FlightOperation.API.Helper
{
    /// <summary>
    /// Static class to do all config related operations
    /// </summary>
    public static class ConfigManager
    {

        /// <summary>
        /// Return config value as String otherwise default
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetItemAsString(string configKey, string defaultValue = null)
        {
            var configValue = ConfigurationManager.AppSettings[configKey];
            if (configValue != null)
            {
                return configValue.ToString();
            }

            return defaultValue ?? string.Empty;
        }
    }
}