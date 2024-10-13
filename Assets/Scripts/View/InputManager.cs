using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public PlayerInputActions inputActions { get; set; }
    public static Vector2 movement { get; set; }
    public static Vector2 mousePosition { get; set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        inputActions = new PlayerInputActions();
        inputActions.PC.Move.performed += v => movement = v.ReadValue<Vector2>();
        inputActions.PC.Move.canceled += v => movement = v.ReadValue<Vector2>();
        inputActions.PC.Mouse.performed += v => mousePosition = v.ReadValue<Vector2>();

        inputActions.PC.Enable();
    }

    private void Update()
    {
    }

    void Move()
    {

    }
}
