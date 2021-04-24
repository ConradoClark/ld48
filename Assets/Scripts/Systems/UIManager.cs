using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text InfoName;
    public TMP_Text InfoDescription;

    public void SetSelection(string name, string description)
    {
        InfoName.text = name;
        InfoDescription.text = description;
    }
}
