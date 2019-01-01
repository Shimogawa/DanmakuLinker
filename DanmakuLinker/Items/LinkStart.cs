using Microsoft.Xna.Framework;
using ShitLib.Net.Bilibili.BLiveDanmaku;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DanmakuLinker.Items
{
	public class LinkStart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BLinker");
            DisplayName.AddTranslation(GameCulture.Chinese, "������");
			Tooltip.SetDefault("Start or end linking to bilibili danmaku server and recording danmaku.");
            Tooltip.AddTranslation(GameCulture.Chinese, "��ʼ����ֹ����bվ��Ļ��");
		}

		public override void SetDefaults()
		{
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = Item.sellPrice(575310, 0, 0, 0);
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	    public override bool UseItem(Player player)
	    {
	        var modPlayer = player.GetModPlayer<DanmakuLinkerPlayer>();
            if (modPlayer.roomID < 0)
            {
                Main.NewText("�趨����ȷ����ʹ��/help�鿴�趨��", Color.Red);
                return true;
            }

            if (!modPlayer.isConnected)
	        {
                Main.NewText("��ʼ���ӵ�Ļ...");
                modPlayer.prog = new BDanmakuGetter(modPlayer.roomID);
                modPlayer.prog.StartLoop();
            }
            else
            {
                Main.NewText("������ֹ������Ա���");
                modPlayer.prog.Disconnect();
            }
            modPlayer.isConnected = !modPlayer.isConnected;

	        return true;
	    }
	}
}
