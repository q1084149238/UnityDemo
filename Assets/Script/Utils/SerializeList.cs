using System;
using System.Collections.Generic;
using UnityEngine;

// https://blog.csdn.net/xlc_137/article/details/105477393
public class SerializeList
{
    public static string ListToJson<T>(List<T> target)
    {
        return JsonUtility.ToJson(new SerializationList<T>(target));
    }
}

[Serializable]
public class SerializationList<T>
{
    [SerializeField]
    List<T> target;

    public SerializationList(List<T> target)
    {
        this.target = target;
    }
}
