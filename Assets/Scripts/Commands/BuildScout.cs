using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Impl.Pooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildScout : MonoBehaviour
{
    public Command Command;
    public BuildQueue BuildQueue;
    public Transform SpawnPoint;
    public float BuildTimeInSeconds;
    public ScoutPool ObjectPool;
    public BoxCollider2D SpawnZone;
    public Transform UnitZone;

    // Start is called before the first frame update
    void OnEnable()
    {
        Command.CommandAction = Build;    
    }

    public Vector2 GetRandomSpawnPoint()
    {
        Vector2 extents = SpawnZone.size / 2f;
        Vector2 point = new Vector2(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y)
        ) + SpawnZone.offset;
        return SpawnZone.transform.TransformPoint(point);
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
        obj.transform.parent = UnitZone;
        obj.transform.position = GetRandomSpawnPoint();
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y);
    }
}
