using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Licht.Impl.Orchestration;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Depth;
    void Start()
    {
        
    }

    IEnumerable<Action> Spawn()
    {
        yield return TimeYields.WaitOneFrame;
    }

}
