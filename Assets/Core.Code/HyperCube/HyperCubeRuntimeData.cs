public class HyperCubeRuntimeData
{
    public HyperCubeData data;
    public float CurrentHP;

    public void Init()
    {
        CurrentHP = data.HP;
    }

    public bool IsAlive() => CurrentHP > 0;

    public void Reset()
    {
        Init();
    }

    public void DoDamage(int i)
    {
        CurrentHP -= i;
    }
}