using System.IO;
using System.Text.Json;
using System.Windows;
using TownBuilder.Models;

namespace TownBuilder.Helppers
{
    public class ConfigHelper
    {
        private static string configFilePath = "..\\..\\..\\..\\..\\General-Config\\config.json";
        private static ConfigModel DefaultConfig()
        {
            return new ConfigModel { Rows = 25, Columns = 45 };
        }

        internal static ConfigModel Load()
        {
            ConfigModel config = DefaultConfig();
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFilePath);
                string json = File.ReadAllText(path);
                config = JsonSerializer.Deserialize<ConfigModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? DefaultConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load configuration: {ex.Message}", "Error loading config", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return config;
        }

        internal static void Save(ConfigModel config)
        {
            try
            {
                var jsonStr = JsonSerializer.Serialize(config);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFilePath);
                File.WriteAllText("config.json", jsonStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load configuration: {ex.Message}", "Error guardar config", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}