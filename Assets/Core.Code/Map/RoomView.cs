using System.Collections.Generic;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    public Context context;
    public List<DoorView> doors;
    
    public List<GameObject> toSearchOnAwake;


    public RoomView roomUp;
    public RoomView roomDown;
    public RoomView roomLeft;
    public RoomView roomRight;

    public bool resetColor = true;
    public Color skyColor;
    public bool hideSpecial;
    
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

        for (var index = 0; index < doors.Count; index++)
        {
            var x = doors[index];
            x.InjectRoom(this);
            x.DoorType = index;
        }
    }

    public DoorView GetDoorRight() => doors[2];
    public DoorView GetDoorLeft() => doors[3];
    public DoorView GetDoorUp() => doors[0];
    public DoorView GetDoorDown() => doors[1];

    public RoomView GetNextRoom(DoorView doorView) =>
        doorView.DoorType switch
        {
            0 => roomUp,
            1 => roomDown,
            2 => roomRight,
            3 => roomLeft,
            _ => null
        };

    public void SetSkyColor()
    {
        context.skyController.SetColor(skyColor,resetColor);
        context.toggleSpecialObjects.Toggle(!hideSpecial);
    }
}