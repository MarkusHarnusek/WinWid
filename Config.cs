using System.IO;

namespace WinWid.Config
{
    public class Config
    {
        public ClockConfig ClockConfig { get; set; }
        public SystemMonitorConfig SystemMonitorConfig { get; set; }
        public string AppDataConfigDir { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WinWid", "config");

#pragma warning disable CS8618
        public Config() 
        {
#pragma warning restore CS8618
            LoadConfig();
        }

        public void LoadConfig()
        {
            if (!Directory.Exists(AppDataConfigDir))
            {
                Directory.CreateDirectory(AppDataConfigDir);
            }

            ClockConfig = new ClockConfig(AppDataConfigDir);
            SystemMonitorConfig = new SystemMonitorConfig(AppDataConfigDir);
        }

        public void SaveConfig()
        {
            ClockConfig.Save();
            SystemMonitorConfig.Save();
        }
    }
}
