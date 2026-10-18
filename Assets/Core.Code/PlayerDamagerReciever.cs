using UnityEngine;

public class PlayerDamagerReciever : MonoBehaviour, ICollision
{
    public Context context;
    
    public void DoCollision()
    {
        context.playerView.saveData.DoDamager(1);
        context.playerView.PlayHurt();
    }
}