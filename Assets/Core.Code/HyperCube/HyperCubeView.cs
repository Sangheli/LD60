using System;
using UnityEngine;

public class HyperCubeView : MonoBehaviour, ICollision, IContextInject
{
    public Context context;
    public HyperCubeData data;
    public AnimationSettings animationSettings;
    public Transform bounceViewTf;
    public HyperCubeRuntimeData RuntimeData = new HyperCubeRuntimeData();
    public CoinView coinPrefab;
    public ParticleSystem vfx;
    
    private void Awake()
    {
        RuntimeData.data = data;
        RuntimeData.Init();
    }
    
    public void Inject(Context context)
    {
        this.context = context;
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
}