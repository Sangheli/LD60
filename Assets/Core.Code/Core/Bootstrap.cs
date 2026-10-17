using System;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public Context context;
    public List<GameObject> toSearchOnAwake;
    
    private void Awake()
    {
        foreach (var x in toSearchOnAwake)
        {
            var toInject = x.GetComponentsInChildren<IContextInject>();
            foreach (var y in toInject)
            {
                y.Inject(context);
            }
        }
    }
}