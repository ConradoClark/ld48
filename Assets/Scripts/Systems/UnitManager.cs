using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Interfaces.Orchestration;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitManager : MonoBehaviour
{
    private SortedSet<Unit> _units;
    public Unit SelectedUnit { get; private set; }
    private IMachine _selectionMachine;

    public void AddUnit(Unit unit)
    {
        if (_units.Contains(unit)) return;
        _units.Add(unit);

        if (unit.Type == Unit.UnitType.Living) Toolbox.Instance.ResourceManager.AddUnit(unit.CellsCost);
        if (unit.Type == Unit.UnitType.Building) Toolbox.Instance.ResourceManager.AddCells(unit.CellSpaces);
    }

    public void DeselectUnit(Unit unit)
    {
        SelectedUnit = null;
        unit.Deselect();
    }

    public void SelectUnit(Unit unit)
    {
        SelectedUnit = unit;
        unit.Select();
    }

    private void HandleClick(Unit objectOnCursor)
    {
        if (objectOnCursor == null)
        {
            if (SelectedUnit != null) DeselectUnit(SelectedUnit);
            return;
        }

        if (SelectedUnit != null) DeselectUnit(SelectedUnit);
        SelectUnit(objectOnCursor);
    }

    void OnEnable()
    {
        _units = new SortedSet<Unit>(new Unit.UnitComparer());
        if (_selectionMachine != null) Toolbox.Instance.MainMachinery.RemoveMachine(_selectionMachine);
        Toolbox.Instance.MainMachinery.AddMachines(_selectionMachine = new BasicMachine(1, HandleSelection()));
    }

    IEnumerable<Action> HandleSelection()
    {
        yield return TimeYields.WaitOneFrame;
        while (enabled)
        {
            var mousePos = Toolbox.Instance.MainInput.actions["point"].ReadValue<Vector2>();
            var worldPoint = Toolbox.Instance.MainCamera.ScreenToWorldPoint(mousePos);

            var clickAction = Toolbox.Instance.MainInput.actions["click"];
            var press = clickAction.phase == InputActionPhase.Performed && clickAction.triggered && clickAction.ReadValue<float>()==0f;

           var objectOnCursor = _units.FirstOrDefault(unit => unit.Collider.OverlapPoint(worldPoint));
           
           if (press) HandleClick(objectOnCursor);
           foreach (var action in HandleUI(objectOnCursor))
           {
               yield return action;
           }

            yield return TimeYields.WaitOneFrame;
        }
    }

    IEnumerable<Action> HandleUI(Unit objectOnCursor)
    {
        if (objectOnCursor == null)
        {
            if (SelectedUnit == null)
            {
                Toolbox.Instance.UIManager.SetSelection("", "", Color.white);
                yield break;
            }
            Toolbox.Instance.UIManager.SetSelection(SelectedUnit.Info.Name, SelectedUnit.Info.Description, SelectedUnit.Info.Color);
            yield break;
        }
        if (objectOnCursor == SelectedUnit) Toolbox.Instance.UIManager.SetSelection(objectOnCursor.Info.Name, objectOnCursor.Info.Description, objectOnCursor.Info.Color);
    }
}
