using System.IO;
using Terraria;
using Newtonsoft.Json;

namespace DanmakuLinker
{
    public class Configuration
    {
        public static readonly string FilePath = Path.Combine(Main.SavePath, "DanmakuLinkerConfig.json");

        [JsonProperty("是否启用滚动弹幕")]
        public bool rollingDanmakuEnabled = true;
        [JsonProperty("弹幕滚动速度")]
        public float danmakuVelocity = 2f;
        [JsonProperty("弹幕最大数量")]
        public int danmakuMaxCount = 500;
        [JsonProperty("弹幕最大占据行数")]
        public int danmakuMaxLineCount = 15;
        [JsonProperty("弹幕间距")]
        public int danmakuSpace = 2;
        [JsonProperty("行间距")]
        public int lineSpace = 2;
        [JsonProperty("弹幕行速度差最大值")]
        public float velocityDeltaMax = 0.4f;
        [JsonProperty("弹幕文字大小")]
        public float danmakuFontSize = 1.5f;

        public static Configuration Read(string path)
        {
            if (!File.Exists(path))
            {
                var config = new Configuration();
                config.Write(FilePath);

                return config;
            }
            Configuration result;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    result = JsonConvert.DeserializeObject<Configuration>(streamReader.ReadToEnd());
                }
            }
            return result;
        }
        
        public void Write(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                string value = JsonConvert.SerializeObject(this, Formatting.Indented);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(value);
                }
            }
        }
        
    }
}