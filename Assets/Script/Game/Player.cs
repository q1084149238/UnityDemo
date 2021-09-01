using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Config.DealersWind wind;
    public bool isPlayer = false;
    public GameObject hand;
    public GameObject draw;
    public GameObject river;
    public GameObject prefab;
    public List<MahjongClass> handList = new List<MahjongClass>();
    private List<MahjongClass> riverList = new List<MahjongClass>();

    private void OnEnable()
    {
        MahjongSubject.Ins.listener.AddListener(Draw);
    }
    private void OnDisable()
    {
        MahjongSubject.Ins.listener.RemoveListener(Draw);
    }

    private void Awake()
    {
        Debug.Log(wind + "Awake");
        handList = MahjongInfo.Ins.CreateHand(this);
        foreach (var item in handList)
        {
            //GameObject obj = ObjectPool.Ins.Get();
            GameObject obj = Instantiate(prefab);
            // 待改
            if (isPlayer)
            {
                obj.GetComponent<Image>().sprite = MahjongInfo.Ins.hand.GetSprite(item.tile);
                if (item.Dora != 0)
                {
                    obj.GetComponent<Image>().material = Resources.Load<Material>("Effect/GameEffect_Dora");
                }
            }
            obj.transform.SetParent(hand.transform);
        }
    }

    public void Draw()
    {

    }
}
