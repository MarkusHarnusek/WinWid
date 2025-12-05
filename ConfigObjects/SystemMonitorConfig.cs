using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using WinWid.Config;

namespace WinWid.ConfigObjects
{
    public enum SystemMonitorPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public class SystemMonitorConfig
    {
        public bool Enable { get; set; } = true;
        public SystemMonitorPosition Position { get; set; } = SystemMonitorPosition.BottomLeft;
        public int Margin { get; set; } = 5;
        public bool ShowCPU { get; set; } = true;
        public bool ShowMemory { get; set; } = true;
        public bool ShowNetwork { get; set; } = true;
        public bool ShowDisk { get; set; } = true;
        public double Opacity { get; set; } = 1;
        public string AppDataConfigDir { get; private set; }

#pragma warning disable CS8618
        [JsonConstructor]
        public SystemMonitorConfig() { }
#pragma warning restore CS8618

        public SystemMonitorConfig(string appDataConfigDir)
        {
            AppDataConfigDir = appDataConfigDir;
            Load();
        }

        public void Load()
        {
            string path = Path.Combine(AppDataConfigDir, "system_monitor.json");

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    SystemMonitorConfig? config = JsonSerializer.Deserialize<SystemMonitorConfig>(json);

                    if (config != null)
                    {
                        Position = config.Position;
                        Margin = config.Margin;
                        ShowCPU = config.ShowCPU;
                        ShowMemory = config.ShowMemory;
                        ShowNetwork = config.ShowNetwork;
                        ShowDisk = config.ShowDisk;
                        Opacity = config.Opacity;
                    }
                }
                catch
                {
                    Save();
                }
            }
            else
            {
                Save();
            }
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            string path = Path.Combine(AppDataConfigDir, "system_monitor.json");
            File.WriteAllText(path, json);
        }
    }
}