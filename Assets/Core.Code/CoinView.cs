using System;
using UnityEngine;

public class CoinView : MonoBehaviour, IPoolable, ICollision
{
    public CoinConfig config;
    public Context context;
    public Transform rotateView;
    public AnimationSettings animationSettings;

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
        context.dataController.AddCoin(config.nominal);
    }
}