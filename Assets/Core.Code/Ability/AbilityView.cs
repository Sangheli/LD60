using System;
using UnityEngine;

public class AbilityView : MonoBehaviour, IPoolable
{
    public Context _context;
    public AbilityConfig config;
    public AnimationSettings shootBounceSettings;
    
    private readonly AbilityRuntimeData _data = new();
    
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Transform animationViewTf;

    private Vector3 startAnimationViewPos;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        startAnimationViewPos = animationViewTf.localPosition;
        _data.config = config;
    }

    public void Inject(Context context)
    {
        _context = context;
    }

    public void Use()
    {
        if(!_data.CanShoot())
            return;
        
        _data.UpdateShootTime();
        _context.spawner.Add(projectilePrefab, shootPoint.position, transform.rotation);
        PlayShootAnimation();
    }

    private void PlayShootAnimation()
    {
        _context.animatorController.PlayBounce(animationViewTf,startAnimationViewPos,shootBounceSettings);
    }
}