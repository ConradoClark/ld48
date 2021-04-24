using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Debug;
using Licht.Impl.Orchestration;
using Licht.Interfaces.Orchestration;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float StartingCO2;
    public float StartingLuCi;
    public float StartingGel;
    
    private float CO2;
    private float LuCi;
    private float Gel;
    private int CurrentCells;
    private int MaximumCells;
    private IMachine _handleResources;

    void Start()
    {
        CO2 = StartingCO2;
        LuCi = StartingLuCi;
        Gel = StartingGel;
    }

    void OnEnable()
    {
        if (_handleResources != null) Toolbox.Instance.MainMachinery.RemoveMachine(_handleResources);
        Toolbox.Instance.MainMachinery.AddMachines(_handleResources = new BasicMachine(1,HandleResources()));
    }

    public bool Consume(float co2, float luci, float gel)
    {
        if (CO2 < co2 || LuCi < luci || Gel < gel) return false;

        CO2 -= co2;
        LuCi -= luci;
        Gel -= gel;
        return true;
    }

    private IEnumerable<Action> HandleResources()
    {
        while (enabled)
        {
            Toolbox.Instance.UIManager.SetResources(CO2, LuCi, Gel, CurrentCells, MaximumCells);
            yield return TimeYields.WaitOneFrame;
        }
    }

    public void AddUnit(int cost)
    {
        CurrentCells += cost;
    }

    public void AddCells(int cells)
    {
        MaximumCells += cells;
    }
}
