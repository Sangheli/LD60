using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ITriggerDoor>(out var x))
        {
            x.DoTriggerDoor();
        }
    }
}