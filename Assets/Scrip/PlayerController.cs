using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputActionController controls;
    private Vector2 moveInput;
    public float speed = 5f; // Velocidad de movimiento del jugador

    private void Awake()
    {
        controls = new InputActionController();
        controls.Game.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Game.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Game.Enable();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    private void OnDisable()
    {
        controls.Game.Disable();
    }
}
