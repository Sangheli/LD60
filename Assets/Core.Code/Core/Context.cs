using System;
using UnityEngine;

[RequireComponent(typeof(PrefabSpawner))]
[RequireComponent(typeof(AnimatorController))]
public class Context : MonoBehaviour
{
    public Content content;
    public PrefabSpawner spawner;
    public AnimatorController animatorController;
    public DataController dataController = new DataController();
    
    private void OnValidate()
    {
        spawner ??= GetComponent<PrefabSpawner>();
        animatorController ??= GetComponent<AnimatorController>();
    }
}