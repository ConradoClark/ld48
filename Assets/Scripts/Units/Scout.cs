using System.Collections;
using System.Collections.Generic;
using Licht.Interfaces.Pooling;
using UnityEngine;

public class Scout : MonoBehaviour, IPoolableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {

    }

    public bool IsActive => enabled;
    public bool Deactivate()
    {
        gameObject.SetActive(false);
        return true;
    }

    public bool Activate()
    {
        gameObject.SetActive(true);
        return true;
    }
}
