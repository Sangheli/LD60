using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Content : ScriptableObject
{
    public List<AbilityConfig> abilities;
}

[Serializable]
public class AbilityConfig
{
    public int count;
    public float cooldown;
}
