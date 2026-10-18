using UnityEngine;

namespace Core.Code
{
    public class PortalDoor : MonoBehaviour, ITriggerDoor
    {
        public Context context;
        public RoomView nextRoom;
        public DoorView nextDoor;
        
        public void DoTriggerDoor()
        { 
            context.playerView.saveData.currentRoom = nextRoom;
            context.playerView.saveData.currentDoor = nextDoor;
            context.mapView.UpdateVisibleRooms();
            context.playerView.saveData.useDoorRot = true;
            context.playerView.EnterRoom();
        }
    }
}