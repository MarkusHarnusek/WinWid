using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WinWid.Config
{
    public class ClockConfig
    {
        public bool Enable {  get; set; } = true;
        public int WidthRatio { get; set; } = 3;
        public int HeightRatio { get; set; } = 3;
        public int TopOffsetRatio { get; set; } = 20;
        public bool Use24H { get; set; } = true;
        public bool ShowSeconds { get; set; } = true;
        public bool ShowDate { get; set; } = true;
        public double Opacity { get; set; } = 1;
        public string AppDataConfigDir { get; private set; }

#pragma warning disable CS8618
        [JsonConstructor]
        public ClockConfig() { }
#pragma warning restore CS8618

        public ClockConfig(string appDataConfigDir)
        {
            AppDataConfigDir = appDataConfigDir;
            Load();
        }

        public void Load()
        {
            string path = Path.Combine(AppDataConfigDir, "clock.json");

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    ClockConfig? config = JsonSerializer.Deserialize<ClockConfig>(json);

                    if (config != null)
                    {
                        Enable = config.Enable;
                        WidthRatio = config.WidthRatio;
                        HeightRatio = config.HeightRatio;
                        TopOffsetRatio = config.TopOffsetRatio;
                        Use24H = config.Use24H;
                        ShowSeconds = config.ShowSeconds;
                        ShowDate = config.ShowDate;
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

            string path = Path.Combine(AppDataConfigDir, "clock.json");
            File.WriteAllText(path, json);
        }
    }
}
