using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitManager : MonoBehaviour
{
    private SortedSet<Unit> _units;
    public Unit SelectedUnit { get; private set; }

    public void AddUnit(Unit unit)
    {
        if (!_units.Contains(unit)) _units.Add(unit);
        else
        {
            Debug.Log("already has this unit " + unit);
        }
    }

    public void SelectUnit(Unit unit)
    {
        SelectedUnit = unit;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        var press = context.ReadValueAsButton();
        if (!press) return;

        var mousePos = Toolbox.MainInput.actions["point"].ReadValue<Vector2>();
        var worldPoint = Toolbox.MainCamera.ScreenToWorldPoint(mousePos);

        Debug.Log("units: " + _units.Count);

        foreach (var unit in _units.Where(unit => unit.Collider.OverlapPoint(worldPoint)))
        {
            Debug.Log("collision with " + unit);
            if (SelectedUnit!=null) SelectedUnit.Deselect();
            SelectedUnit = unit;
            SelectedUnit.Select();
            return;
        }
        if (SelectedUnit != null) SelectedUnit.Deselect();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("1");
        _units = new SortedSet<Unit>(new Unit.UnitComparer());
        Toolbox.MainInput.actions["click"].performed += OnClick;
    }
}
