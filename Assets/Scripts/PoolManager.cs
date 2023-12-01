using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public GameObject[] objs;
    public List<GameObject>[] objPool;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;

            objPool = new List<GameObject>[objs.Length];

            for (int i = 0; i < objs.Length; i++)
            {
                objPool[i] = new List<GameObject>();
            }
        }
    }

    public GameObject GetObj(int id)
    {
        GameObject obj = null;
        foreach (GameObject i in objPool[id])
        {
            if (!i.activeSelf)
            {
                obj = i;
                obj.SetActive(true);
                break;
            }
        }
        if(!obj)
        {
            obj = Instantiate(objs[id], transform.position, Quaternion.identity);
            objPool[id].Add(obj);
        }
        return obj;
    }
}
