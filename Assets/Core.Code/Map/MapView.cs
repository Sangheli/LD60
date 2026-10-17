using System;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    public Context context;
    public List<RoomView> rooms;

    private void Start()
    {
        UpdateVisibleRooms();
    }

    public void UpdateVisibleRooms()
    {
        foreach (var x in rooms)
        {
            if (x == context.playerView.saveData.currentRoom)
            {
                x.gameObject.SetActive(true);
                continue;
            }

            x.gameObject.SetActive(false);
        }
    }
}