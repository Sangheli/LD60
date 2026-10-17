using System;
using UnityEngine;

[RequireComponent(typeof(PrefabSpawner))]
[RequireComponent(typeof(AnimatorController))]
public class Context : MonoBehaviour
{
    public Content content;
    public PrefabSpawner spawner;
    public AnimatorController animatorController;

    private void Awake()
    {
        spawner ??= GetComponent<PrefabSpawner>();
        animatorController ??= GetComponent<AnimatorController>();
    }
}