using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using ShitLib.Net;
using ShitLib.Net.Bilibili.BLiveDanmaku;
using ShitLib.Net.Bilibili.BLiveDanmaku.MessageTypes;
using Terraria;
using Terraria.ModLoader;

namespace DanmakuLinker
{
    class DanmakuLinkerPlayer : ModPlayer
    {
        public bool isConnected;
        public int roomID = -1;
        public BDanmakuGetter prog;
        
        public override void PostUpdate()
        {
            if (isConnected)
            {
                var first = prog.DanmakuList.GetFirst();
                if (first != null)
                {
                    ShowDanmaku(first);
                }
            }
        }

        private void ShowDanmaku(MessageInfo<MessageType, BMessage> info)
        {
            switch (info.MessageType)
            {
                case MessageType.Danmaku:
                    {
                        if (DanmakuLinker.danmakuPlayer.config.rollingDanmakuEnabled)
                        {
                            var bdanmaku = info.Message as BDanmaku;
                            DanmakuLinker.danmakuPlayer.bDanmakuQueue.Enqueue(bdanmaku);
                        }
                        else
                        {
                            var bdanmaku = (BDanmaku)info.Message;

                            var sb = new StringBuilder();
                            for (var i = 0; i < bdanmaku.Prefix.Length; i++)
                            {
                                if (bdanmaku.Prefix[i] != null)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            sb.Append(string.Format("[c/ff7f50:{0}]", bdanmaku.Prefix[i]));
                                            break;
                                        case 1:
                                            sb.Append(string.Format("[c/9aff02:{0}]", bdanmaku.Prefix[i]));
                                            break;
                                        case 2:
                                            var regex = new Regex(@"\d+");
                                            var match = regex.Match(bdanmaku.Prefix[i]);
                                            var level = int.Parse(match.Value);

                                            var g = 0xfa - (level / 3) * 0x12;
                                            if (g < 0x80) g = 0x80;
                                            var b = 0xf4 - (level / 3) * 0x24;
                                            if (b < 0x0) b = 0x0;

                                            sb.Append(string.Format("[c/ff{0:X2}{1:X2}:{2}]", g, b, bdanmaku.Prefix[i]));
                                            break;
                                    }
                                }
                            }

                            sb.Append(string.Format("[c/40e0d0:{0}] 说： [c/ffb6c1:{1}]", bdanmaku.Username,
                                bdanmaku.Danmaku));
                            Main.NewText(sb.ToString());
                        }

                        break;
                    }
                case MessageType.Gift:
                    {
                        var bgift = info.Message as BGift;
                        Main.NewText(string.Format("[c/40e0d0:{0}] 送出 [c/ff00ff:{1} * {2}]", bgift.Username, bgift.GiftName,
                            bgift.Amount));
                        break;
                    }
                case MessageType.EnterRoom:
                    {
                        var bwelcome = info.Message as BWelcome;
                        Main.NewText(string.Format("[c/40e0d0:{0}] 进入了直播间。", bwelcome.Username));
                        break;
                    }
                case MessageType.OnlineViewerInfo:
                    Main.NewText(info.Message.WholeMessage, Color.DarkGray, false);
                    break;
                case MessageType.Log:
                    Main.NewText(info.Message.WholeMessage);
                    break;
                case MessageType.SysMsg:
                    break;
                case MessageType.Others:
                    Main.NewText(info.Message.WholeMessage);
                    break;
                default:
                    break;
            }
        }

        
    }
}
