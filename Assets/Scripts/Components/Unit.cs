using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class Unit : MonoBehaviour
{
    public float HitPoints;
    public SpriteRenderer UnitSprite;
    public SpriteRenderer SelectionSprite;
    public Collider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("2");
        Toolbox.UnitManager.AddUnit(this);
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void Select()
    {
        SelectionSprite.enabled = true;
    }

    public void Deselect()
    {
        SelectionSprite.enabled = false;
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