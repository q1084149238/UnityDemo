using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity 脚本基类 MonoBehaviour 与 GameObject 的关系 https://blog.csdn.net/hihozoo/article/details/66970467
// MonoBehaviour类 https://zhidao.baidu.com/question/2079851605275845308.html
// 雨松 http://www.xuanyusong.com/archives/category/unity/unity3d
public class Game : MonoBehaviour
{
    // 宝牌数
    public int indicator = 1;
    private void Awake()
    {
        Debug.Log("Game Awake");
        Player player = GameObject.Find("Canvas/Player").GetComponent<Player>();
        Player right = GameObject.Find("Canvas/Right").GetComponent<Player>();
        Player opposite = GameObject.Find("Canvas/Opposite").GetComponent<Player>();
        Player left = GameObject.Find("Canvas/Left").GetComponent<Player>();

        Queue<Player> queue = new Queue<Player>();

    }

    void Start()
    {
        MahjongSubject.Ins.listener.Invoke();
    }

    void Update()
    {

    }
}
