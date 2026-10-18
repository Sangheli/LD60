using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<AbilityConfig> abilities;
    public bool isAlive => currentHP > 0;

    public RoomView currentRoom;
    public DoorView currentDoor;
    public bool useDoorRot;

    public void Init()
    {
        currentHP = startHP;
    }
    
    public int CollectedStars { get; set; } = 0;

    public List<RoomView> clearedRoom = new List<RoomView>();
    
    public void AddClearedRoom(RoomView room)
    {
        clearedRoom.Add(room);
    }

    public bool IsRoomCleared(RoomView roomView) => clearedRoom.Contains(roomView);

    private int startHP = 100;
    private int currentHP = 0;
    
    public void DoDamager(int value)
    {
        currentHP -= value;
    }
}