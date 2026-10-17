using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IPoolable
{
    public Context context;
    public Rigidbody rb;
    public ProjectileConfig projectileConfig;
    
    private void Awake()
    {
        rb ??= GetComponent<Rigidbody>();
    }
    
    public void Inject(Context context)
    {
        this.context = context;
    }

    public void Init()
    {
        context.spawner.RemoveObject(gameObject, projectileConfig.lifeTime);
        rb.AddForce(transform.forward * projectileConfig.impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<ICollision>().DoCollision();
        context.spawner.RemoveObject(gameObject,0);
    }
}