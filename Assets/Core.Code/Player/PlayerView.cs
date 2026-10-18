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
    public ParticleSystem healVfx;
    public ParticleSystem hurtVfx;
    public Transform aimTf;
    
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
        saveData.currentRoom.OnRoomEnter();
        context.spawner.RemoveAll();
        saveData.currentRoom.SetSkyColor();
        characterController.enabled = false;
        var newPos = saveData.currentDoor.activeEnterPoint.position;
        newPos.y = moveTF.position.y;
        moveTF.position = newPos;
        if (saveData.useDoorRot) moveTF.transform.rotation = saveData.currentDoor.activeEnterPoint.rotation;
        yield return null;
        characterController.enabled = true;
        doorTransit = null;
        saveData.useDoorRot = false;
    }

    public float collectVfxCoolDown = 0.15f;
    public float timeToPlayVfx;
    public float timeToPlayVfxHeal;
    public float timeToPlayVfxHurt;
    
    
    public void PlayCoinPickup()
    {
        if(Time.time<timeToPlayVfx)
            return;

        timeToPlayVfx = Time.time + collectVfxCoolDown;
        coinPickupVfx.Play();
    }
    
    public void PlayHeal()
    {
        if(Time.time<timeToPlayVfxHeal)
            return;

        timeToPlayVfxHeal = Time.time + collectVfxCoolDown;
        healVfx.Play();
    }
    
    public void PlayHurt()
    {
        if(Time.time<timeToPlayVfxHurt)
            return;

        timeToPlayVfxHurt = Time.time + collectVfxCoolDown;
        hurtVfx.Play();
    }
}