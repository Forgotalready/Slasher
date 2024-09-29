using System;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    private IControllable _controllable;
    public GameInput GameInput {  get; private set; }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        ReadMovement();
    }

    public void Init()
    {
        GameInput = new GameInput();
        GameInput.Enable();

        _controllable = GetComponent<IControllable>();

        if (_controllable == null)
        {
            throw new Exception("PlayerController not installed");
        }
    }

    private void ReadMovement()
    {
        var inputDirection = GameInput.Gameplay.Movement.ReadValue<Vector2>();
        _controllable.InputUpdate(inputDirection);
    }


}
