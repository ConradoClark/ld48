using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public  Transform PoorProgressBar;
    public float ScaleAtCompletion;
    
    public void UpdateProgress(float progress)
    {
        PoorProgressBar.localScale = new Vector3(Mathf.Min(ScaleAtCompletion, progress * ScaleAtCompletion), PoorProgressBar.localScale.y, PoorProgressBar.localScale.z);
    }

    public void EnableBar()
    {
        gameObject.SetActive(true);
    }

    public void DisableBar()
    {
        gameObject.SetActive(false);
    }
}
