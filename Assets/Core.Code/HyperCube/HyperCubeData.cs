using UnityEngine;

[CreateAssetMenu]
public class HyperCubeData : ScriptableObject
{
    public float coinToSpawnMin = 1;
    public float coinToSpawn = 5;
    public float HP = 20;
    public Vector3 spawnShift;

    public int GetCoinToSpawn() => (int)UnityEngine.Random.Range(coinToSpawnMin, coinToSpawn + 1);
}