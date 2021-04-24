using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Pooling;
using UnityEngine;

public class ScoutPool : MonoBehaviour
{
    public int PoolSize;
    public GameObject Prefab;
    public ObjectPool<Scout> Pool;

    // Start is called before the first frame update
    void Start()
    {
        if (Prefab == null) return;
        Pool = new ObjectPool<Scout>(PoolSize, index =>
        {
            var prefab = Instantiate(Prefab,transform);
            prefab.name = $"{name}-{nameof(Scout)}-{index+1}";
            prefab.SetActive(false);
            return prefab.GetComponent<Scout>();
        });
        Pool.Activate();
    }
}
