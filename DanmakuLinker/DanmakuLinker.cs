using System;
using System.Collections.Generic;
using System.IO;
using DanmakuLinker.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Terraria.UI;

namespace DanmakuLinker
{
	class DanmakuLinker : Mod
	{
        public static DanmakuPlayer danmakuPlayer = new DanmakuPlayer();

		public static Random rand = new Random();

		public DanmakuLinker()
		{
		}

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(0, new LegacyGameInterfaceLayer("Danmaku UI", delegate
                {
                    danmakuPlayer.Update();
                    danmakuPlayer.RenderDanmaku(Main.spriteBatch);
                    return true;
                }, InterfaceScaleType.UI));
        }

	    public override void Load()
	    {
	        danmakuPlayer.InitPlayer();
        }

	    //public override void PreSaveAndQuit()
        //{
        //    foreach (var danmakuPlayer in Main.danmakuPlayer)
        //    {
        //        var modPlayer = danmakuPlayer.GetModPlayer<DanmakuLinkerPlayer>();
        //        if (modPlayer.isConnected)
        //        {
        //               modPlayer.prog.Disconnect();
        //            modPlayer.isConnected = false;
        //        }
        //    }
        //}
    }
}
