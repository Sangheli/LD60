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
    public Vector3 direction;
    public AbilityConfig config;
    public Transform shootPoint;
    
    private void Awake()
    {
        RuntimeData.data = data;
        RuntimeData.Init();
        
        if (context!=null)
            context.animatorController.PlayFloatAnimation(bounceViewTf,Vector3.zero, animationSettingsJump);
        
        SelectNextWaypoint();
        data1.config = config;
    }
    
    public void Inject(Context context)
    {
        this.context = context;
        context.animatorController.PlayFloatAnimation(bounceViewTf,Vector3.zero, animationSettingsJump);
    }

    public void SelectNextWaypoint()
    {
        var points = new List<int>();
        for (var i = 0; i < waypoints.Count; i++) points.Add(i);
        points.Remove(currentWaypoint);
        currentWaypoint = points[UnityEngine.Random.Range(0, points.Count)];
        direction = (waypoints[currentWaypoint].position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    
    private void Update()
    {
        transform.position += direction * data.Speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.5f) 
            SelectNextWaypoint();
        
        Shoot();
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
    
    public GameObject projectilePrefab;
    private readonly AbilityRuntimeData data1 = new();
    
    public void Shoot()
    {
        if(config == null)
            return;
                
        
        if(!data1.CanShoot())
            return;
        
        data1.UpdateShootTime();
        context.spawner.Add
        (
            projectilePrefab, 
            shootPoint.position, 
            Quaternion.LookRotation(context.playerView.aimTf.position - transform.position).normalized
        );
    }
}