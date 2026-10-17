using System;
using Core.Code;
using UnityEngine;

public class DoorView : MonoBehaviour
{
    private RoomView _roomView;
    public Transform view;
    public int DoorType;
    public Transform activeEnterPoint;
    public DoorInputView input;

    private void Awake()
    {
        input.doorView = this;
    }

    public void InjectRoom(RoomView roomView)
    {
        _roomView = roomView;
    }

    public void DoTriggerDoor()
    {
        var nextRoom = _roomView.GetNextRoom(this);
        
        if (nextRoom == null)
            return;
        
        _roomView.context.playerView.saveData.currentRoom = nextRoom;
        _roomView.context.playerView.saveData.currentDoor = GetMirrorDoor(nextRoom);
        _roomView.context.mapView.UpdateVisibleRooms();
        _roomView.context.playerView.EnterRoom();
    }

    public DoorView GetMirrorDoor(RoomView roomView) =>
        DoorType switch
        {
            0 => roomView.GetDoorDown(),
            1 => roomView.GetDoorUp(),
            2 => roomView.GetDoorLeft(),
            3 => roomView.GetDoorRight(),
            _ => null
        };
}