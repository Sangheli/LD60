using UnityEngine;

public class AbilityRuntimeData
{
    public float nextShootTime;
    public AbilityConfig config;

    public void UpdateShootTime()
    {
        nextShootTime = Time.time + 1f / config.fireRate;
    }

    public bool CanShoot() => Time.time > nextShootTime;
}