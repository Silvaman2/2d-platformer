using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    [SerializeField] public PlayerControls controls;

    public float movementInput = 0;
    public bool jumpInput = false;
    public bool jumpEndInput = false;
    public bool actionInput = false;
    public bool attackInput = false;
    public bool dropInput = false;
    public bool dashInput = false;
    public bool resetSceneInput = false;

    public void Start()
    {
        controls = new PlayerControls();
        controls.Gameplay.Enable();
    }

    public void Update()
    {
        movementInput = controls.Gameplay.Move.ReadValue<Vector2>().x;
        jumpInput = controls.Gameplay.Jump.WasPressedThisFrame();
        jumpEndInput = controls.Gameplay.Jump.WasReleasedThisFrame();
        actionInput = controls.Gameplay.Action.WasPressedThisFrame();
        attackInput = controls.Gameplay.Action.IsPressed();
        dropInput = controls.Gameplay.Drop.WasPressedThisFrame();
        dashInput = controls.Gameplay.Dash.WasPressedThisFrame();
        resetSceneInput = controls.Gameplay.ResetScene.WasPressedThisFrame();
    }
}
