# Danmaku Linker 0.5.0

链接[b站](https://www.bilibili.com)或[斗鱼](https://www.douyu.com/)弹幕。提供2个未修改贴图的物品（以打开和关闭链接）和2个指令（设置链接房间与滚动弹幕）。

Link live danmaku for [BILIBILI](https://www.bilibili.com) or [Douyu](https://www.douyu.com/).

作者：鱼鱼（滚动弹幕部分），Rebuild（其余部分）

Credits: @AxeelAnder (for the rolling part) and @Rebuild (for the rest)

## Usage 使用方法

### 1. Command 指令

|Command|Effect|用途|
|-------|------|---|
|`/ssr [roomID]`|Set the room id to link to.|设置链接房间号|
|`/trd`|Toggle rolling danmaku effect.|开启或关闭滚动弹幕效果|

更多设置可以在`DanmakuLinkerConfig.json`中找到。

More can be found in file `DanmakuLinkerConfig.json`.

### 2. Start 启用

使用物品“B站弹幕链接器”或“斗鱼弹幕连接器”。

Use item "BLinker" (for Bilibili) or "DLinker" (for Douyu).

### 3. End 停用

再次使用物品“B站弹幕链接器”或“斗鱼弹幕连接器”。

Again, use item "BLinker".


## 注释 Comments

滚动弹幕目前仅限于Bilibili。

### Used Libraries

 - [SxxtLib](https://github.com/Shimogawa/ShitLib) is a newly initiated old project.
 - [TModLoader](https://github.com/blushiemagic/tModLoader) for the mod support for Terraria.
