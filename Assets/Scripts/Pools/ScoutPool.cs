using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Pooling;
using UnityEngine;

public class ScoutPool : Singleton<ScoutPool>
{
    public int PoolSize;
    public GameObject Prefab;
    public static ObjectPool<Scout> Pool;

    // Start is called before the first frame update
    void Start()
    {
        if (Prefab == null) return;
        Pool = new ObjectPool<Scout>(PoolSize);
        Pool.Activate();
    }
}
