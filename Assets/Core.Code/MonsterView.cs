using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour, ICollision, IContextInject
{
    public Context context;
    public HyperCubeData data;
    public AnimationSettings animationSettings;
    public AnimationSettings animationSettingsJump;
    public Transform bounceViewTf;
    public Transform jumpViewTf;
    public HyperCubeRuntimeData RuntimeData = new();
    public CoinView coinPrefab;
    public HealView healPrefab;
    public ParticleSystem vfx;

    public List<Transform> waypoints;

    private int currentWaypoint = 0;
    private int lastWaypoint = 0;
    public Vector3 direction;
    
    private void Awake()
    {
        RuntimeData.data = data;
        RuntimeData.Init();
        
        if (context!=null)
            context.animatorController.PlayFloatAnimation(bounceViewTf,Vector3.zero, animationSettingsJump);
        
        SelectNextWaypoint();
    }
    
    public void Inject(Context context)
    {
        this.context = context;
        context.animatorController.PlayFloatAnimation(bounceViewTf,Vector3.zero, animationSettingsJump);
    }

    public void SelectNextWaypoint()
    {
        lastWaypoint = currentWaypoint;
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Count) currentWaypoint = 0;
        direction = (waypoints[currentWaypoint].position - transform.position).normalized;
    }
    
    private void Update()
    {
        transform.position += direction * data.Speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.5f)
        {
            SelectNextWaypoint();
            transform.rotation = waypoints[lastWaypoint].rotation;
        }
    }

    public void DoCollision()
    {
        if(!RuntimeData.IsAlive())
            return;
        
        context.animatorController.PlayBounce(bounceViewTf,Vector3.zero, animationSettings);
        RuntimeData.DoDamage(1);
        
        if (vfx!=null)
            vfx.Play();

        if (!RuntimeData.IsAlive())
        {
            SpawnCoins();
            SpawnHeals();
            gameObject.SetActive(false);
        }
            
    }

    private void SpawnCoins()
    {
        var num = data.GetCoinToSpawn();
        
        for (var i = 0; i < num; i++)
        {
            var spawnPos = transform.position + new Vector3(
                UnityEngine.Random.Range(-data.spawnShift.x, data.spawnShift.x),
                UnityEngine.Random.Range( data.spawnShift.y/2, data.spawnShift.y),
                UnityEngine.Random.Range(-data.spawnShift.z, data.spawnShift.z)
            );

            context.spawner.Add(coinPrefab.gameObject, spawnPos, Quaternion.identity);
        }
    }
    
    private void SpawnHeals()
    {
        int num = data.GetHealToSpawn();
        
        for (var i = 0; i < num; i++)
        {
            var spawnPos = transform.position + new Vector3(
                UnityEngine.Random.Range(-data.spawnShift.x, data.spawnShift.x),
                UnityEngine.Random.Range( data.spawnShift.y/2, data.spawnShift.y),
                UnityEngine.Random.Range(-data.spawnShift.z, data.spawnShift.z)
            );

            context.spawner.Add(healPrefab.gameObject, spawnPos, Quaternion.identity);
        }
    }
}