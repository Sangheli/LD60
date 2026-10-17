using System;
using UnityEngine;

public class CoinView : MonoBehaviour, IPoolable, ICollision, ICollectLoot
{
    public CoinConfig config;
    public Context context;
    public Transform rotateView;
    public AnimationSettings animationSettings;

    public Rigidbody rb;

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(context!=null)
            Init();
    }

    public void Init()
    {
        context.animatorController.PlayRotate(rotateView,animationSettings);
    }

    public void Inject(Context context)
    {
        this.context = context;
    }

    public void DoCollision()
    {
      
    }

    public void DoCollect()
    {
        rb.isKinematic = true;
        context.dataController.AddCoin(config.nominal);
        context.spawner.RemoveObject(gameObject,0.001f);
    }
}