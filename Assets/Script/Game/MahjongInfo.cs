using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongInfo : AttachSingleton<MahjongInfo>
{
    public Dictionary<string, Sprite> player = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> left = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> right = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> opposite = new Dictionary<string, Sprite>();
    public List<MahjongClass> total = new List<MahjongClass>();

    public List<MahjongClass> playerHand = new List<MahjongClass>();
    public List<MahjongClass> leftHand = new List<MahjongClass>();
    public List<MahjongClass> rightHand = new List<MahjongClass>();
    public List<MahjongClass> oppositeHand = new List<MahjongClass>();
    public enum turn { player, right, opposite, left }
    public turn t = turn.player;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerMahjong();
        CreateTotalMahjong();
        CreateHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 加载玩家资源
    /// </summary>
    void CreatePlayerMahjong()
    {
        Sprite[] atlas = Resources.LoadAll<Sprite>("Image/mahjong");
        foreach (var item in atlas)
        {
            player.Add(item.name, item);
        }
    }

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
                    total.Add(new MahjongClass("red_dora_man5", 5, true));
                    total.Add(new MahjongClass("red_dora_bamboo5", 15, true));
                    total.Add(new MahjongClass("red_dora_pin5", 25, true));
                    continue;
                }
                total.Add(new MahjongClass("man" + i, i, false));
                total.Add(new MahjongClass("bamboo" + i, i + 10, false));
                total.Add(new MahjongClass("pin" + i, i + 20, false));
            }
        }
        for (int i = 1; i <= 4; i++)
        {
            total.Add(new MahjongClass("wind_east", 31, false));
            total.Add(new MahjongClass("wind_south", 32, false));
            total.Add(new MahjongClass("wind_west", 33, false));
            total.Add(new MahjongClass("wind_north", 34, false));
            total.Add(new MahjongClass("dragon_white", 35, false));
            total.Add(new MahjongClass("dragon_green", 36, false));
            total.Add(new MahjongClass("dragon_red", 37, false));
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
    /// 创建初始手牌
    /// </summary>
    void CreateHand()
    {
        for (int i = 0; i < 52; i++)
        {
            if (i % 4 == 0)
            {
                playerHand.Add(total[i]);
            }
            else if (i % 4 == 1)
            {
                rightHand.Add(total[i]);
            }
            else if (i % 4 == 2)
            {
                oppositeHand.Add(total[i]);
            }
            else if (i % 4 == 3)
            {
                leftHand.Add(total[i]);
            }
            total.RemoveAt(i);
        }

        playerHand.Sort((a, b) => a.tag.CompareTo(b.tag));
        rightHand.Sort((a, b) => a.tag.CompareTo(b.tag));
        oppositeHand.Sort((a, b) => a.tag.CompareTo(b.tag));
        leftHand.Sort((a, b) => a.tag.CompareTo(b.tag));
    }

    void Draw()
    {
        playerHand.Add(total[0]);
        total.RemoveAt(0);
    }
}
