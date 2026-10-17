using UnityEngine;

namespace Core.Code
{
    public class DoorInputView : MonoBehaviour, ITriggerDoor
    {
        public DoorView doorView;

        public void DoTriggerDoor()
        {
            doorView.DoTriggerDoor();
        }
    }
}