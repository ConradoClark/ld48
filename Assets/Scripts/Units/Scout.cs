using System.Collections;
using System.Collections.Generic;
using Licht.Interfaces.Pooling;
using UnityEngine;

public class Scout : MonoBehaviour, IPoolableObject
{
    public void Initialize()
    {
    }

    public bool IsActive => gameObject.activeSelf;
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
