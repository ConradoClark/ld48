using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Impl.Pooling;
using UnityEngine;

public class BuildScout : MonoBehaviour
{
    public Command Command;
    public BuildQueue BuildQueue;
    public Transform SpawnPoint;
    public float BuildTimeInSeconds;
    public ScoutPool ObjectPool;

    // Start is called before the first frame update
    void OnEnable()
    {
        Command.CommandAction = Build;    
    }

    private void Build()
    {
        if (!Toolbox.Instance.ResourceManager.Consume(1, 0, 1)) return;
        var jobNumber = BuildQueue.AddJob(BuildTimeInSeconds);
        Toolbox.Instance.MainMachinery.AddMachines(new BasicMachine(1, HandleBuildJob(jobNumber)));
    }
    private IEnumerable<Action> HandleBuildJob(int jobNumber)
    {
        while (!BuildQueue.IsJobCompleted(jobNumber))
        {
            yield return TimeYields.WaitOneFrame;
        }
        CreateScout();
    }

    private void CreateScout()
    {
        if (!ObjectPool.Pool.TryGetFromPool(out Scout obj)) return;
    }
}
