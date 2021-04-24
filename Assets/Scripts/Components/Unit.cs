using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class Unit : MonoBehaviour
{
    [Serializable]
    public class UnitInfo
    {
        public string Name;
        public string Description;
    }

    public float HitPoints;
    public SpriteRenderer UnitSprite;
    public SpriteRenderer SelectionSprite;
    public Collider2D Collider;
    public Transform CommandsObject;
    public UnitInfo Info;

    void OnEnable()
    {
        Toolbox.Instance.UnitManager.AddUnit(this);
    }

    public void Select()
    {
        SelectionSprite.enabled = true;
        CommandsObject.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        SelectionSprite.enabled = false;
        CommandsObject.gameObject.SetActive(false);
    }

    public class UnitComparer : IComparer<Unit>
    {
        public int Compare(Unit x, Unit y)
        {
            if (x == null || y==null) return -1;

            var comparison = x.UnitSprite.sortingOrder.CompareTo(y.UnitSprite.sortingOrder);
            if (comparison == 0) comparison = x.GetInstanceID().CompareTo(y.GetInstanceID());
            return comparison;
        }
    }
}