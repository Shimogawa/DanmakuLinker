using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShitLib.Net.BLiveDanmaku.MessageTypes;
using Terraria;

namespace DanmakuLinker
{
    public class DanmakuPlayer
    {
        // 配置
        public Configuration config;
        // 普通的滚动类型弹幕的队列
        public Queue<BDanmaku> bDanmakuQueue = new Queue<BDanmaku>();
        // 显示用的弹幕池
        // TODO: 采用更加装逼但是没有什么卵用的池结构
        List<TDanmaku> tDanmakuPool = new List<TDanmaku>();
        // 用于判断某行是否处于空闲状态(可以发送新的弹幕)
        bool[] linesIdleState;
        // 记录行速度，使弹幕能错开
        float[] linesVelocity;


        /// <summary>
        /// 初始化弹幕播放器
        /// </summary>
        public void InitPlayer()
        {
            // 初始化配置
            config = Configuration.Read(Configuration.FilePath);
            TDanmaku.fontSize = config.danmakuFontSize;

            // 初始化并将初始值设置为true
            linesIdleState = new bool[config.danmakuMaxLineCount];
            for (int i = 0; i < linesIdleState.Length; i++)
                linesIdleState[i] = true;
            // 初始化速度
            linesVelocity = new float[config.danmakuMaxLineCount];
            for (int i = 0; i < linesVelocity.Length; i++)
                linesVelocity[i] = config.danmakuVelocity + Main.rand.NextFloat(config.velocityDeltaMax);

            bDanmakuQueue = new Queue<BDanmaku>();
            tDanmakuPool = new List<TDanmaku>();
        
        }

        public void Update()
        {
            try
            {
                // 发射新弹幕
                while (bDanmakuQueue.Count > 0)
                {
                    // 检测弹幕数量是否过多
                    if(tDanmakuPool.Count >= config.danmakuMaxCount)
                    {
                        break;
                    }
                    // 检查是否有空闲行可用
                    for (int i = 0; i < linesIdleState.Length; i++)
                    {
                        if (linesIdleState[i])
                        {
                            var tDanmaku = new TDanmaku(bDanmakuQueue.Dequeue(), i);

                            tDanmakuPool.Add(tDanmaku);
                            linesIdleState[i] = false;

                            break;
                        }
                    }

                    // 如果没有可用行，就break
                    break;
                }

                // 更新行状态信息，写的十分沙雕
                var remove = new List<TDanmaku>();
                foreach (var td in tDanmakuPool)
                {
                    if (td.position.X + td.width + config.danmakuSpace < Main.screenWidth)
                    {
                        linesIdleState[td.lineNumber] = true;
                    }
                    else
                    {
                        // 感觉加上会更保险一点
                        linesIdleState[td.lineNumber] = false;
                    }

                    if (td.position.X + td.width < 0)
                    {
                        remove.Add(td);
                    }
                }
                tDanmakuPool.RemoveAll(t => remove.Contains(t));
            }
            catch(System.Exception ex)
            {
            }
        }

        public void RenderDanmaku(SpriteBatch spriteBatch)
        {
            try
            {
                // 绘制弹幕
                for (int i = 0; i < tDanmakuPool.Count; i++)
                {
                    var td = tDanmakuPool[i];
                    td.Draw(spriteBatch);
                    td.position -= new Vector2(linesVelocity[td.lineNumber], 0);
                }
            }
            catch(System.Exception ex)
            {
            }
        }
    }
}
