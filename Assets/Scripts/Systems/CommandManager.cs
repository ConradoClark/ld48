using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Interfaces.Orchestration;
using UnityEditor.EventSystems;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private HashSet<Command> _commands;
    private IMachine _hoverMachine;
    public bool Hovering;

    public void Add(Command command)
    {
        if (_commands.Contains(command)) return;
        _commands.Add(command);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        _commands = new HashSet<Command>();
        if (_hoverMachine != null) Toolbox.Instance.MainMachinery.RemoveMachine(_hoverMachine);
        Toolbox.Instance.MainMachinery.AddMachines(_hoverMachine = new BasicMachine(1, HandleHover()));
    }

    IEnumerable<Action> HandleHover()
    {
        while (enabled)
        {
            var mousePos = Toolbox.Instance.MainInput.actions["point"].ReadValue<Vector2>();
            var worldPoint = Toolbox.Instance.MainCamera.ScreenToWorldPoint(mousePos);

            var objectOnCursor = _commands.FirstOrDefault(command => command.Collider.OverlapPoint(worldPoint));

            if (objectOnCursor == null)
            {
                Hovering = false;
                Toolbox.Instance.UIManager.SetCommandInfo("", "", Color.white);
                yield return TimeYields.WaitOneFrame;
                continue;
            }

            Hovering = true;
            Toolbox.Instance.UIManager.SetCommandInfo(objectOnCursor.Title, objectOnCursor.Description, objectOnCursor.TitleColor);
            yield return TimeYields.WaitOneFrame;
        }
    }
}
