using UnityEngine;

public class TreeView : MonoBehaviour, ICollision
{
    public Context context;
    public AnimationSettings animationSettings;
    public TreeRuntimeData runtimeData = new ();
    public TreeData data;
    public Transform bounceViewTf;
    
    private void Awake()
    {
        runtimeData.data = data;
        runtimeData.Init();
    }

    public void DoCollision()
    {
        if(!runtimeData.IsAlive())
            return;
        
        context.animatorController.PlayBounce(bounceViewTf,Vector3.zero, animationSettings);
        runtimeData.DoDamage(1);

        if (!runtimeData.IsAlive())
        {
            gameObject.SetActive(false);
        }
            
    }
}