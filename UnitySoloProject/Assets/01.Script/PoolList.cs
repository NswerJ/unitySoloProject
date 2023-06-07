using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pool
{
    public string key;
    public int poolCount;
    public GameObject obj;
}
public class PoolList : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;

    private List<GameObject> objs = new List<GameObject>();

    public static PoolList instance;

    private void Awake()
    {
        instance = this;

        foreach (var item in pools)
        {

            for (int i = 0; i < item.poolCount; i++)
            {

                GameObject obj = Instantiate(item.obj);
                obj.name = item.key;
                obj.SetActive(false);
                objs.Add(obj);
                obj.transform.SetParent(transform);
            }

        }

    }

    public GameObject Pop(string poolName, Vector3 pos)
    {

        var obj = objs.Find(x => x.name == poolName);

        if (obj == null)
        {

            Debug.LogError("¾¾¹ß ÀÌ¸§");
            return null;

        }

        obj.SetActive(true);
        obj.transform.SetParent(null);
        obj.transform.position = pos;
        objs.Remove(obj);

        return obj;
    }

    public void Push(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        objs.Add(obj);
    }
}
