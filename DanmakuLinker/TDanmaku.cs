using Microsoft.Xna.Framework;
using Terraria;
using ReLogic;
using Microsoft.Xna.Framework.Graphics;
using ShitLib.Net.BLiveDanmaku.MessageTypes;

namespace DanmakuLinker
{
    public class TDanmaku
    {
        public const int FONT_SIZE_BASE = 20;
        public static float fontSize;

        public BDanmaku danmaku;

        public int lineNumber;

        public Vector2 position;

        public float width;

        public TDanmaku(BDanmaku bDanmaku, int lineNumber)
        {
            danmaku = bDanmaku;

            this.lineNumber = lineNumber;
            width = Main.fontMouseText.MeasureString(danmaku.Danmaku).X * fontSize;

            position = new Vector2(Main.screenWidth, 
                lineNumber * (DanmakuLinker.danmakuPlayer.config.lineSpace + FONT_SIZE_BASE * fontSize));

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, 
            //    danmaku.Danmaku, position.X, position.Y,
            //    Color.White, Color.Black, Vector2.Zero, 
            //    // 如果实装字体大小，这里改成字体大小
            //    fontSize);

            //Utils.DrawBorderString(spriteBatch, danmaku.Danmaku, position, Color.White, fontSize);

            if (fontSize > 1f)
                Utils.DrawBorderString(spriteBatch, danmaku.Danmaku, position, Color.White, fontSize);
            else
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText,
                    danmaku.Danmaku, position.X, position.Y,
                    Color.White, Color.Black, Vector2.Zero, 1f);
        }
    }
}
