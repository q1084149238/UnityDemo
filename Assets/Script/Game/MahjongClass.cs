public class MahjongClass
{
    // 麻将牌面
    public string tile;
    // 麻将数字
    public int tag;
    // 宝牌
    public bool dora;

    public MahjongClass(string tile, int tag, bool dora)
    {
        this.tile = tile;
        this.tag = tag;
        this.dora = dora;
    }
}
