using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DanmakuLinker
{
    class SetRoomID :CommandTemplate
    {
        public SetRoomID()
        {
            name = "ssr";
            argstr = "roomid";
            desc = "设定弹幕房间链接号";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var player = caller.Player.GetModPlayer<DanmakuLinkerPlayer>();
            if (player.isConnected)
            {
                Main.NewText("请先关闭当前链接。", Color.Red);
                return;
            }
            if (args.Length != 1)
            {
                Main.NewText("非法参数。", Color.Red);
                return;
            }

            int rm = -1;
            if (!int.TryParse(args[0], out rm))
            {
                Main.NewText("非法参数。", Color.Red);
                return;
            }

            player.roomID = rm;
            Main.NewText(string.Format("弹幕链接房间设定为 {0}", rm));

            //DanmakuLinker.danmakuPlayer.InitPlayer();
        }
    }

    public class ToggleRollingDanmaku : CommandTemplate
    {
        public ToggleRollingDanmaku()
        {
            name = "trd";
            argstr = "";
            desc = "设置是否打开滚动弹幕";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            DanmakuLinker.danmakuPlayer.config.rollingDanmakuEnabled = !DanmakuLinker.danmakuPlayer.config.rollingDanmakuEnabled;
            if (DanmakuLinker.danmakuPlayer.config.rollingDanmakuEnabled)
            {
                Main.NewText("滚动弹幕已启用", Color.Green);
            }
            else
            {
                Main.NewText("滚动弹幕已禁用", Color.Green);
            }
            DanmakuLinker.danmakuPlayer.config.Write(Configuration.FilePath);
        }
    }

    public abstract class CommandTemplate : ModCommand
    {
        public string name, argstr, desc;

        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return name; }
        }

        public override string Usage
        {
            get { return string.Format("/{0} {1}", name, argstr); }
        }

        public override string Description
        {
            get { return string.Format("弹幕器设定: {0}", desc); }
        }
    }
}
