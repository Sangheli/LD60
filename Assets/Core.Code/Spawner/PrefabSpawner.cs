using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public Context Context;
    public Transform spawnedParent;
    private List<GameObject> spawnedList = new();

    public GameObject Add(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        var spawned = Instantiate(prefab, pos, rot, spawnedParent);
        spawnedList.Add(spawned);

        if (spawned.TryGetComponent<IPoolable>(out var ipoolable))
        {
            ipoolable.Inject(Context);
            ipoolable.Init();    
        }

        return gameObject;
    }

    public void RemoveObject(GameObject view, float timeOut)
    {
        spawnedList.Remove(view);
        
        if (timeOut > 0.0001)
        {
            StartCoroutine(AwaitDestroy(view, timeOut));
        }
        else
        {
            Destroy(view.gameObject);    
        }
    }

    IEnumerator AwaitDestroy(GameObject view, float time)
    {
        yield return new WaitForSeconds(time);
        if(view == null)
            yield break;
        
        Destroy(view);
    }
}