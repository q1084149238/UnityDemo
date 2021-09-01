using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


// SpriteAtlas与AssetBundle最佳食用方案 https://www.cnblogs.com/msxh/p/14194756.html
// Sprite Atlas功能讲解 https://blog.csdn.net/qq_39574690/article/details/104413284
// 日本麻将规则 Japanese Mahjong - Riichi https://www.docin.com/p-71577600.html
// 麻将基本术语中英文对照表 https://www.xqbase.com/other/mahjongg_english.htm
// 日麻词典 https://bgm.tv/blog/269374
public class MahjongInfo : AttachSingleton<MahjongInfo>
{
    //public Dictionary<string, Sprite> player = new Dictionary<string, Sprite>();
    public SpriteAtlas hand;
    public SpriteAtlas player;
    public SpriteAtlas left;
    public SpriteAtlas right;
    public SpriteAtlas opposite;
    public SpriteAtlas empty;

    public List<MahjongClass> total = new List<MahjongClass>();
    // 摸到第几张
    public int index = 0;
    // 岭上牌
    public List<MahjongClass> kanDora = new List<MahjongClass>();
    // 宝牌
    public List<MahjongClass> dora = new List<MahjongClass>();
    // 里宝牌
    public List<MahjongClass> uraDora = new List<MahjongClass>();

    protected override void Awake()
    {
        base.Awake();
        // CreatePlayerMahjong();
        CreateTotalMahjong();
        CreateDeadWall();
        //CreateHand();
    }

    /// <summary>
    /// 加载图集资源
    /// </summary>
    // void CreatePlayerMahjong()
    // {
    //     Sprite[] atlas = Resources.LoadAll<Sprite>("Image/mahjong");
    //     foreach (var mahjong in atlas)
    //     {
    //         player.Add(mahjong.name, mahjong);
    //     }
    // }

    /// <summary>
    /// 定义一副麻将
    /// </summary>
    void CreateTotalMahjong()
    {
        for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                if (i == 5 && j == 4)
                {
                    total.Add(new MahjongClass("red_m5", 5, 1));
                    total.Add(new MahjongClass("red_p5", 15, 1));
                    total.Add(new MahjongClass("red_s5", 25, 1));
                    continue;
                }
                total.Add(new MahjongClass("m" + i, i));
                total.Add(new MahjongClass("p" + i, i + 10));
                total.Add(new MahjongClass("s" + i, i + 20));
            }
        }
        for (int i = 1; i <= 7; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                total.Add(new MahjongClass("z" + i, 30 + i));
            }
        }
        Shuffle();
    }

    /// <summary>
    /// 洗牌算法
    /// </summary>
    void Shuffle()
    {
        for (int i = 0; i < total.Count; i++)
        {
            int r = Random.Range(i, total.Count);
            MahjongClass temp = total[i];
            total[i] = total[r];
            total[r] = temp;
        }
    }

    /// <summary>
    /// 创建王牌
    /// </summary>
    void CreateDeadWall()
    {
        for (int i = 0; i < 4; i++)
        {
            kanDora.Add(total[total.Count - 1]);
            total.RemoveAt(total.Count - 1);
        }
        for (int i = 0; i < 5; i++)
        {
            dora.Add(total[total.Count - 1]);
            total.RemoveAt(total.Count - 1);
        }
        // 待改
        for (int i = 0; i < total.Count; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                if (total[i].tag == (dora[j].tag + 1))
                {
                    total[i].Dora += 1;
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            uraDora.Add(total[total.Count - 1]);
            total.RemoveAt(total.Count - 1);
        }
    }

    /// <summary>
    /// 创建初始手牌
    /// </summary>
    public List<MahjongClass> CreateHand(Player player)
    {
        lock (total)
        {
            int end = index + 13;
            List<MahjongClass> list = new List<MahjongClass>();
            for (; index < end; index++)
            {
                total[index].Belongto = player;
                list.Add(total[index]);
            }
            list.Sort((a, b) => a.tag.CompareTo(b.tag));
            return list;
        }
    }
}


