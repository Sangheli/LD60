using System;
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
    
    private void Awake()
    {
        RuntimeData.data = data;
        RuntimeData.Init();
    }
    
    public void Inject(Context context)
    {
        this.context = context;
        context.animatorController.PlayFloatAnimation(bounceViewTf,Vector3.zero, animationSettingsJump);
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