# Danmaku Linker 0.5.0

链接[b站](https://www.bilibili.com)弹幕。只有一个未修改贴图的物品（以打开和关闭链接）和一个指令（设置链接房间）。

Link ONLY for [BILIBILI](https://www.bilibili.com) danmaku live. ONLY 1 single item for opening and closing the stream.

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

使用物品“链接器”。

Use item "BLinker".

### 3. End 停用

再次使用物品“链接器”。

Again, use item "BLinker".


## 注释 Comments

目前仅支持b站弹幕，虽然斗鱼接口已经完工，但是懒得做，鸽几天。

### Used Libraries

 - [SxxtLib](https://github.com/Shimogawa/ShitLib) is a newly initiated old project.
 - [TModLoader](https://github.com/blushiemagic/tModLoader) for the mod support for Terraria.
