using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// https://blog.csdn.net/zhuyuqiang1238/article/details/115700070
// https://blog.csdn.net/CJsen/article/details/52487706
public class Player : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject playerDraw;
    public GameObject playerRiver;
    public GameObject prefab;

    void Start()
    {
        foreach (var item in MahjongInfo.Ins.playerHand)
        {
            Debug.Log(item.tile);
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<Image>().sprite = MahjongInfo.Ins.player[item.tile];
            obj.transform.SetParent(this.playerHand.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (MahjongInfo.Ins.t == MahjongInfo.turn.player)
        // {
        //     GameObject obj = Instantiate(prefab);
        //     obj.GetComponent<Image>().sprite = MahjongInfo.Ins.player[MahjongInfo.Ins.total[0].tile];
        //     obj.transform.SetParent(playerDraw.transform);
        //     MahjongInfo.Ins.playerHand.Add(MahjongInfo.Ins.total[0]);
        //     MahjongInfo.Ins.total.RemoveAt(0);
        //     MahjongInfo.Ins.t = MahjongInfo.turn.right;
        // }
    }
}
