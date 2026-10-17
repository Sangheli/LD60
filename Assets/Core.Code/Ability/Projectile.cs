using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IPoolable
{
    public Context context;
    public Rigidbody rb;
    public float lifeTime = 5;
    public float impulse = 5;
    
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
        context.spawner.RemoveObject(gameObject, lifeTime);
        rb.AddForce(transform.forward * impulse);
    }
}