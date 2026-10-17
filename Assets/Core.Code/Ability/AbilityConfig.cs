using UnityEngine;

[CreateAssetMenu]
public class AbilityConfig : ScriptableObject
{
    public GameObject prefab;
    public float fireRate;
    public int count;
    public float cooldown;
}