using System.Collections.Generic;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    public Context context;
    private RoomData _data;
    public List<DoorView> doors;
    
    public List<GameObject> toSearchOnAwake;
    
    private void Awake()
    {
        foreach (var x in toSearchOnAwake)
        {
            var toInject = x.GetComponentsInChildren<IContextInject>();
            foreach (var y in toInject)
            {
                y.Inject(context);
            }
        }
    }
}