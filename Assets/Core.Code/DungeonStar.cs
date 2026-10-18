using UnityEngine;

public class DungeonStar : MonoBehaviour, ICollectStar
{
    public Context context;
    public RoomView room;
    
    public void OnCollectStar()
    {
        context.playerView.saveData.AddClearedRoom(room);
        room.OnRoomEnter();
        gameObject.SetActive(false);
    }
}