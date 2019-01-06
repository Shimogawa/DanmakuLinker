using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using ShitLib.Net;
using ShitLib.Net.Bilibili.BLiveDanmaku;
using ShitLib.Net.Bilibili.BLiveDanmaku.MessageTypes;
using ShitLib.Net.Douyu;
using ShitLib.Net.Douyu.MessageTypes;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DanmakuLinker
{
	class DanmakuLinkerPlayer : ModPlayer
	{
		public bool isConnected = false;
		public int roomID = -1;
		public IDanmakuGetter prog = null;

		public PlatformEnum Platform { get; set; }

		public override void Load(TagCompound tag)
		{
			Platform = PlatformEnum.None;
		}

		public override void PostUpdate()
		{
			if (Platform == PlatformEnum.None)
				return;
			switch (Platform)
			{
				case PlatformEnum.Bilibili:
					ShowB_internal();
					break;
				case PlatformEnum.Douyu:
					ShowD_internal();
					break;
				default:
					break;
			}
		}

		private void ShowD_internal()
		{
			if (isConnected)
			{
				if (!prog.IsConnected)
				{
					RestartDLinker();
				}
				var p = prog as DDanmakuGetter;
				var first = p.DanmakuList.GetFirst();
				if (first != null)
				{
					ShowDDanmaku(first);
				}
			}
		}

		private void RestartDLinker()
		{
			prog.Connect();
		}

		private void ShowB_internal()
		{
			if (isConnected)
			{
				var p = prog as BDanmakuGetter;
				var first = p.DanmakuList.GetFirst();
				if (first != null)
				{
					ShowBDanmaku(first);
				}
			}
		}

		private void ShowDDanmaku(MessageInfo<MessageType, DMessage> info)
		{
			switch (info.MessageType)
			{
				case MessageType.Log:
					Main.NewText(info.Message.WholeMessage);
					break;
				case MessageType.Danmaku:
					var ddanmaku = info.Message as DDanmaku;
					var color = DDanmaku.COLOR_CODE[ddanmaku.Color];
					var sb = new StringBuilder();
					if (ddanmaku.User.IsSuperModerator) sb.Append("[c/00ff7f:【超管】]");
					if (ddanmaku.User.IsRoomModerator) sb.Append("[c/ffd700:【房管】]");
					if (ddanmaku.User.NobelLevel.HasValue && ddanmaku.User.NobelLevel.Value <= 6)
						sb.Append(string.Format(
							"[c/50d2a5:【{0}】]", DUser.NOBEL_LEVEL_NAME[ddanmaku.User.NobelLevel.Value]
						));

					if (ddanmaku.User.Badge != null && ddanmaku.User.Badge.BadgeLevel != 0)
						sb.Append(string.Format(
							"[c/9aff02:【{0} · {1}】]",
							ddanmaku.User.Badge.BadgeName, ddanmaku.User.Badge.BadgeLevel
						));

					var level = ddanmaku.User.Level;
					var g = 0xfa - (level / 3) * 0x12;
					if (g < 0x80) g = 0x80;
					var b = 0xf4 - (level / 3) * 0x24;
					if (b < 0x0) b = 0x0;
					if (ddanmaku.Color != 0)
					{
						sb.Append(string.Format(
							"[c/ff{0:X2}{1:X2}:【Lv.{2}】][c/40e0d0:{3}] 说：[c/{4}:{5}]",
							g, b, level, ddanmaku.User.Username, color, ddanmaku.Danmaku
						));
					}
					else
					{
						sb.Append(string.Format(
							"[c/ff{0:X2}{1:X2}:【Lv.{2}】][c/40e0d0:{3}] 说：{4}",
							g, b, level, ddanmaku.User.Username, ddanmaku.Danmaku
						));
					}
					Main.NewText(sb.ToString());
					break;
				case MessageType.EnterRoom:
					var dwelcome = info.Message as DWelcome;
					sb = new StringBuilder();
					if (dwelcome.User.IsSuperModerator) sb.Append("[c/00ff7f:【超管】]");
					if (dwelcome.User.IsRoomModerator) sb.Append("[c/ffd700:【房管】]");
					if (dwelcome.User.NobelLevel.HasValue && dwelcome.User.NobelLevel.Value <= 6)
						sb.Append(string.Format(
							"[c/50d2a5:【{0}】]", DUser.NOBEL_LEVEL_NAME[dwelcome.User.NobelLevel.Value]
						));
					sb.Append(string.Format("[c/40e0d0:{0}] 进入了直播间。", dwelcome.Username));
					Main.NewText(sb.ToString());
					break;
				case MessageType.Gift:
					var dgift = info.Message as DGift;
					Main.NewText(string.Format("[c/40e0d0:{0}] 送出 [c/ff00ff:{1} * {2}]（[c/b07c2f:{3}连击！]）",
						dgift.User.Username, dgift.GiftName, dgift.Amount, dgift.Combo));
					break;
			}
		}

		private void ShowBDanmaku(MessageInfo<MessageType, BMessage> info)
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
							if (bdanmaku.User.IsAdmin)
								sb.Append("[c/ff7f50:【房管】]");
							if (bdanmaku.User.IsSVIP)
								sb.Append(string.Format("[c/9aff02:【{0}】]", BUser.SVIP));
							if (bdanmaku.User.Badge != null)
							{
								var level = bdanmaku.User.Badge.BadgeLevel;
								var g = 0xfa - (level / 3) * 0x12;
								if (g < 0x80) g = 0x80;
								var b = 0xf4 - (level / 3) * 0x24;
								if (b < 0x0) b = 0x0;
								sb.Append(string.Format("[c/ff{0:x2}{1:x2}:【{2} {3}】]", g, b, bdanmaku.User.Badge.BadgeName, level));
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
