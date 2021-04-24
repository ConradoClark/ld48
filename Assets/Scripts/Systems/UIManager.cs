using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text InfoCaption;
    public TMP_Text InfoName;
    public TMP_Text InfoDescription;

    public TMP_Text CommandCaption;
    public TMP_Text CommandName;
    public TMP_Text CommandDescription;

    public TMP_Text CO2Counter;
    public TMP_Text LuciCounter;
    public TMP_Text GelCounter;
    public TMP_Text CellsCounter;

    public void SetSelection(string title, string description, Color color)
    {
        InfoName.text = title;
        InfoDescription.text = description;
        InfoName.color = color;
        InfoCaption.enabled = !string.IsNullOrWhiteSpace(title);
    }

    public void SetCommandInfo(string title, string description, Color color)
    {
        CommandName.text = title;
        CommandDescription.text = description;
        CommandName.color = color;
        CommandCaption.enabled = !string.IsNullOrWhiteSpace(title);
    }

    public void SetResources(float co2, float luci, float gel, int cells, int maximumCells)
    {
        CO2Counter.text = Mathf.CeilToInt(co2).ToString().PadLeft(3, '0');
        LuciCounter.text = Mathf.CeilToInt(luci).ToString().PadLeft(3, '0');
        GelCounter.text = Mathf.CeilToInt(gel).ToString().PadLeft(3, '0');
        CellsCounter.text =
            $"{Mathf.Min(cells, 99).ToString().PadLeft(2, '0')}/{Mathf.Min(maximumCells, 99).ToString().PadLeft(2, '0')}";
    }
}
