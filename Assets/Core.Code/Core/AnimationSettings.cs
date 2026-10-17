using UnityEngine;

[CreateAssetMenu]
public class AnimationSettings : ScriptableObject
{
    public Vector3 ShiftV3;
    public float Shift = 1f;
    public float Time = 1f;
    public float StepCount = 5;
    public bool useRandomStart;

    public int GetStartShift() => useRandomStart ? UnityEngine.Random.Range(0, (int)StepCount) : 0;
}