using System.Collections.Generic;
using UnityEngine;

public class ToggleSpecialObjects : MonoBehaviour
{
    public List<GameObject> toHide;

    public void Toggle(bool state)
    {
        foreach (var x in toHide) x.gameObject.SetActive(state);
    }
}