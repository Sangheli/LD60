using System;
using Core.Code;
using UnityEngine;

[RequireComponent(typeof(PrefabSpawner))]
[RequireComponent(typeof(AnimatorController))]
[RequireComponent(typeof(ToggleSpecialObjects))]
public class Context : MonoBehaviour
{
    public Content content;
    public PrefabSpawner spawner;
    public AnimatorController animatorController;
    public DataController dataController = new DataController();
    public MapView mapView;
    public PlayerView playerView;
    public SkyController skyController = new SkyController();
    public Camera camera;
    public ToggleSpecialObjects toggleSpecialObjects;
    
    private void OnValidate()
    {
        spawner ??= GetComponent<PrefabSpawner>();
        animatorController ??= GetComponent<AnimatorController>();
        toggleSpecialObjects ??= GetComponent<ToggleSpecialObjects>();
    }

    private void Start()
    {
        skyController.Init(camera);
    }
}