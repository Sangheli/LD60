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
    public ParticleSystem coinPickupVfx;
    
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
        context.spawner.RemoveAll();
        saveData.currentRoom.SetSkyColor();
        characterController.enabled = false;
        var newPos = saveData.currentDoor.activeEnterPoint.position;
        newPos.y = moveTF.position.y;
        moveTF.position = newPos;
        yield return null;
        characterController.enabled = true;
        doorTransit = null;

    }

    public float collectVfxCoolDown = 0.15f;
    public float timeToPlayVfx;
    
    
    public void PlayCoinPickup()
    {
        if(Time.time<timeToPlayVfx)
            return;

        timeToPlayVfx = Time.time + collectVfxCoolDown;
        coinPickupVfx.Play();
    }
}