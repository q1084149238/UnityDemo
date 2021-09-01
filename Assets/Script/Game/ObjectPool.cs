using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://blog.csdn.net/qq_44195770/article/details/114067202
public class ObjectPool : AttachSingleton<ObjectPool>
{
    public GameObject prefab;
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    public int defaultCount = 13;
    public int maxCount = 14;

    protected override void Awake()
    {
        base.Awake();
        GameObject obj;
        for (int i = 0; i < defaultCount; i++)
        {
            obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        GameObject obj;
        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(prefab);
        }
        return obj;
    }

    public void Put(GameObject obj)
    {
        if (objectPool.Count <= maxCount)
        {
            if (!objectPool.Contains(obj))
            {
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        else
        {
            Destroy(obj);
        }
    }
}
