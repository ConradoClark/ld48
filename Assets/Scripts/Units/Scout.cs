using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Interfaces.Orchestration;
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
        Toolbox.Instance.MainMachinery.AddMachines(new  BasicMachine(1,Float()), new BasicMachine(1,WanderMovement()));
        return true;
    }

    public IEnumerable<Action> Float()
    {
        var originalY = transform.localPosition.y;
        while (enabled)
        {
            // float down
            float lerp = originalY;
            var actions = EasingYields.Lerp(f => lerp = f, () => lerp, 1.5f, originalY - 0.5f, EasingYields.EasingFunction.SineEaseInOut,
                Toolbox.Instance.MainTimer);
            foreach (var act in actions)
            {
                if (!enabled) goto reset;
                yield return act;
                transform.localPosition = new Vector3(transform.localPosition.x, lerp, transform.localPosition.z);
            }

            // float up
            lerp = transform.localPosition.y;
            actions = EasingYields.Lerp(f => lerp = f, () => lerp, 1.5f, originalY, EasingYields.EasingFunction.SineEaseInOut,
                Toolbox.Instance.MainTimer);
            foreach (var act in actions)
            {
                if (!enabled) goto reset;
                yield return act;
                transform.localPosition = new Vector3(transform.localPosition.x, lerp, transform.localPosition.z);
            }
        }

        reset:
        transform.localPosition =
            new Vector3(transform.localPosition.x, originalY, transform.localPosition.z);
    }

    public IEnumerable<Action> WanderMovement()
    {
        var originalX = transform.localPosition.x;
        while (enabled)
        {
            // float down
            float lerp = originalX;
            var actions = EasingYields.Lerp(f => lerp = f, () => lerp, 4f, originalX + 1f, EasingYields.EasingFunction.SineEaseInOut,
                Toolbox.Instance.MainTimer);
            foreach (var act in actions)
            {
                if (!enabled) goto reset;
                yield return act;
                transform.localPosition = new Vector3(lerp, transform.localPosition.y, transform.localPosition.z);
            }

            // float up
            lerp = transform.localPosition.x;
            actions = EasingYields.Lerp(f => lerp = f, () => lerp, 4f, originalX, EasingYields.EasingFunction.SineEaseInOut,
                Toolbox.Instance.MainTimer);
            foreach (var act in actions)
            {
                if (!enabled) goto reset;
                yield return act;
                transform.localPosition = new Vector3(lerp, transform.localPosition.y, transform.localPosition.z);
            }
        }

        reset:
        transform.localPosition =
            new Vector3(originalX, transform.localPosition.y, transform.localPosition.z);
    }
}
