using System;
using Core.Code.Player;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Context context;
    public KeyboardInput keyboardInput = new();
    private IPlayerInput _playerInput = new PlayerInputHuman();
    public SaveData saveData = new();
    public AbilityView currentAbility;

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
}