using System;
using System.Collections;
using Core.Code.Player;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Rigidbody rb;
    public CharacterController characterController;
    public Context context;
    public KeyboardInput keyboardInput = new();
    private IPlayerInput _playerInput = new PlayerInputHuman();
    public SaveData saveData;
    public AbilityView currentAbility;
    public Transform moveTF;
    
    private void Awake()
    {
        currentAbility._context = context;
        _playerInput.ability = currentAbility;
        keyboardInput.Input = _playerInput;
    }

    private void FixedUpdate()
    {
        if(!saveData.isAlive)
            return;
        
        keyboardInput.Update();
    }

    private IEnumerator doorTransit;
    
    public void EnterRoom()
    {
        if(doorTransit!=null)
            return;
        
        doorTransit = EnterRoomIE();
        StartCoroutine(doorTransit);
    }

    IEnumerator EnterRoomIE()
    {
        characterController.enabled = false;
        var newPos = saveData.currentDoor.activeEnterPoint.position;
        newPos.y = moveTF.position.y;
        moveTF.position = newPos;
        yield return null;
        characterController.enabled = true;
        doorTransit = null;
    }
}