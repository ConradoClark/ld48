using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Interfaces.Orchestration;
using UnityEngine;
using UnityEngine.InputSystem;

public class Command : MonoBehaviour
{
    public Collider2D Collider;
    public Action CommandAction;
    public string Title;
    public string Description;
    public Color TitleColor = Color.white;
    private IMachine _commandMachine;

    void OnEnable()
    {
        Toolbox.Instance.CommandManager.Add(this);
        if (_commandMachine != null) Toolbox.Instance.MainMachinery.RemoveMachine(_commandMachine);
        Toolbox.Instance.MainMachinery.AddMachines(_commandMachine = new BasicMachine(1, HandleCommand()));
    }

    private IEnumerable<Action> HandleCommand()
    {
        yield return TimeYields.WaitOneFrame;
        
        while (enabled)
        {
            var mousePos = Toolbox.Instance.MainInput.actions["point"].ReadValue<Vector2>();
            var worldPoint = Toolbox.Instance.MainCamera.ScreenToWorldPoint(mousePos);

            var clickAction = Toolbox.Instance.MainInput.actions["click"];
            var press = clickAction.phase == InputActionPhase.Performed && clickAction.triggered && clickAction.ReadValue<float>() == 0f;
            var trigger = press && Collider.OverlapPoint(worldPoint);

            if (trigger)
            {
                CommandAction();
            }

            yield return TimeYields.WaitOneFrame;
        }
    }
}
