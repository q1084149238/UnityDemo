using System;
using UnityEngine;

[Serializable]
public class MahjongClass
{
    // 麻将牌面
    public string tile;
    // 麻将数字
    public int tag;
    // 宝牌
    public int dora = 0;
    public int Dora
    {
        get { return dora; }
        set { dora = value; }
    }
    // 属于
    public Player belongto = null;
    public Player Belongto
    {
        get { return belongto; }
        set { belongto = value; }
    }
    public MahjongClass(string tile, int tag)
    {
        this.tile = tile;
        this.tag = tag;
    }
    public MahjongClass(string tile, int tag, int dora)
    {
        this.tile = tile;
        this.tag = tag;
        this.dora = dora;
    }
}
