using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// C# Event/UnityEvent辨析 https://blog.csdn.net/qq_28849871/article/details/78366236
// 观察者模式 https://zhuanlan.zhihu.com/p/39096076
// Unity（十一） Action UnityAction与event UnityEvent https://blog.csdn.net/LinZhonglong/article/details/81131097
// 设计模式学习笔记六:观察者模式 https://www.cnblogs.com/movin2333/p/15121373.html
public class MahjongSubject : Singleton<MahjongSubject>
{
    // 当前出牌，牌山，各家牌河，宝牌，点数，最后出的牌
    //public enum state { player = 0, right = 1, opposite = 2, left = 3 }
    public UnityEvent listener = new UnityEvent();
    public Config.Turn turn = Config.Turn.East;
}
