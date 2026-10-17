using UnityEngine;

public class GrassView : MonoBehaviour, IContextInject
{
    public Context context;
    public AnimationSettings animationSettings;
    public Transform animTf;
    
    public void Inject(Context context)
    {
        this.context = context;
    }
}