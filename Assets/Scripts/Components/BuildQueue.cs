using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Interfaces.Orchestration;
using UnityEngine;

public class BuildQueue: MonoBehaviour
{
    public ProgressBar Progress;
    private Queue<(int JobNumber, float BuildTimeInSeconds)> _buildQueue;
    private int _jobNumber;
    private float _currentProgress;
    private IMachine _queueMachine;

    void OnEnable()
    {
        _buildQueue = new Queue<(int,float)>();
        if (_queueMachine != null) Toolbox.Instance.MainMachinery.RemoveMachine(_queueMachine);
        Toolbox.Instance.MainMachinery.AddMachines(_queueMachine = new BasicMachine(1, HandleQueue()));
    }

    public int AddJob(float seconds)
    {
        _buildQueue.Enqueue((_jobNumber, seconds));
        return _jobNumber++;
    }

    public bool IsJobCompleted(int job)
    {
        return _buildQueue.Count == 0 || _buildQueue.Peek().JobNumber > job;
    }

    public IEnumerable<Action> HandleQueue()
    {
        while (enabled)
        {
            if (_buildQueue.Count == 0)
            {
                yield return TimeYields.WaitOneFrame;
                continue;
            }

            Progress.EnableBar();
            var currentBuild = _buildQueue.Peek();
            var buildTimeInMillis = currentBuild.BuildTimeInSeconds * 1000;
            _currentProgress = 0f;

            while (_currentProgress < buildTimeInMillis)
            {
                _currentProgress += (float) Toolbox.Instance.MainTimer.UpdatedTimeInMilliseconds;
                Progress.UpdateProgress(_currentProgress / buildTimeInMillis);
                yield return TimeYields.WaitOneFrame;
            }

            Progress.DisableBar();
            _buildQueue.Dequeue();
        }
    }

}
