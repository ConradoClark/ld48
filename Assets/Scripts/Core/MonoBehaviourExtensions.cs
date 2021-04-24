using UnityEngine;
using System.Collections;

public static class MonoBehaviourExtensions
{
    public static T GetOrAddComponent<T>(this Component child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            Debug.Log("type " + typeof(T) + " not found. Adding new one.");
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }
}