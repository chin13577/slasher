using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public List<IUpdateable> updateObjs = new List<IUpdateable>();

    public void Initialize()
    {
    }

    public void AddListener(IUpdateable obj)
    {
        if (!updateObjs.Contains(obj))
            updateObjs.Add(obj);
    }

    public void RemoveListener(IUpdateable obj)
    {
        updateObjs.Remove(obj);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < updateObjs.Count; i++)
        {
            updateObjs[i].OnFixedUpdate();
        }
    }

    private void Update()
    {
        for (int i = 0; i < updateObjs.Count; i++)
        {
            updateObjs[i].OnUpdate();
        }
    }

}
