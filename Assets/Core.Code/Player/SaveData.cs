using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<AbilityConfig> abilities;
    public bool isAlive = true;

    public RoomView currentRoom;
    public DoorView currentDoor;
    public bool useDoorRot;
    
    public int CollectedStars { get; set; } = 0;

    public List<RoomView> clearedRoom = new List<RoomView>();
    
    public void AddClearedRoom(RoomView room)
    {
        clearedRoom.Add(room);
    }

    public bool IsRoomCleared(RoomView roomView) => clearedRoom.Contains(roomView);
}