using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public struct ObjectPool
    {
        public int id;
        public int startSpawnCount;
        public GameObject prefab;
    }

    public ObjectPool[] poolPrefabs;
    private Dictionary<int, List<GameObject>> poolDict = new Dictionary<int, List<GameObject>>();

    public void Initialize()
    {
        InitialPoolDictionary();
        CreateStartAmountPoolObject();
    }

    private void InitialPoolDictionary()
    {
        for (int i = 0; i < poolPrefabs.Length; i++)
        {
            if (!poolDict.ContainsKey(i))
                poolDict.Add(i, new List<GameObject>());
        }
    }

    private void CreateStartAmountPoolObject()
    {
        for (int i = 0; i < poolPrefabs.Length; i++)
        {
            for (int j = 0; j < poolPrefabs[i].startSpawnCount; j++)
            {
                var obj = CreateObject(poolPrefabs[i].id);
                poolDict[i].Add(obj);
            }
        }
    }

    public GameObject GetObject(int key)
    {
        var result = poolDict[key].Find(obj => obj.activeInHierarchy == false); 
        if (result == null)
        {
            result = CreateObject(key);
            poolDict[key].Add(result);
        }
        return result;
    }

    private GameObject CreateObject(int key)
    {
        var obj = Instantiate(poolPrefabs[key].prefab, this.transform);
        obj.SetActive(false);
        return obj;
    }
}
