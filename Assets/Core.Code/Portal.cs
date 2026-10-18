using System;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Context context;
    public List<GameObject> stars;

    private void Awake()
    {
        UpdateStars();
    }

    public void UpdateStars()
    {
        int num = context.playerView.saveData.CollectedStars;

        foreach (var x in stars) x.gameObject.SetActive(false);
        for (var index = 0; index < num; index++) stars[index].gameObject.SetActive(true);
    }
}