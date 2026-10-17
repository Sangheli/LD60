using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<AbilityConfig> abilities;
    public bool isAlive = true;

    public RoomView currentRoom;
    public DoorView currentDoor;
}